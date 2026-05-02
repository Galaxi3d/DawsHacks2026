function loginValues(){
    //gather login info
    const email=document.getElementById("email").value.trim();
    const passwd=document.getElementById("passwd").value;
    if (email && passwd){
        loginUpload(email, passwd)
    }else {
        alert("Please fill in all fields");//incorrect input message
    }
}

function loginUpload(email, passwd){
    fetch("http://10.230.122.4:5001/api/usercontrollers/loginUser", {//send login info to db
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
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