var createController = {
    init: function () {
        //createController.registerEvent();
        //var optionValue = $('.ddlChiNhanh').val();
        //createController.loadddlDMDailyByChiNhanh(optionValue);

        // nhan vien ddl
        //var vpValue = $('.ddlVanPhong').val();
        //createController.loadddlNhanVienByVanPhong(vpValue);
        $('.ddlChiNhanh').off('change').on('change', function () {
            var optionValue = $('.ddlChiNhanh').val();
            $('#hidMaCN').val(optionValue);
            $('#hidSubmit').click();
        });
    }
    //,
    //registerEvent: function () {
    //    $('.ddlChiNhanh').off('change').on('change', function () {
    //        var optionValue = $(this).val();
    //        createController.loadddlDMDailyByChiNhanh(optionValue);

    //    });

    //    // nhan vien ddl
    //    $('.ddlVanPhong').off('change').on('change', function () {
    //        var vpValue = $(this).val();
    //        createController.loadddlNhanVienByVanPhong(vpValue);
    //    });

    //    createController.loadddlNhanVienByVanPhong(vpValue);
    //},
    //loadddlDMDailyByChiNhanh: function (optionValue) {
    //    $('.ddlVanPhong').html('');
    //    var option = '';

    //    $.ajax({
    //        url: '/Users/GetVanPhongByMaCN',
    //        type: 'GET',
    //        data: {
    //            chinhanh: optionValue
    //        },
    //        dataType: 'json',
    //        success: function (response) {
    //            var data = JSON.parse(response.data);

    //            $.each(data, function (i, item) {
    //                option = option + '<option value="' + item.Id + '">' + item.Name + '</option>'; //chinhanh1

    //            });
    //            $('.ddlVanPhong').html(option);
    //        }
    //    });


    //}
    //,
    //loadddlNhanVienByVanPhong: function (vpValue) {
    //    $('.ddlNguoiNhan').html('');
    //    var option = '';

    //    $.ajax({
    //        url: '/Users/GetNhanVienByVanPhong',
    //        type: 'GET',
    //        data: {
    //            vanPhongId: vpValue
    //        },
    //        dataType: 'json',
    //        success: function (response) {
    //            var data = JSON.parse(response.data);
    //            if (data !== false) {
    //                $.each(data, function (i, item) {
    //                    option = option + '<option value="' + item.HoTen + '">' + item.HoTen + '</option>'; //chinhanh1

    //                });
    //                $('.ddlNguoiNhan').html(option);
    //            }

    //        }
    //    });
    //}
};
createController.init();