
$("#input_file_btn").on("click", function () {
    $("#input_file").click();
});

$("#input_file").on("change", function () {
    var path = $("#input_file").val();
    $("#file_sel_path").text = path;
});