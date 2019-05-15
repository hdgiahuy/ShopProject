
var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');           
            $.ajax({
                url: "/Administrator/Oder/thaydoitrangthaiTK",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == "Chờ phê duyệt") {
                        btn.text('Chờ phê duyệt');
                    }
                    else if (response.status == "Chuyển hàng") {
                        btn.text('Chuyển hàng');
                    }
                    else if (response.status == "Đă thanh toán") {
                        btn.text('Đă thanh toán');
                    }
                    else {
                        btn.text('Đă hủy');
                    }
                }              
              
            });
            $.ajax({
                url: "/Administrator/Oder/Index"
            })
           
        });
       

    }
}
user.init();
