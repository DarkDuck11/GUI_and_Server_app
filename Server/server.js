import http from 'http';
import decodeUriComponent from 'decode-uri-component';
import {login, 
    registration, 
    addHomework,
    getHomework,
    searchCourse,
    addCourse,
    deleteCourse,
    searchHomework,
    addRating
} from './database.js';


const port = process.env.PORT || 3000;

http.createServer((request, response) => {
    let body = '';
    request.on('data', chunk => {
        body += chunk;
    });
    request.on('end', async () => {
        const func = body.split('=')[0];
        const data = decodeUriComponent(body.split('=')[1]).split('&');
        let resp = '';
        console.log(`Запит: ${func}`);
        if(!!data[0]) {
            console.log(data);
        }
        

        if(func === 'login') {
            resp = await login(data[0], data[1]);
        }
        if(func === 'registration') {
            resp = await registration(data[0], data[1], data[2], data[3], data[4]);
        }
        if(func === 'addHomework') {
            resp = await addHomework(data[0], data[1], data[2]);
        }
        if(func === 'getHomework') {
            resp = await getHomework(data[0]);
        }
        if(func === 'searchCourse') {
            resp = await searchCourse(data[0]);
        }
        if(func === 'addCourse') {
            resp = await addCourse(data[0], data[1]);
        }
        if(func === 'deleteCourse') {
            resp = await deleteCourse(data[0]);
        }
        if(func === 'searchHomework') {
            resp = await searchHomework(data[0], data[1]);
        }
        if(func === 'addRating') {
            resp = await addRating(data[0], data[1], data[2]);
        }
        if(func === 'logout') {
            resp = 'true';
        }

        console.log(`Відповідь: ${resp}\n`);
        response.end(resp);
    });
}).listen(port, () => console.log(`Server started on port ${port}`));