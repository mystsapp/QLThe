var createController = {
    init: function () {
        createController.registerEvent();
        var optionValue = $('.ddlChiNhanh').val();
        createController.loadddlDMDailyByChiNhanh(optionValue);
    },
    registerEvent: function () {
        $('.ddlChiNhanh').off('change').on('change', function () {
            var optionValue = $(this).val();
            createController.loadddlDMDailyByChiNhanh(optionValue);
        });
    },
    loadddlDMDailyByChiNhanh: function (optionValue) {
        $('.ddlVanPhong').html('');
        var option = '';

        $.ajax({
            url: '/Users/GetVanPhongByChiNhanh',
            type: 'GET',
            data: {
                chinhanh: optionValue
            },
            dataType: 'json',
            success: function (response) {
                var data = JSON.parse(response.data);

                $.each(data, function (i, item) {
                    option = option + '<option value="' + item.Name + '">' + item.Name + '</option>'; //chinhanh1

                });
                $('.ddlVanPhong').html(option);
            }
        });
    }
};
createController.init();