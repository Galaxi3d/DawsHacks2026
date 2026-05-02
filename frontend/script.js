// https://www.youtube.com/watch?v=G0jO8kUrg-I

const blankBox = document.getElementById("personal_blank_box");
const listContainer = document.getElementById("personal_list_container");
const communityListContainer = document.getElementById("community_list_container");
function AddTask(){
    if(blankBox.value===''){
        alert("There is no task to check off!")
    }
    else{
        let li=document.createElement("li");
        li.innerHTML=blankBox.value;
        listContainer.appendChild(li);
        // Adds the x to the end of element
        let span = document.createElement("span");
        span.innerHTML="X";
        li.appendChild(span);
        updatePoints(10);
    }
    blankBox.value = '';
    saveData()
}


listContainer.addEventListener("click", function(e){
    if(e.target.tagName==="LI"){
        e.target.classList.toggle("checked"); 
    }
    else if(e.target.tagName==="SPAN"){
        e.target.parentElement.remove();
    }
    saveData()
});

communityListContainer.addEventListener("click", function(e){
    if(e.target.tagName==="LI"){
        e.target.classList.toggle("checked");
    }
});
// This saves the list item as data in the browser's storage (I think)
function saveData(){
    localStorage.setItem("data", listContainer.innerHTML)
}
// This shows the data when you go pack to the site
function showData(){
    listContainer.innerHTML = localStorage.getItem("data")
}
showData()