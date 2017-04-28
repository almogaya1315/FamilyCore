
//var userabout_btn = document.getElementById("userabout_btn");
//userabout_btn.addEventListener("click", OnLoadEdit);
//var editabout_input = document.getElementById("editabout_input");
//editabout_input.addEventListener("click", OnLoadAbout);

function OnLoadEdit() {

    var userabout_div = document.getElementById("_userabout");
    userabout_div.style.display = "none";
    var editabout_div = document.getElementById("_editabout");
    editabout_div.style.display = "block";
}

function OnLoadAbout() {

    document.getElementById("_editabout").style.display = "none";
    document.getElementById("_userabout").style.display = "normal";
}