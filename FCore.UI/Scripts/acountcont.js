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
                var list = data.Families;
                var select = $('#famenum_box');
                select.empty();

                if (list.length > 0) {
                    $.each(list, function (i) {
                        var option = '<option>' + list[i].Text + '</option>';
                        select.append(option);
                    })
                    select.click();

                    var relmem_text = $('#relmem_droptext');
                    relmem_text.prop('disabled', false);
                    var relmem = $('#memenum_box');
                    relmem.prop('disabled', false);
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