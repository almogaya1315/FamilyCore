
//$("#input_file_btn").on("click", function () {
//    $("#input_file").click();
//});

//$("#input_file").select(function () {
//    var path = $("#input_file").val();
//    $("#file_sel_path").text = path;
//});

$(function () {
    $('#back_from_pi').on('click', function () {
        var url = '@Url.Action("LoadProfileImage", "AddChildWizard", Model)'
        $('#wizard_page').load(url)
    })
})