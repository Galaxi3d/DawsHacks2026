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
            email : email,
            password: passwd
        })
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`Request failed with status ${response.message}`);
        }
        return response.json();
    })
    .then(data => {
        if (data.success) {
            localStorage.setItem("user_id", data.user_id);
            localStorage.setItem("points", data.points);
            localStorage.setItem("firstName", data.firstName);
            localStorage.setItem("lastName", data.lastName);
            localStorage.setItem("email", email);
        }
    })
    .catch(error => {
        console.error(error);
        alert(error.message);
    });
}