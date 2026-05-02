

function updatePoints() 
{
    fetch("http://10.230.122.4:5001/api/usercontrollers/UpdateUserPoints", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            user_id: localStorage.getItem("user_id"),
            points: 10
        })
    })
    .then(data => {
        localStorage.setItem("points", parseInt(JSON.stringify(data)));
        console.log(data);
    })
    .catch(error => {
        console.error(error);
        alert(error.message);
    });

}