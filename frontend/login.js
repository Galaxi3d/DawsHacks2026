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
            email: email,
            password: passwd
        })
    })
    .then(response => response.text().then(text => {
        const body = text ? JSON.parse(text) : {};
        return { ok: response.ok, body };
    }))
    .then(({ ok, body }) => {
        if (!ok) {
            throw new Error(body.message || body.error || "Login failed. Check your email and password.");
        }

        const userId = body.userId || body.user_id || body.id;
        const firstName = body.firstName || body.first_name;
        const lastName = body.lastName || body.last_name;

        if (userId) {
            localStorage.setItem("user_id", userId);
        }
        if (body.points !== undefined) {
            localStorage.setItem("points", body.points);
        }
        if (firstName) {
            localStorage.setItem("firstName", firstName);
        }
        if (lastName) {
            localStorage.setItem("lastName", lastName);
        }
        localStorage.setItem("email", email);

        window.location.href = "complete_tasks.html";
    })
    .catch(error => {
        console.error(error);
        alert(error.message);
    });
}