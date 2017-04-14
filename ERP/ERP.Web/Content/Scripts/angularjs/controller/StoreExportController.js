﻿
app.controller('StoreExportController', function ($rootScope, $scope, $http, config) {
    $rootScope.title = "Xuất kho";
    $rootScope.dashboard = false;
    $scope.StoreType = 1;
    $('.datetimepicker').daterangepicker({
        singleDatePicker: true,
        calender_style: "picker_2"
    }, function (start, end, label) {
        $(this).val($(this).val);
    });
    //$scope.numPerPage = angular.copy($rootScope.PageSetting.NumberPerPage);
    //$scope.currentPage = angular.copy($rootScope.PageSetting.CurrentPage);
    $scope.NhanVien = [];
    $scope.GiaTriThamChieu = [];
    $scope.LoaiChungTu = null;
    $scope.GeneralInfo = {
        NgayChungTu: null,
        NgayHachToan: null,
        SoChungTu: null,
        DienGiai: null,
        KemTheo: null,
        ChiTiet: [],
        KhachHang: null,
        NguoiNhan: null,
        DiaChi: null,
        NhanVienBanHang: null
    };
    $scope.ValidateGeneral = {
        NhanVienBanHang: true,
        NgayHachToan: true,
        NgayChungTu: true,
        NgayHachToanLess: true,
    }
    $scope.Validate = {
        FromDateThamChieu: true,
        ToDateThamChieu: true,
        ToDateThamChieuLess: true,
        LoaiChungTu: true,
        GiaTriChungTu: true

    };
    $scope.GiaTriChungTu = {
        Search: null,
        Date: null
    };
    $scope.ThamChieu = {
        From: null,
        To: null,
        ListResult: [],
        ListSelect: [],
        TraHang: null
    };



    $scope.KhachHang = {
        KhachHang: [],
        DoiTuong: [],
    };
    $scope.Detail = {
        ListHangHoa: [],
        ListTaiKhoan: [],
        ListAdd: [],
        SearchHang: [],
        //ListKho: []
    }


    //Init all data
    function Init() {

        $http({
            method: 'GET',
            url: '/api/Api_NhanvienHL/GetListNhanvien'
        }).then(function (response) {
            if (typeof (response.data) == "object") {
                $scope.NhanVien = response.data;
            }
            else {
                ErrorSystem();
            }
        }, function (error) {
            ConnectFail();
        });


        $http({
            method: 'GET',
            url: '/api/Api_KH'
        }).then(function (response) {
            if (typeof (response.data) == "object") {
                $scope.KhachHang.KhachHang = response.data;
            }
            else {
                ErrorSystem();
            }
        }, function (error) {
            ConnectFail();
        });

        //$http({
        //    method: 'GET',
        //    url: '/HangHoa/GetAllWithTon'
        //}).then(function (response) {
        //    if (typeof (response.data) == "object") {
        //        $scope.Detail.ListHangHoa = response.data;
        //    }
        //    else {
        //        ErrorSystem();
        //    }
        //}, function (error) {
        //    ConnectFail();
        //});

        //$http({
        //    method: 'GET',
        //    url: '/Kho/GetAll'
        //}).then(function (response) {
        //    if (typeof (response.data) == "object") {
        //        $scope.Detail.ListKho = response.data;
        //    }
        //    else {
        //        ErrorSystem();
        //    }
        //}, function (error) {
        //    ConnectFail();
        //});

        $http({
            method: 'GET',
            url: '/api/Api_TaiKhoanHachToan'
        }).then(function (response) {
            if (typeof (response.data) == "object") {
                $scope.Detail.ListTaiKhoan = response.data;
            }
            else {
                ErrorSystem();
            }
        }, function (error) {
            ConnectFail();
        });


        //$http({
        //    method: 'GET',
        //    url: '/DoiTuong/GetAllCongTy'
        //}).then(function (response) {
        //    if (typeof (response.data) == "object") {
        //        $scope.KhachHang.DoiTuong = response.data;
        //        console.log($scope.KhachHang.DoiTuong);
        //    }
        //    else {
        //        ErrorSystem();
        //    }
        //}, function (error) {
        //    ConnectFail();
        //});


    }
    
    Init();
    //End Init all data


    //Hiển thị khách hàng khi click
    $scope.ShowKhachHang = function () {
        if ($("#KhachHang").css("display") == "none") {
            $(".tableselect").css({ "display": "none" });
            $("#KhachHang").css({ "display": "block" });
        }
        else {
            $(".tableselect").css({ "display": "none" });
        }
    }
    //end Hiển thị khách hàng khi click



    // lựa chọn khách hàng khi click
    $scope.SelectKhachHang = function (item) {
        $scope.GeneralInfo.KhachHang = item.MA_KHACH_HANG;
        $scope.GeneralInfo.TenDoiTuong = item.TEN_CONG_TY;
        $(".tableselect").css({ "display": "none" });
    }
    //End lựa chọn KH


    //Hiển thị ô giá trị chứng từ
    $scope.ShowDataGiaTriChungTu = function () {
        if ($scope.LoaiChungTu == 2 && $("#DataGiaTriChungTu").css("display") == "none") {
            $("#DataGiaTriChungTu").css({ "display": "block" });
        }
        else {
            $("#DataGiaTriChungTu").css({ "display": "none" });
        }
    }
    //End 



    //Chọn giá trị chứng từ
    $scope.SelectDataGiaTriChungTu = function (item) {
        $scope.GiaTriChungTu.Data = item;
        $scope.GiaTriChungTu.Search = item.tendoituong;
        $("#DataGiaTriChungTu").css({ "display": "none" });
    }
    //end


    //Thay đổi loại chứng từ
    $scope.ChangeLoaiChungTu = function () {

        if ($scope.LoaiChungTu == 1) {
            $("#Select_DataGiaTriChungTu").css({ "display": "block" });
            $("#Input_DataGiaTriChungTu").css({ "display": "none" });
            $("#Input_MaChungTu").css({ "display": "none" });
            $("#DataGiaTriChungTu").css({ "display": "none" });
            $http({
                method: 'GET',
                url: '/api/Api_Loaichungtu'
            }).then(function (response) {
                if (typeof (response.data) == "object") {
                    $scope.GiaTriThamChieu = [];
                    for (var i = 0; i < response.data.length; i++) {
                        $scope.GiaTriThamChieu.push({
                            "value": response.data[i].MA_LOAI_CHUNG_TU,
                            "show": response.data[i].TEN_LOAI_CHUNG_TU
                        });
                    }
                }
                else {
                    ErrorSystem();
                }
            }, function (error) {
                ConnectFail();
            });
        }
        else if ($scope.LoaiChungTu == 2) {
            $("#Select_DataGiaTriChungTu").css({ "display": "none" });
            $("#Input_DataGiaTriChungTu").css({ "display": "block" });
            $("#Input_MaChungTu").css({ "display": "none" });
            $("#DataGiaTriChungTu").css({ "display": "block" });
            $http({
                method: 'GET',
                url: '/api/Api_XuatNhapKho/GetAllDoiTuong'
            }).then(function (response) {
                if (typeof (response.data) == "object") {
                    var data = response.data.DoiTuong;
                    var colength = 5;
                    var madoituong = "", tendoituong = "";
                    var max = 0;
                    var maxlength = response.data.Length;
                    for (var i = 0; i < response.data.length; i++) {
                        madoituong = response.data[i].MA_DOI_TUONG;
                        tendoituong = response.data[i].TEN_DOI_TUONG;
                        $scope.GiaTriThamChieu.push({
                            value: response.data[i].MA_DOI_TUONG,
                            show: "",
                            madoituong: madoituong,
                            tendoituong: tendoituong,
                        });
                    }
                }
                else {
                    ErrorSystem();
                }
            }, function (error) {
                ConnectFail();
            });
        }
        else if ($scope.LoaiChungTu == 3) {
            $("#Select_DataGiaTriChungTu").css({ "display": "none" });
            $("#Input_DataGiaTriChungTu").css({ "display": "none" });
            $("#Input_MaChungTu").css({ "display": "block" });
            $("#DataGiaTriChungTu").css({ "display": "none" });
            //$http({
            //    method: 'POST',
            //    url: '/api/Api_XuatNhapKho/SearchAllMa/A/A'
            //}).then(function (response) {
            //    if (typeof (response.data) == "object") {
            //        $scope.GiaTriThamChieu = [];
            //        for (var i = 0; i < response.data.length; i++) {
            //            $scope.GiaTriThamChieu.push({
            //                "value": response.data[i].SO_CHUNG_TU,
            //                "show": response.data[i].SO_CHUNG_TU
            //            });
            //        }
            //    }
            //    else {
            //        ErrorSystem();
            //    }
            //}, function (error) {
            //    ConnectFail();
            //});
        }
    };
    //End

    $scope.SearchThamChieu = function () {
        if (CheckSearchThamChieu() == false) {
            return;
        }
        
        if ($scope.LoaiChungTu == 1) {
            var data = {
                GiaTriChungTu: $scope.GiaTriLoaiChungTu,
                FromTime: $scope.ThamChieu.From,
                ToTime: $scope.ThamChieu.To
                
            }
         
                $http.post('/api/Api_XuatNhapKho/SearchByTypeWithDate',data)
                .then(function (response) {
                    console.log(response);
                    if (typeof (response.data) == "object") {
                        $scope.ThamChieu.ListResult = response.data;
                        if ($scope.ThamChieu.ListResult.length == 0) {
                            Norecord();
                        }
                    }
                    else {
                        ErrorSystem();
                    }
                }, function (error) {
                    ConnectFail();
                });

            
           


            //$http({
            //    method: 'POST',
            //    url: '/api/Api_XuatNhapKho/SearchByType/' + GiaTriChungTu + '/' + FromTime + '/' + ToTime,
            //    data: { FromTime: $scope.ThamChieu.From, ToTime: $scope.ThamChieu.To, GiaTriChungTu: $scope.GiaTriLoaiChungTu }
            //}).then(function (response) {
            //    console.log(response);
            //    if (typeof (response.data) == "object") {
            //        $scope.ThamChieu.ListResult = response.data;
            //        if ($scope.ThamChieu.ListResult.length == 0) {
            //            Norecord();
            //        }
            //    }
            //    else {
            //        ErrorSystem();
            //    }
            //}, function (error) {
            //    ConnectFail();
            //});
        }
        else if ($scope.LoaiChungTu == 2) {
            var data = {
                GiaTriChungTu: $scope.GiaTriChungTu.Data.madoituong,
                FromTime: $scope.ThamChieu.From,
                ToTime: $scope.ThamChieu.To

            }

            $http.post('/api/Api_XuatNhapKho/SearchByDoiTuongWithDate', data)
            .then(function (response) {
                console.log(response);
                if (typeof (response.data) == "object") {
                    $scope.ThamChieu.ListResult = response.data;
                    if ($scope.ThamChieu.ListResult.length == 0) {
                        Norecord();
                    }
                }
                else {
                    ErrorSystem();
                }
            }, function (error) {
                ConnectFail();
            });
        }
        else {
            var mact = $scope.MaChungTu.Search;
            $http.get('/api/Api_XuatNhapKho/GetbyMa/' + mact)
            .then(function (response) {
                console.log(response);
                if (typeof (response.data) == "object") {
                    $scope.ThamChieu.ListResult = response.data;
                    if ($scope.ThamChieu.ListResult.length == 0) {
                        Norecord();
                    }
                }
                else {
                    ErrorSystem();
                }
            }, function (error) {
                ConnectFail();
            });
        }
    };
    function CheckSearchThamChieu() {
        $scope.ThamChieu.From = $("#ThamChieuFrom").val();
        $scope.ThamChieu.To = $("#ThamChieuTo").val();
        var check = true;
        if ($scope.LoaiChungTu == null) {
            $scope.Validate.LoaiChungTu = false;
            check = false;
        }
        else {
            $scope.Validate.LoaiChungTu = true;
        }
        if (($scope.LoaiChungTu == 2 && $scope.GiaTriChungTu.Data == null) || ($scope.LoaiChungTu == 1 && $scope.GiaTriLoaiChungTu == null) || ($scope.LoaiChungTu == 3 && $scope.MaChungTu.Search == null)) {
            $scope.Validate.GiaTriChungTu = false;
            check = false;
        }
        else {
            $scope.Validate.GiaTriChungTu = true;
        }
        if ($scope.ThamChieu.From != "" && $scope.ThamChieu.To != "" && ConvertToDate($scope.ThamChieu.From) > ConvertToDate($scope.ThamChieu.To)) {
            $scope.Validate.ToDateThamChieuLess = false;
            check = false;
        }
        else {
            $scope.Validate.ToDateThamChieuLess = true;
        }
        return check;
    };
    $scope.SelectThamChieu = function (item, index) {
        if (item.Action == true) {
            item.Action = false;
        }
        else {
            item.Action = true;

        }
    };
    $scope.RemoveThamChieu = function (index) {
        $scope.ThamChieu.ListSelect.splice(index, 1);
        if ($scope.LoadHangTra == true) {
            ResetAfterSave();
        }
    }
    $scope.SetThamChieu = function () {
        var length = $scope.ThamChieu.ListResult.length;
        //$scope.ThamChieu.ListSelect = [];
        var check = false;
        for (var i = 0; i < length; i++) {
            if ($scope.ThamChieu.ListResult[i].Action == true) {
                check = false;
                for (var j = 0; j < $scope.ThamChieu.ListSelect.length; j++) {
                    if ($scope.ThamChieu.ListSelect[j].SO_CHUNG_TU == $scope.ThamChieu.ListResult[i].SO_CHUNG_TU) {
                        check = true;
                        break;
                    }
                }
                if (!check) {

                    $scope.ThamChieu.ListSelect.push(angular.copy($scope.ThamChieu.ListResult[i]));
                }
            }
        }
        $("#modal_theme_primary").modal("toggle");
        ResetThamChieu();
    };
    function ResetThamChieu() {
        $("#ThamChieuFrom").val("");
        $("#ThamChieuTo").val("");
        $scope.ThamChieu.ListResult = [];
    };
    $scope.ShowDataNhanVien = function () {
        if ($("#DataNhanVien").css("display") == "none") {
            $(".tableselect").css({ "display": "none" });
            $("#DataNhanVien").css({ "display": "block" });
        }
        else {
            $(".tableselect").css({ "display": "none" });
        }
    }
    $scope.SelectDataNhanVien = function (item) {
        $scope.GeneralInfo.NhanVienBanHang = item.USERNAME;
    }
    $scope.AddNew = function () {
        $scope.Detail.ListAdd.push({
            MaHang: null,
            TenHang: null,
            TKKho: null,
            DonGia: null,
            SoLuong: null,
            DVT: null,
            TKNo: null,
            TKCo: null,
            DonGiaVon: null
        });
    }
    $scope.ShowHangHoa = function (index) {
        if ($("#DataHangHoa" + index).css("display") == "none") {
            $(".tableselect").css({ "display": "none" });
            $("#DataHangHoa" + index).css({ "display": "block" });
        }
        else {
            $(".tableselect").css({ "display": "none" });
        }
    }
    $scope.SelectHangHoa = function (index, childIndex) {
        $scope.Detail.ListAdd[index].MaHang = $scope.Detail.ListHangHoa[childIndex].MA_HANG;
        $scope.Detail.ListAdd[index].TenHang = $scope.Detail.ListHangHoa[childIndex].TEN_HANG;
        $scope.Detail.ListAdd[index].SearchHang = $scope.Detail.ListHangHoa[childIndex].MA_HANG;
        $scope.Detail.ListAdd[index].KhoList = $scope.Detail.ListHangHoa[childIndex].KHO;
        $(".tableselect").css({ "display": "none" });
    };

    $scope.ShowKho = function (index) {
        if ($("#DataKho" + index).css("display") == "none") {
            $(".tableselect").css({ "display": "none" });
            $("#DataKho" + index).css({ "display": "block" });
        }
        else {
            $(".tableselect").css({ "display": "none" });
        }
    };
    $scope.SelectKho = function (index, item, kho) {
        item.Kho = kho.MA_KHO;
        $(".tableselect").css({ "display": "none" });
    };

    $scope.ShowTaiKhoanCo = function (index) {
        if ($("#DataTaiKhoanCo" + index).css("display") == "none") {
            $(".tableselect").css({ "display": "none" });
            $("#DataTaiKhoanCo" + index).css({ "display": "block" });
        }
        else {
            $(".tableselect").css({ "display": "none" });
        }
    };
    $scope.SelectTKCo = function (index, tkindex) {
        $scope.Detail.ListAdd[index].TKCo = $scope.Detail.ListTaiKhoan[tkindex].SO_TK;
        $(".tableselect").css({ "display": "none" });
    };
    $scope.ShowTaiKhoanNo = function (index) {
        if ($("#DataTaiKhoanNo" + index).css("display") == "none") {
            $(".tableselect").css({ "display": "none" });
            $("#DataTaiKhoanNo" + index).css({ "display": "block" });
        }
        else {
            $(".tableselect").css({ "display": "none" });
        }
    };
    $scope.SelectTKNo = function (index, tkindex) {
        $scope.Detail.ListAdd[index].TKNo = $scope.Detail.ListTaiKhoan[tkindex].SO_TK;
        $("#DataTaiKhoanNo" + index).css({ "display": "none" });
    };

    $scope.ShowTaiKhoanKho = function (index) {
        if ($("#DataTaiKhoanKho" + index).css("display") == "none") {
            $(".tableselect").css({ "display": "none" });
            $("#DataTaiKhoanKho" + index).css({ "display": "block" });
        }
        else {
            $(".tableselect").css({ "display": "none" });
        }
    };
    $scope.SelectTKKho = function (index, tkindex) {
        $scope.Detail.ListAdd[index].TKKho = $scope.Detail.ListTaiKhoan[tkindex].SO_TK;
        $(".tableselect").css({ "display": "none" });
    };
    function ResetAfterSave() {
        $scope.GiaTriThamChieu = [];
        //$scope.numPerPage = angular.copy($rootScope.PageSetting.NumberPerPage);
        //$scope.currentPage = angular.copy($rootScope.PageSetting.CurrentPage);
        $scope.GiaTriChungTu.Search = null,
        $scope.GiaTriChungTu.Date = null;
        $scope.GeneralInfo.NgayChungTu = null;
        $scope.GeneralInfo.NgayHachToan = null;
        $("#GeneralInfo_NgayHachToan").val("");
        $("#GeneralInfo_NgayChungTu").val("");
        $scope.GeneralInfo.SoChungTu = null;
        $scope.GeneralInfo.DienGiai = null;
        $scope.GeneralInfo.KemTheo = null;
        $scope.GeneralInfo.ChiTiet = [];
        $scope.Detail.ListAdd = [];
        $scope.Detail.SearchHang = [];
        $scope.ValidateGeneral.NgayHachToan = true;
        $scope.ValidateGeneral.NgayChungTu = true;
        $scope.ValidateGeneral.NgayHachToanLess = true;
        $scope.Validate.FromDateThamChieu = true;
        $scope.Validate.ToDateThamChieu = true;
        $scope.Validate.ToDateThamChieuLess = true;
        $scope.Validate.LoaiChungTu = true;
        $scope.Validate.GiaTriChungTu = true;
        $scope.ThamChieu.From = null;
        $scope.ThamChieu.To = null;
        $scope.ThamChieu.ListResult = [];
        $scope.ThamChieu.ListSelect = [];
        $scope.GeneralInfo.NguoiNhan = null;
        $scope.GeneralInfo.DiaChi = null;
        $scope.GeneralInfo.KhachHang = null;
        $scope.GeneralInfo.TenDoiTuong = null;
        $scope.GeneralInfo.NhanVienBanHang = null;
    }
    function CheckAll() {
        var check = true;
        $scope.GeneralInfo.NgayChungTu = $("#GeneralInfo_NgayChungTu").val();
        $scope.GeneralInfo.NgayHachToan = $("#GeneralInfo_NgayHachToan").val();
        if ($scope.GeneralInfo.NhanVienBanHang == null && $scope.StoreType==1) {
            $scope.ValidateGeneral.NhanVienBanHang = false;
            check = false;
        } else {
            $scope.ValidateGeneral.NhanVienBanHang = true;
        }
        if ($scope.GeneralInfo.NgayChungTu == null || $scope.GeneralInfo.NgayChungTu == "") {
            $scope.ValidateGeneral.NgayChungTu = false;
            check = false;
        } else {
            $scope.ValidateGeneral.NgayChungTu = true;
        }
        if ($scope.GeneralInfo.NgayHachToan == null || $scope.GeneralInfo.NgayHachToan == "") {
            $scope.ValidateGeneral.NgayHachToan = false;
            check = false;
        }
        else {
            $scope.ValidateGeneral.NgayHachToan = true;
        }
        if (ConvertToDate($scope.GeneralInfo.NgayHachToan) < ConvertToDate($scope.GeneralInfo.NgayChungTu)) {
            $scope.ValidateGeneral.NgayHachToanLess = false;
            check = false;
        }
        else {
            $scope.ValidateGeneral.NgayHachToanLess = true;
        }
        return check;
    }
    var a = $('#username').val();
    var b = $('#macongty').val();
    $scope.SaveXuatKho = function () {
        if (CheckAll() == false) {
            return;
        }
        var loaixuatkho = "";
        if ($scope.StoreType == 1) {
            loaixuatkho = "Bán hàng";
        } else {
            loaixuatkho = "Sản xuất";
            $scope.GeneralInfo.KhachHang = null;
        }
        $http({
            method: 'POST',
            url: '/api/Api_XuatKho/PostKHO_XUAT_KHO',
            data: {
                SO_CHUNG_TU: $scope.GeneralInfo.SoChungTu,
                NGAY_CHUNG_TU: $scope.GeneralInfo.NgayChungTu,
                NGAY_HACH_TOAN: $scope.GeneralInfo.NgayHachToan,
                ChiTiet: $scope.Detail.ListAdd,
                ThamChieu: $scope.ThamChieu.ListSelect,
                NGUOI_GIAO_HANG: $scope.GeneralInfo.NguoiGiaoHang,
                LOAI_XUAT_KHO: loaixuatkho,
                KHACH_HANG: $scope.GeneralInfo.KhachHang,
                NHAN_VIEN_BAN_HANG: $scope.GeneralInfo.NhanVienBanHang,
                LY_DO_XUAT: $scope.GeneralInfo.DienGiai,
                NGUOi_NHAN: $scope.GeneralInfo.NguoiNhan,
                NGUOi_LAP_PHIEU: a,
                TRUC_THUOC: b,


            }
        }).then(function (response) {
            response.data = jQuery.parseJSON(response.data);
            if (response.data == config.INPUT_ERROR) {
                InputFail();
            }
            else if (response.data == config.FAIL) {
                ErrorSystem();
            }
            else {
                ResetAfterSave();
                new PNotify({
                    title: 'Thành công',
                    text: 'Chứng từ ' + response.data + ' đã được tạo',
                    addclass: 'bg-primary'
                });
            }
        }, function (error) {
            ConnectFail();
        });
    }
    $scope.ShowDoiTuong = function () {
        if ($("#DoiTuong").css("display") == "none") {
            $(".tableselect").css({ "display": "none" });
            $("#DoiTuong").css({ "display": "block" });
        }
        else {
            $(".tableselect").css({ "display": "none" });
        }
    };
    $scope.SelectDoiTuong = function (item) {
        $scope.GeneralInfo.NguoiNhan = item.MA_DOI_TUONG;
        $scope.GeneralInfo.TenDoiTuong = item.TEN_DOI_TUONG;
    }
    $scope.ChangeType=function()
    {

    }



    //Tìm Kiếm Thông Tin hàng Hóa
    $scope.FindProduct = function (machuan) {

        $http({
            method: 'GET',
            data: machuan,
            url: window.location.origin + '/api/Api_TonKhoHL/GetHH_TON_KHO/' + machuan
        }).then(function successCallback(response) {
            $scope.danhsachhanghoa = response.data;

        });
    }


    //button add check
    $scope.check = function (mahang, tenhang) {
        $scope.Detail.ListAdd.push({
            MaHang: mahang,
            TenHang: tenhang,
        });
    }
    //End Tìm Kiếm Thông Tin hàng Hóa


});