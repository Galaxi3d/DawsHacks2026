

function updatePoints(point_increase) 
{
    fetch("http://10.230.122.4:5001/api/usercontrollers/UpdateUserPoints", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            user_id: localStorage.getItem("user_id"),
            points: parseInt(localStorage.getItem("points")) + point_increase
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

    let points = localStorage.getItem("points");
    document.getElementById("points").textContent = points;

}