// LoadFamiliesDynamic

$(function () {
    $('#relfam_droptext').on('input', function () {
        var text = $('#relfam_droptext').val();
        if (text >= 2) {
            $.ajax({
                url: '/Acount/LoadFamiliesDynamic',
                type: 'Get',
                data: text,
                cache: false,
                contentType: false,
                processData: false,
                success: function () {
                    var relmem_text = $('#relmem_droptext');
                    relmem_text.prop('disabled', false);
                    var relmem = $('#memenum_box');
                    relmem.prop('disabled', false);

                    var url = '@Url.Action("LoadFamiliesDynamic", "Acount")';
                    $('#famenum_div').load(url);
                }
            })
        }
    })
})

// LoadMembersDynamic

$(function () {
    var relmem = $('#relmem_droptext');
    relmem.on('input', function () {
        var text = relmem.val();
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