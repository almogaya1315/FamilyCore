// LoadFamiliesDynamic

function LoadFamiliesDynamic() {
    var text = $('#relfam_droptext').val();
    if (text.length >= 2) {
        var boxdata = { "Text": text }
        $.ajax({
            url: '/Acount/LoadFamiliesDynamic',
            type: 'Post',
            //dataType: 'json',
            data: boxdata,
            cache: false,
            //contentType: 'application/json; charset=utf-8',
            processData: true,
            success: function (data) {
                var fam_list = data.Families;
                var fam_select = $('#famenum_box');
                fam_select.empty();

                if (fam_list.length > 0) {
                    // set relative families
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

                    $('#relmem_droptext').prop('disabled', false);
                    $('#memenum_box').prop('disabled', false);

                    // set relative names
                    var mem_list = data.Members;
                    var mem_select = $('#memenum_box');

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

                //var url = '/Acount/LoadFamiliesDynamic'; 
                //$('#famenum_div').load(url);
            }
        })
    }
}

// LoadMembersDynamic

$(function () {
    $('#relmem_droptext').on('input', function () {
        var text = $('#relmem_droptext').val();
        if (text >= 2) {
            $.ajax({
                url: '/Acount/LoadMembersDynamic',
                type: 'Get',
                data: text,
                cache: false,
                contentType: false,
                processData: false,
                success: function () {
                    var url = '@Url.Action("LoadMembersDynamic", "Acount")';
                    $('#memenum_div').load(url);
                }
            })
        }
    })
})