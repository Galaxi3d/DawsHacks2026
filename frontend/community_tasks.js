const communityListContainer = document.getElementById("community_list_container");
const list_container = document.getElementById("list_container");


function saveData(){
    localStorage.setItem("data", listContainer.innerHTML)
}

communityListContainer.addEventListener("click", function(e){
    if(e.target.tagName==="LI"){
        e.target.classList.toggle("checked");
        updatePoints(100);
    }
});


