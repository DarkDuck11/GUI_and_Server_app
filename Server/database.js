import {GET_DATA, WRITE_DATA} from './firebase.js';
import firebase from "firebase/compat/app";

export async function login(login, password) {
    let message = '';
    const data = await GET_DATA(`Users/${login}`);
    if(data) {
        if(data.password === password) {
            message = data.role;
        }
        else {
            message = 'Пароль неправильний.';
        }
    } 
    else {
        message = 'Користувач з даним логіном відсутній.';
    }

    return message;
}

export async function registration(login, password, firstName, lastName, role) {
    firstName = firstName[0].toUpperCase() + firstName.slice(1);
    lastName = lastName[0].toUpperCase() + lastName.slice(1);
    role = role[0].toUpperCase() + role.slice(1);
    let message = '';
    if(await GET_DATA(`Users/${login}`)) {
        message = 'Користувач з даним логіном вже існує.';
    }
    else {
        let updates = {};
        updates[`Users/${login}/password`] = password;
        updates[`Users/${login}/firstName`] = firstName;
        updates[`Users/${login}/lastName`] = lastName;
        updates[`Users/${login}/role`] = role;

        await WRITE_DATA(updates);
        message = 'Реєстрація пройшла успішно.';
    }
    return message;
}

export async function addHomework(login, name, text) {
    name = name[0].toUpperCase() + name.slice(1);
    let message = '';
    const courses = await GET_DATA(`Courses`);
    if(Object.keys(courses).includes(name)) {
        const data = await GET_DATA(`Users/${login}/Homework/${name}`);
        if(data) {
            message = 'Домашнє завдання з такою назвою вже існує.';
        }
        else {
            let updates = {};
            updates[`Users/${login}/Homework/${name}/description`] = text;
            await WRITE_DATA(updates);
            message = 'Домашнє завдання додано.';
        }
    }
    else {
        message = 'Не можливо додати домашнє завдання, на курс, який не існує.'
    }

    return message;
}

export async function getHomework(login) {
    let message = '';
    const data = await GET_DATA(`Users/${login}/Homework`);
    if(data) {
        for(let name in data) {
           if(data[name]?.rating) {
                message += `${name} - ${data[name].rating}/5\n`;
           }
           else {
            message += `${name} - неоцінено\n`;
           }        
        }
    }
    return message.slice(0, -1);
}

export async function searchCourse(name) {
    name = name[0].toUpperCase() + name.slice(1);
    let message = '';
    const data = await GET_DATA(`Courses/${name}`);
    if(data) {
        message = data;
    }
    else {
        message = 'Курс не знайдено.';
    }

    return message;
}

export async function addCourse(name, text) {
    name = name[0].toUpperCase() + name.slice(1);
    let message = '';
    const data = await GET_DATA(`Courses/${name}`);
    if(data) {
        message = 'Курс з такою назвою вже існує.';
    }
    else {
        let updates = {};
        updates[`Courses/${name}`] = text;
        await WRITE_DATA(updates);
        message = 'Курс додано.';
    }
    return message;
}


export async function deleteCourse(name) {
    name = name[0].toUpperCase() + name.slice(1);
    let response = '';
    const data = await GET_DATA(`Courses/${name}`);
    if(data) {
        response = 'Курс видалено.';
        await firebase.database().ref(`Courses/${name}`).remove()
        .catch(() => response = 'Не вдалось видалити курс.');
    }
    else {
        response = 'Курсу з даною назвою не існує.'
    }
    return response;
}

export async function searchHomework(login, course) {
    course = course[0].toUpperCase() + course.slice(1);
    let message = '';
    const data = await GET_DATA(`Users/${login}`);
    if(data) {
        if(data.role === 'Student') {
            if(data?.Homework?.[course]) {
                message = `true&${data.Homework[course].description}`;
            }
            else {
                message = 'false&У студента відсутнє дане домашнє завдання.';
            }
        }
        else {
            message = 'false&Даний користувач не є студентом.'
        }
    }
    else {
        message = 'false&Студента з таким іменем не існує.';
    }

    return message;
}

export async function addRating(login, course, rating) {
    course = course[0].toUpperCase() + course.slice(1);
    let message = '';
    const data = await GET_DATA(`Users/${login}/Homework/${course}/rating`);
    if(data) {
        message = 'Ви вже оцінили даний курс студентові.';
    }
    else {
        let updates = {};
        updates[`Users/${login}/Homework/${course}/rating`] = rating;
        await WRITE_DATA(updates);
        message = 'Завдання оцінено.';
    }
    return message;
}
