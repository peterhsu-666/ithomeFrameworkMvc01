$(document).ready(function () {
    $('[name="City"]').change(function () {
        $.ajax({
            url: '/Home/GetVillageList',
            type: 'POST',
            //contentType: 'application/json',
            //dataType: 'json',
            data: {
                id: $('[name="City"]').val(),
            },
            error: function (xhr) {
                alert("error");
            },
            success: function (response) {
                if (response != "") {
                    var data = JSON.parse(response); //轉成array

                    $('[name=Village]').find('option').remove();

                    $('[name="Village"]').append("<option value=''></option>");

                    data.forEach(function (el) {
                        var option = document.createElement('option');
                        option.value = el["VillageId"];
                        option.append(el["VillageName"])
                        $('[name="Village"]').append(option)
                    })
                }
            }
        });
    });
});