
$(function () {
    $("#Admin, #Create, #Edit, #ManageChat").on('change', function () {
        var admin_val = $("#Admin").is(':checked') ? true : false;
        $("#Admin").prop('val', admin_val);

        var create_val = $("#Create").is(':checked') ? true : false;
        $("#Create").prop('val', create_val);

        var edit_val = $("#Edit").is(':checked') ? true : false;
        $("#Edit").prop('val', edit_val);

        var chat_val = $("#ManageChat").is(':checked') ? true : false;
        $("#ManageChat").prop('val', chat_val);

        $("#perms_form_submit").click();
    })
})