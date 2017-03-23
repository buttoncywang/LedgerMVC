﻿$(document).ready(function () {
    $('.page-link').click(function () {
        var currentPageIndex = $(this).text();
        reloadTable(currentPageIndex);
    });

    $('#ChargeDate').datepicker({
        showOtherMonths: true,
        hideIfNoPrevNext: true,
        maxDate: "+0d",
        dateFormat: "yy/mm/dd"
    });

    function reloadTable(pageIndex) {
        /*
            用於日期轉換用，使用Json.net有格式轉換問題
            http://blog.darkthread.net/blogs/darkthreadtw/archive/2013/11/15/10657.aspx#10727
        */
        months = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
        days = [
            '01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12',
            '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24',
            '25', '26', '27', '28', '29', '30', '31'
        ];
        $.ajax({
            type: "GET",
            url: './ShowPagedChargeRecords',
            data: { currentPageIndex: pageIndex },
            error: function (data) {
                alert(data);
            },
            success: function (responseData) {
                $('#ChargeList tbody').empty();
                var tbodyElement = '';
                var chargeList = responseData.ChargeItems;
                var totalPages = responseData.TotalPages;

                for (var i = 0; i < chargeList.length; i++) {
                    var date = new Date(parseInt(chargeList[i]["ChargeDate"].substr(6)));
                    tbodyElement += '<tr><td>' +
                        chargeList[i]["ChargeRecordId"] +
                        '</td>' +
                        '<td>' +
                        chargeList[i]["ChargeType"] +
                        '</td>' +
                        '<td>' +
                        date.getFullYear() +
                        "-" +
                        months[date.getMonth()] +
                        "-" +
                        days[date.getDay()] +
                        '</td>' +
                        '<td>NT$' +
                        (chargeList[i]["ChargePrice"]).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') +
                        '</td>' +
                        '<td><textarea cols="50" rows="5">' +
                        chargeList[i]["Memo"] +
                        '</textarea></td></tr>';
                }
                $('#ChargeList tbody').append(tbodyElement);
                reloadPagination(pageIndex, totalPages);
            }
        });
    }

    function reloadPagination(pageIndex, totalPages) {
        $('#LedgerPagnation').empty();
        var htmlElement = '';
        for (var i = 1; i <= Number(totalPages) ; i++) {
            if (i == Number(pageIndex)) {
                htmlElement += '<li class="page-item active"><a class="page-link">' + i + '</a></li>';
            } else {
                htmlElement += '<li class="page-item"><a class="page-link">' + i + '</a></li>';
            }
        }
        $('#LedgerPagnation').append(htmlElement);
        $('.page-link').click(function () {
            var currentPageIndex = $(this).text();
            reloadTable(currentPageIndex);
        });
    }
});