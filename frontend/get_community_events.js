function getCommunityEvents() 
{
    fetch("http://10.230.122.4:5001/api/Community/GetCommunityEvents", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            startindex: 0,
            endindex: 10,
            tags: [],
            userid: localStorage.getItem("user_id")
        })
    })
    .then(data => {
        console.log(data);
        return data.json();
    })
    .catch(error => {
        console.error(error);
        alert(error.message);
    });  
}
