function signupValues(){
    // gather user input to be put into db
    const firstName=document.getElementById("firstName").value.trim();
    const lastName=document.getElementById("lastName").value.trim();
    const email=document.getElementById("email").value.trim();
    const passwd=document.getElementById("passwd").value;

    if (firstName && lastName && email && passwd.length >= 5) {//input validation. all values must hava data, password longer than 5 char
        signupUpload(firstName, lastName, email, passwd);
    } else {
        alert("Please fill in all fields and use a password with at least 5 characters.");//incorrect input message
    }
}

function signupUpload(firstName, lastName, email, passwd){
    fetch("http://10.230.122.4:5001/api/usercontrollers/RegisterUser", {//send signup info to db
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            firstName,
            lastName,
            email,
            password: passwd
        })
    })
    .then(data => {
        localStorage.setItem("userId", JSON.stringify(data));
        localStorage.setItem("points", 0);
        localStorage.setItem("firstName", firstName);
        localStorage.setItem("lastName", lastName);
        localStorage.setItem("email", email);

        console.log(data);

    })
    .catch(error => {
        console.error(error);
        alert(error.message);
    });
}