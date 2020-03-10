var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
        $('.tdVal').click(function () {
            id = $(this).data('id');
            $('#hidMaCT').val(id);
            var page = $('.active .page-link').text();
            $('#hidPage').val(page);
            //$.ajax({
            //    url: '/CapThes/Index',
            //    data: {
            //        maCT: id
            //    },
            //    dataType: 'json',
            //    type: 'GET',
            //    success: function (response) {
                    
            //    }
            //});

            $('#btnSubmit').click();
        });
    }
};
indexController.init();