if (localStorage.getItem("points") === null) {
    localStorage.setItem("points", 0);
}

function updatePoints(point_increase) 
{
    new_points = parseInt(localStorage.getItem("points")) + point_increase;
    fetch("http://10.230.122.4:5001/api/usercontrollers/UpdateUserPoints", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            user_id: localStorage.getItem("user_id"),
            points: new_points
        })
    })
    .then(data => {
        localStorage.setItem("points", new_points);
        document.getElementById("points").textContent = new_points;
        console.log(data);
    })
    .catch(error => {
        console.error(error);
        alert(error.message);
    });

}