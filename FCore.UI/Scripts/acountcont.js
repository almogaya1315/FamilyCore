﻿// LoadFamiliesDynamic

// listener to #relfam_droptext 'oninput'
function LoadFamiliesDynamic() {
    var text = $('#relfam_droptext').val();
    if (text.length >= 2) {
        var boxdata = { "Text": text }
        $.ajax({
            url: '/Acount/LoadFamiliesDynamic',
            type: 'Post',
            data: boxdata,
            cache: false,
            processData: true,
            success: function (data) {
                SetRelativeFamilies(data);
                ResetRelativeNames();
            }
        })
    }
}

function SetRelativeFamilies(data) {
    var fam_list = data.Families;
    var fam_select = $('#famenum_box');
    fam_select.empty();

    var fam_option;
    $.each(fam_list, function (i) {
        if (fam_list[i].Value == 'ph') {
            fam_option = '<option hidden>' + fam_list[i].Text + '</option>';
        }
        else {
            fam_option = '<option>' + fam_list[i].Text + '</option>';
        }
        fam_select.append(fam_option);
    })
}

// LoadMembersDynamic

// listener to #famenum_box 'onchange'
function FamilyListOnChange() {
    var familyName = $('#famenum_box option:selected').text();
    var data = { "FamilyName": familyName };

    $('#relmem_droptext').prop('disabled', false);
    $('#memenum_box').prop('disabled', false);
    $('#relfam_droptext').val('');

    DynamicMemberAjaxRequest(data);
}

// listener to #relmem_droptext 'oninput'
function LoadMembersDynamic() {
    var text = $('#relmem_droptext').val();
    if (text.length >= 2) {
        var familyName = $('#famenum_box option:selected').text();
        var data = { "FamilyName": familyName, "Text": text };
        DynamicMemberAjaxRequest(data);
    }
}


function DynamicMemberAjaxRequest(data) {
    $.ajax({
        url: '/Acount/LoadMembersDynamic',
        type: 'Post',
        data: data,
        cache: false,
        processData: true,
        success: function (data) {
            SetRelativeNames(data);
        }
    })
}

function SetRelativeNames(data) {
    var mem_list = data.Members;
    var mem_select = $('#memenum_box');
    mem_select.empty();

    var mem_option;
    $.each(mem_list, function (i) {
        if (mem_list[i].Value == 'ph') {
            mem_option = '<option hidden>' + mem_list[i].Text + '</option>';
        }
        else {
            mem_option = '<option>' + mem_list[i].Text + '</option>';
        }
        mem_select.append(mem_option);
    })
}

function ResetRelativeNames() {
    $('#relmem_droptext').val('');
    $('#relmem_droptext').prop('disabled', true);

    $('#memenum_box').empty();
    var placeholder = '<option disabled selected hidden>Choose family first</option>';
    $('#memenum_box').append(placeholder);
    $('#memenum_box').prop('disabled', false);
}

// listener to #memenum_box 'onchange'
function AddRelativeName() {
    $('#relmem_droptext').val('');
    var relativeName = $('#memenum_box option:selected').text();
    var data = { "FirstName": relativeName };
    $.ajax({
        url: '/Acount/AddRelative',
        type: 'Post',
        data: data,
        processData: true
    })
}

// submit 'choose user' form
function SubmitChooseUserForm(submitId) {
    $(submitId).click();
}


