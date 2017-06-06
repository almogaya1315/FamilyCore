// LoadFamiliesDynamic

function LoadFamiliesDynamic() {
    var text = $('#relfam_droptext').val();
    if (text.length >= 2) {
        var boxdata = { "Text": text }
        $.ajax({
            url: '/Acount/PostLoadFamiliesDynamic',
            type: 'Post',
            //dataType: 'json',
            data: boxdata,
            cache: false,
            //contentType: 'application/json; charset=utf-8',
            processData: true,
            success: function (data) {
                var list = data.Families;

                var relmem_text = $('#relmem_droptext');
                relmem_text.prop('disabled', false);
                var relmem = $('#memenum_box');
                relmem.prop('disabled', false);

                var url = '/Acount/GetLoadFamiliesDynamic'; //'@Url.Action("GetLoadFamiliesDynamic", "Acount")';
                $('#famenum_div').load(url);
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