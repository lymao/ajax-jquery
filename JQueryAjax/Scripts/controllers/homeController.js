﻿var homeconfig = {
    pageSize: 4,
    pageIndex: 1,
}
var homeController = {
    init: function () {
        homeController.loadData();
        homeController.registerEvent();
    },
    registerEvent: function () {
        $('.txtSalary').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                var id = $(this).data('id');
                var value = $(this).val();

                homeController.updateSalary(id, value);
            }
        });

        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            homeController.resetForm();
        });

        $('#btnSave').off('click').on('click', function () {
            homeController.saveData();
        });

        $('.btn-edit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            homeController.loadDetail(id);
        });

        $('.btn-delete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.confirm({
                size: "small",
                message: "Are you sure?",
                callback: function (result) {
                    if (result === true)
                        homeController.deleteEmployee(id);
                    /* result is a boolean; true = OK, false = Cancel*/
                }
            })
        });

        $('#btnSearch').off('click').on('click', function () {
            homeController.loadData(true);
        });

        $('#btnReset').off('click').on('click', function () {
            $('#txtSearch').val('');
            $('#ddlStatusS').val('');
            homeController.loadData(true);
        });
        $('#txtSearch').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                homeController.loadData(true);
            }
        });

    },
    deleteEmployee: function (id) {
        $.ajax({
            url: '/Home/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Delete Success", function () {
                        homeController.loadData(true);
                    });
                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadDetail: function (id) {
        $.ajax({
            url: '/Home/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#hidID').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#txtSalary').val(data.Salary);
                    $('#ckStatus').prop('checked', data.Status);
                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    saveData: function () {
        var name = $('#txtName').val();
        var salary = parseFloat($('#txtSalary').val());
        var status = $('#ckStatus').prop('checked');
        var id = parseInt($('#hidID').val());
        var employee = {
            Name: name,
            Salary: salary,
            Status: status,
            ID: id
        }
        $.ajax({
            url: '/Home/SaveData',
            data: {
                strEmployee: JSON.stringify(employee)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    $('#modalAddUpdate').modal('hide');
                    bootbox.alert("Save Success", function () {

                        homeController.loadData(true);
                    });

                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtName').val('');
        $('txtSalary').val(0);
        $('#ckStatus').prop('checked', true);
    },
    updateSalary: function (id, value) {
        var data = {
            ID: id,
            Salary: value
        };
        $.ajax({
            url: '/Home/Update',
            type: 'POST',
            dataType: 'json',
            data: { model: JSON.stringify(data) },
            success: function (response) {
                if (response.status) {
                    bootbox.alert("Update success");
                }
                else {
                    bootbox.alert(response.message);
                }
            }
        })
    },
    loadData: function (changePageSize) {
        var name = $('#txtSearch').val();
        var status = $('#ddlStatusS').val();
        $.ajax({
            url: '/Home/LoadData',
            type: 'GET',
            data: {
                name: name,
                status: status,
                page: homeconfig.pageIndex,
                pageSize: homeconfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Salary: item.Salary,
                            Status: item.Status == true ? "<span class=\"label label-success\">Actived</span>" : "<span class=\"label label-danger\">Locked</span>"
                        });

                    });
                    $('#tblData').html(html);
                    homeController.paging(response.total, function () {
                        homeController.loadData();
                    }, changePageSize);
                    homeController.registerEvent();
                }
            }
        })
    },

    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / homeconfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }
        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                homeconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
homeController.init();