
import firebase from "firebase/compat/app";
import "firebase/compat/database";

const firebaseConfig = {
  apiKey: "AIzaSyAKPa9UVXvnp-AQ0v8dwTfxcFCpvhkvuPo",
  authDomain: "student-4bcd8.firebaseapp.com",
  databaseURL: "https://student-4bcd8-default-rtdb.europe-west1.firebasedatabase.app",
  projectId: "student-4bcd8",
  storageBucket: "student-4bcd8.appspot.com",
  messagingSenderId: "285290956406",
  appId: "1:285290956406:web:995d09b6278fad3d3e8157"
};

firebase.default.initializeApp(firebaseConfig);

export async function GET_DATA(path) {
  let response;
  await firebase.database().ref(path).get().then(data => {
      if(data.exists()) {
        response = data.val();
      } 
      else {
        response = false;
      }
  });
  return response;
}

export async function WRITE_DATA(updates) {
  await firebase.database().ref().update(updates);
}


