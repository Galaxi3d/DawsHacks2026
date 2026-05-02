function formValue(){
    // gather user input to be put into db
    const firstName = document.getElementById("firstName").value.trim();
    const lastName = document.getElementById("lastName").value.trim();
    const email = document.getElementById("email").value.trim();
    const passwd = document.getElementById("passwd").value;

    if (firstName && lastName && email && passwd.length >= 5) {
        signupUpload(firstName, lastName, email, passwd);
    } else {
        alert("Please fill in all fields and use a password with at least 5 characters.");
    }
}

function signupUpload(firstName, lastName, email, passwd){
    fetch("http://10.230.122.4:5001/api/usercontrollers/RegisterUser", {
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
    .then(response => response.json())
    .then(data => {
        console.log(data);
        alert("Signup request sent.");
    })
    .catch(error => {
        console.error(error);
        alert("Signup failed. Check the console for details.");
    });
}