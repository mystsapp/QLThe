var indexTestController = {
    init: function () {
        indexTestController.registerEvent();
    },
    registerEvent: function () {
        
        $('#btnExportAll').off('click').on('click', function () {

            indexTestController.exportList();

        });

    },
    exportList: function () {
        var idList = [];
        $.each($('.ckId'), function (i, item) {
            if ($(this).prop('checked')) {
                idList.push({
                    Username: $(item).data('username'),
                    Password: $(item).data('password'),
                    HoTen: $(item).data('hoten')
                });
            }

        });

        console.log(idList);

        if (idList.length !== 0) {
            $('#stringId').val(JSON.stringify(idList));
            $('#frmExportAll').submit();
        }
        else {
            bootbox.alert({
                size: "small",
                title: "Information",
                message: "Bạn chưa chọn bàn giao!",
                callback: function () {
                    //e.preventDefault();

                }
            });
        }



    }

};
indexTestController.init();