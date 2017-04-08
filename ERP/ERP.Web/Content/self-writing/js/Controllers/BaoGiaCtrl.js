
app.controller('baogiaCtrl', function ($scope, $http) {

    $scope.kiemtra = function (item) {
        $scope.item = item;
        var cachtinh = $scope.item.CACH_TINH_THANH_TIEN;       
        if (cachtinh == "Giá nhập") {
            $scope.item.THANH_TIEN = (($scope.item.SO_LUONG * $scope.item.DON_GIA) + (($scope.item.SO_LUONG * $scope.item.DON_GIA) * ($scope.item.CHIET_KHAU / 100)));
            $scope.item.TIEN_VAT = (($scope.item.THANH_TIEN * $scope.item.CK_VAT) / 100);
        } else if (cachtinh == "Giá list") {
            $scope.item.THANH_TIEN = (($scope.item.SO_LUONG * $scope.item.DON_GIA) - (($scope.item.SO_LUONG * $scope.item.DON_GIA) * ($scope.item.CHIET_KHAU / 100)));
            $scope.item.TIEN_VAT = (($scope.item.THANH_TIEN * $scope.item.CK_VAT) / 100);
        } else {
            $scope.item.THANH_TIEN = $scope.item.SO_LUONG * $scope.item.DON_GIA;
            $scope.item.TIEN_VAT = (($scope.item.THANH_TIEN * $scope.item.CK_VAT) / 100);
        }        
        var tong_thanh_tien = 0;
        var tong_tien_VAT = 0;
        var tong_tien_chua_van_chuyen = 0;
        var tong_tien_da_tinh_van_chuyen = 0;
            for (var i = 0; i < $scope.Detail.ListAdd.length; i++) {
                tong_thanh_tien = parseFloat($scope.Detail.ListAdd[i].THANH_TIEN + tong_thanh_tien);
                tong_tien_VAT = parseFloat($scope.Detail.ListAdd[i].TIEN_VAT + tong_tien_VAT)
                tong_tien_chua_van_chuyen = parseFloat($scope.Detail.ListAdd[i].THANH_TIEN + $scope.Detail.ListAdd[i].TIEN_VAT + tong_tien_chua_van_chuyen);
            }
            $scope.tong_thanh_tien = tong_thanh_tien;
            $scope.tong_tien_VAT = tong_tien_VAT;
            $scope.tong_tien_chua_van_chuyen = tong_tien_chua_van_chuyen;



            tong_tien_da_tinh_van_chuyen = parseFloat(tong_tien_chua_van_chuyen + $scope.BangBaoGia.phi_van_chuyen);
            $scope.BangBaoGia.tong_tien = tong_tien_da_tinh_van_chuyen;
        
          
    };



    //Mảng chi tiết báo giá
    $scope.Detail = {
        ListAdd: []
    }
    $scope.Detail.ListAdd = [{
        SO_BAO_GIA: '',
        MA_HANG: '',
        SO_LUONG: 0,
        DON_GIA: 0,
        CHIET_KHAU: 0,
        CACH_TINH_THANH_TIEN: '',
        THANH_TIEN: 0,
        CK_VAT: 0,
        TIEN_VAT: 0,
        TINH_TRANG_HANG: '',
        THOI_GIAN_GIAO_HANG: '',
        NGAY_GIAO_HANG: '',
        DIA_DIEM_GIAO_HANG: '',
        GHI_CHU: '',
    }];




    $scope.tongthanhtien = function (item) {
        
    };
    $scope.tongthanhtien();

    var salehienthoi = $('#salehienthoi').val();
    //khai báo thông tin chung
    $scope.BangBaoGia = {
        so_bao_gia: '',
        ngay_bao_gia: '',
        ma_du_kien: '',
        ma_khach_hang: '',
        lien_he_khach_hang: '',
        phuong_thuc_thanh_toan: '',
        han_thanh_toan: '',
        hieu_luc_bao_gia: '',
        dieu_khoan_thanh_toan: '',
        phi_van_chuyen: '',
        tong_tien: '',
        da_duyet: false,
        nguoi_duyet: '',
        da_trung: false,
        da_huy: false,
        ly_do_huy: '',
        sales_bao_gia: '',
        truc_thuoc: '',
    };

    //button add new row
    //$scope.AddNew = function () {
    //    $scope.Detail.ListAdd.push({
    //        SO_BAO_GIA: null,
    //        MA_HANG: null,
    //        SO_LUONG: 0,
    //        DON_GIA: null,
    //        CHIET_KHAU: null,
    //        CACH_TINH_THANH_TIEN: null,
    //        THANH_TIEN: null,
    //        CK_VAT: 0,
    //        TIEN_VAT: null,
    //        TINH_TRANG_HANG: null,
    //        THOI_GIAN_GIAO_HANG: null,
    //        NGAY_GIAO_HANG: null,
    //        DIA_DIEM_GIAO_HANG: null,
    //        GHI_CHU: '',
    //    });
    //}

    //Khai báo đối tượng lưu vào cơ sở dữ liệu-------------------------------------
    $scope.onSave = function () {
        if (!$scope.arrayKhachHang.ma_khach_hang) {
            alert('Thiếu thông tin Mã khách hàng');
            return;
        }

        if (!$scope.arraySaleGiu.username) {
            alert('Thiếu thông tin Sale giữ');
            return;
        }

        if (!$scope.BangBaoGia.ngay_bao_gia) {
            alert('Thiếu thông tin Ngày báo giá');
            return;
        }


        for (var i = 0; i < $scope.Detail.ListAdd.length; i++) {
            if (!$scope.Detail.ListAdd[i].MA_HANG) {
                alert('Thiếu thông tin Mã hàng - tại dòng ' + (i + 1));
                return;
            }

            if (!$scope.Detail.ListAdd[i].SO_LUONG) {
                alert('Thiếu thông tin số lượng giữ - tại dòng ' + (i + 1));
                return;
            }

            if (!$scope.Detail.ListAdd[i].DON_GIA) {
                alert('Thiếu thông tin đơn giá - tại dòng ' + (i + 1));
                return;
            }

        }

        var tong_tien = parseFloat($('#tong_tien').text());
        $scope.Bao_Gia = {
            SALES_BAO_GIA: $scope.arraySaleGiu.username,
            MA_KHACH_HANG: $scope.arrayKhachHang.ma_khach_hang,
            NGAY_BAO_GIA: $scope.BangBaoGia.ngay_bao_gia.format('DD-MM-YYYY'),
            MA_DU_KIEN: $scope.arrayIDDuKien.ma_du_kien,
            LIEN_HE_KHACH_HANG: $scope.arrayLienHe.id_lien_he,
            PHUONG_THUC_THANH_TOAN: $scope.BangBaoGia.phuong_thuc_thanh_toan,
            HAN_THANH_TOAN: $scope.BangBaoGia.han_thanh_toan,
            HIEU_LUC_BAO_GIA: $scope.BangBaoGia.hieu_luc_bao_gia,
            DIEU_KHOAN_THANH_TOAN: $scope.BangBaoGia.dieu_khoan_thanh_toan,
            PHI_VAN_CHUYEN: $scope.BangBaoGia.phi_van_chuyen,
            TONG_TIEN: tong_tien,
            DA_DUYET: false,
            DA_TRUNG: false,
            DA_HUY : false,
            TRUC_THUOC:'HOPLONG'
        };

    $scope.arrayChiTietBaoGia = [];

    for (var i = 0; i < $scope.Detail.ListAdd.length; i++) {
           

        var ChiTietBaoGia = {

            MA_HANG: $scope.Detail.ListAdd[i].MA_HANG,
            SO_LUONG: $scope.Detail.ListAdd[i].SO_LUONG,
            DON_GIA: $scope.Detail.ListAdd[i].DON_GIA,
            CHIET_KHAU: $scope.Detail.ListAdd[i].CHIET_KHAU,
            CACH_TINH_THANH_TIEN: $scope.Detail.ListAdd[i].CACH_TINH_THANH_TIEN,
            THANH_TIEN: $scope.Detail.ListAdd[i].THANH_TIEN,
            CK_VAT: $scope.Detail.ListAdd[i].CK_VAT,
            TIEN_VAT: $scope.Detail.ListAdd[i].TIEN_VAT,
            TINH_TRANG_HANG: $scope.Detail.ListAdd[i].TINH_TRANG_HANG,
            THOI_GIAN_GIAO_HANG: $scope.Detail.ListAdd[i].THOI_GIAN_GIAO_HANG,
            NGAY_GIAO_HANG: $scope.Detail.ListAdd[i].NGAY_GIAO_HANG,
            DIA_DIEM_GIAO_HANG: $scope.Detail.ListAdd[i].DIA_DIEM_GIAO_HANG,
            GHI_CHU: $scope.Detail.ListAdd[i].GHI_CHU,
        }
        //PUSH ChiTietGiu VÀO MẢNG arrayChiTietGiu
        $scope.arrayChiTietBaoGia.push(ChiTietBaoGia);
    }

    //Lưu vào CSDL
          
    $http({
        method: 'POST',
        data: $scope.Bao_Gia,
        url: window.location.origin + '/api/Api_BaoGia'
    }).then(function successCallback(response) {
        $scope.Bao_Gia = response.data;
        if (!$scope.Bao_Gia) {
            alert('Không lưu được thông tin chung của giữ hàng');
            return;
        }
        $scope.Bao_Gia.SO_BAO_GIA;

        for (var i = 0; i < $scope.arrayChiTietBaoGia.length; i++) {
            $scope.arrayChiTietBaoGia[i].SO_BAO_GIA = $scope.Bao_Gia.SO_BAO_GIA;
        }


        if ($scope.arrayChiTietBaoGia.length > 0) {
            $http({
                method: 'POST',
                data: $scope.arrayChiTietBaoGia,
                url: window.location.origin + '/api/Api_ChiTietBaoGia'
            }).then(function successCallback(response) {
                alert("Hoàn Thành Lưu");
            }, function errorCallback(response) {
                alert('Không lưu được chi tiết giữ kho');
            });
            return;
        }
        
    }, function errorCallback(response) {
        console.log(response);
        alert('Sự cố hệ thống, Không lưu được phiếu giữ kho, Bạn vui lòng liên hệ với admin để khắc phục ');
    });
}

    //End Khai báo đối tượng lưu vào cơ sở dữ liệu-----------------------------------



    //Button hủy
    $scope.onHuy = function () {
        window.location.href = window.location.origin + '/GiuHang/Giu_Hang_HL';
    }
    //End button hủy

    //Show thông tin khách hàng---------------------------------------------------------------------------------------------------------------
    $scope.arrayKhachHang = {
        ma_khach_hang: '',
        ten_cong_ty: ''
    };

    //mảng khách hàng
    $scope.arrayKHFinded = [];
    $scope.arrayKH = [];
    $scope.showtable_ma_khach_hang = false;

    //get data khách hàng
<<<<<<< HEAD
=======

>>>>>>> origin/hien
    $http.get(window.location.origin + '/api/Api_KH/GET_KHACH_CUA_SALE/' + salehienthoi)

         .then(function (response) {
             if (response.data) {
                 $scope.arrayKH = response.data;
                 $scope.arrayKHFinded = $scope.arrayKH.map(function (item) {
                     return item;
                 });
             }
         }, function (error) {
             console.log(error);
         });

    //hàm tìm kiếm
    $scope.onKHFind = function () {
        if (!$scope.TEN_CONG_TY) {
            $scope.arrayKHFinded = $scope.arrayKH.map(function (item) {
                return item;
            });
        }
        $scope.arrayKHFinded = $scope.arrayKH.filter(function (item) {
            if (item.TEN_CONG_TY.toLowerCase().indexOf($scope.arrayKhachHang.ten_cong_ty.toLowerCase()) >= 0) {
                return true;
            } else {
                return false;
            }
        });
    }

    // hiển thị danh sách đổi tượng(LẤY THEO MÃ)
    $scope.showInfoKH = function (p_dt) {
        $scope.arrayKhachHang.ma_khach_hang = p_dt.MA_KHACH_HANG;
        $scope.arrayKhachHang.ten_cong_ty = p_dt.TEN_CONG_TY;
        $scope.showtable_ma_khach_hang = false;
    }
    //End Show thông tin khách hàng--------------------------------------------------------------------------------------------

    //Show thông tin don hang du kien---------------------------------------------------------------------------------------------------------------
    $scope.arrayIDDuKien = {
        ma_du_kien: '',
    };

    //mảng don hang du kien
    $scope.arrayDuKienFinded = [];
    $scope.arrayDuKien = [];
    $scope.showtable_DuKien = false;

    //get data don hang du kien
    $http.get(window.location.origin + '/api/Api_BaoGia_DonHangDuKien')
         .then(function (response) {
             if (response.data) {
                 $scope.arrayDuKien = response.data;
                 $scope.arrayDuKienFinded = $scope.arrayDuKien.map(function (item) {
                     return item;
                 });
             }
             
         }, function (error) {
             console.log(error);
         });

    //hàm tìm kiếm
    $scope.onDuKienFind = function () {
        if (!$scope.MA_DU_KIEN) {
            $scope.arrayDuKienFinded = $scope.arrayDuKien.map(function (item) {
                return item;
            });
        }
        $scope.arrayDuKienFinded = $scope.arrayDuKien.filter(function (item) {
            if (item.MA_DU_KIEN.toLowerCase().indexOf($scope.arrayIDDuKien.ma_du_kien.toLowerCase()) >= 0) {
                return true;
            } else {
                return false;
            }
        });
    }

    // hiển thị danh sách ma du kien(LẤY THEO MÃ)
    $scope.showInfoDuKien = function (p_dt) {
        $scope.arrayIDDuKien.ma_du_kien = p_dt.MA_DU_KIEN;
        $scope.arrayIDDuKien.ngay_tao = p_dt.NGAY_TAO;
        $scope.showtable_DuKien = false;
    }
    //End Show thông tin don hang du kien--------------------------------------------------------------------------------------------




    //Show thông tin người giữ----------------------------------------------------------------------------------------------------
    $scope.arraySaleGiu = {
        username: '',
        ho_va_ten: '',
    };

    //mảng nguoi giu
    $scope.arrayNGFinded = [];
    $scope.arrayNG = [];
    $scope.showtable_sale_giu = false;

    //get data nguoi giu
    $http.get(window.location.origin + '/api/Api_KH/GetAllSale')
         .then(function (response) {
             if (response.data) {
                 $scope.arrayNG = response.data;
                 $scope.arrayNGFinded = $scope.arrayNG.map(function (item) {
                     return item;
                 });
             }
         }, function (error) {
             console.log(error);
         });

    //hàm tìm kiếm
    $scope.onSaleGiuFind = function () {
        if (!$scope.HO_VA_TEN) {
            $scope.arrayNGFinded = $scope.arrayNG.map(function (item) {
                return item;
            });
        }
        $scope.arrayNGFinded = $scope.arrayNG.filter(function (item) {
            if (item.HO_VA_TEN.toLowerCase().indexOf($scope.arraySaleGiu.username.toLowerCase()) >= 0) {
                return true;
            } else {
                return false;
            }
        });
    }

    // hiển thị danh sách đổi tượng(LẤY THEO MÃ)
    $scope.showInfoNG = function (p_dt) {
        $scope.arraySaleGiu.username = p_dt.USERNAME;
        $scope.arraySaleGiu.ho_va_ten = p_dt.HO_VA_TEN;
        $scope.showtable_sale_giu = false;
    }
    //End Show thông tin người giữ----------------------------------------------------------------------------------------------------------------------

    

    $scope.run_lienhe = function () {
        //Show thông tin lien he---------------------------------------------------------------------------------------------------------------
        $scope.arrayLienHe = {
            id_lien_he: '',
            nguoi_lien_he: '',
        };

        //mảng nguoi giu
        $scope.arrayLienHeFinded = [];
        $scope.arrayLH = [];
        $scope.showtable_LienHe = false;

        var makh = $scope.arrayKhachHang.ma_khach_hang;
        //get data nguoi giu
        $http.get(window.location.origin + '/api/Api_BaoGia/GetLienHeKhach/' + makh)
             .then(function (response) {
                 if (response.data) {
                     $scope.arrayLH = response.data;
                     $scope.arrayLienHeFinded = $scope.arrayLH.map(function (item) {
                         return item;
                     });
                 }
             }, function (error) {
                 console.log(error);
             });

        //hàm tìm kiếm
        $scope.onLienHeFind = function () {
            if (!$scope.NGUOI_LIEN_HE) {
                $scope.arrayLienHeFinded = $scope.arrayLH.map(function (item) {
                    return item;
                });
            }
            $scope.arrayLienHeFinded = $scope.arrayLH.filter(function (item) {
                if (item.NGUOI_LIEN_HE.toLowerCase().indexOf($scope.arrayLienHe.nguoi_lien_he.toLowerCase()) >= 0) {
                    return true;
                } else {
                    return false;
                }
            });
        }

        // hiển thị danh sách đổi tượng(LẤY THEO MÃ)
        $scope.showInfoLienHe = function (p_dt) {
            $scope.arrayLienHe.id_lien_he = p_dt.ID_LIEN_HE;
            $scope.arrayLienHe.nguoi_lien_he = p_dt.NGUOI_LIEN_HE;
            $scope.showtable_LienHe = false;
        }
        //End Show thông tin lien he--------------------------------------------------------------------------------------------

    };
    
    
    
    //Tìm Kiếm Thông Tin hàng Hóa
    $scope.FindProduct = function (machuan) {
    
        $http({
            method: 'GET',
            data: machuan,
            url: window.location.origin + '/api/Api_TonKhoHL/GetHH_TON_KHO/'+ machuan
        }).then(function successCallback(response) {
            $scope.danhsachhanghoa = response.data;
            
        });
    }


    //button add check
    $scope.check = function (mahang, tenhang, dvt, xuatxu, dongia) {
        $scope.Detail.ListAdd.push({
            MA_HANG: mahang,
            DON_GIA: parseInt(dongia),
            SO_BAO_GIA: null,
            SO_LUONG: 0,
            CHIET_KHAU: 0,
            CACH_TINH_THANH_TIEN: null,
            THANH_TIEN: 0,
            CK_VAT: 0,
            TIEN_VAT: 0,
            TINH_TRANG_HANG: null,
            THOI_GIAN_GIAO_HANG: null,
            NGAY_GIAO_HANG: null,
            DIA_DIEM_GIAO_HANG: null,
            GHI_CHU: '',
        });
    }
        //End Tìm Kiếm Thông Tin hàng Hóa


    $scope.phuongthuctt = ["Chuyển khoản", "Tiền mặt","Trả tiền sau khi nhận hàng"];
    $scope.cachtinhthanhtien = ['Giá nhập','Giá list'];
    $scope.dieukhoantt = ['5 ngày', '7 ngày', '30 ngày', 'Ngày 5 hàng tháng', 'Ngày 15 hàng tháng', 'Ngày 30 hàng tháng'];
});
