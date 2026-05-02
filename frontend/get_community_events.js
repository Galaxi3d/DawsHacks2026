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
    .then(response => {
        if (!response.ok) {
            throw new Error(`Request failed with status ${response.status}`);
        }
        return response.json();
    })
    .then(data => {
        console.log(data);
    })
    .catch(error => {
        console.error(error);
        alert(error.message);
    });  
}
