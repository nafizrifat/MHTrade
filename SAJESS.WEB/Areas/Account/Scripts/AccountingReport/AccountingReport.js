$(document).ready(function () {
    AccountingReportHelper.ReportInit();
});
var AccountingReportManager = {

    TrialBalanceReport: function () {
       // var date = $("#rptdate").datepicker('getDate');
        var data = { 'date': $("#trialbalanceDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14) };
        //var datedata = { date: date.toISOString() }
        $.ajax({
            type: "GET",
            url: "/Account/AccountingReport/TrialBalanceData",
            //data: JSON.stringify({ "date": date }),
            data: data,
            dataType: "json",
            contentType: "application/json;charset:utf-8",
            success: function (response) {
               if (response) {
                    window.open("../AccountingReport/TrialBalanceReport");
                }
                else {

                    $.bigBox({
                        title: "No Data Found",
                        content: "There is no record for the given date",
                        color: "#C46A69",
                        //timeout: 6000,
                        icon: "fa fa-warning shake animated",
                        number: "1",
                        timeout: 3000
                    });

                  
                }

            },
            error: function (response) {

            }

        });
    },
    GLDateWiseTransactionReport: function () {
        debugger;
       
        var id = $("#cmbGLTACHeads").val();
        var fromDate = $("#GLTfromDate").datepicker('getDate');
        var toDate = $("#GLTtoDate").datepicker('getDate');
        //var datedata = { date: date.toISOString() }
        var data = { id: id, 'startDate': $("#GLTfromDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14), 'endDate': $("#GLTtoDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14) };
        $.ajax({
            type: "GET",
            url: "/Account/AccountingReport/GlDateWiseTransactionData",
            //data: JSON.stringify({ "date": date }),
            //data: { id: id, "fromDate": fromDate, "toDate": toDate },
           // data: { id: id, fromDate: fromDate, toDate: toDate },
            data: data,
            dataType: "json",
            contentType: "application/json;charset:utf-8",
            success: function (response) {
                if (response) {
                    window.open("../AccountingReport/GlDateWiseTransactionReport");
                }
                else {

                    $.bigBox({
                        title: "No Data Found",
                        content: "There is no record for the given date",
                        color: "#C46A69",
                        //timeout: 6000,
                        icon: "fa fa-warning shake animated",
                        number: "1",
                        timeout: 3000
                    });

                  
                }

            },
            error: function (response) {

            }

        });
    },
    DateWiseTransactionReport: function () {
        debugger;
        var data = { 'startDate': $("#DwTfromDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14), 'endDate': $("#DwTtoDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14) };
        $.ajax({
            type: "GET",
            url: "/Account/AccountingReport/DateWiseTransactionData",
            data: data,
            dataType: "json",
            contentType: "application/json;charset:utf-8",
            success: function (response) {

                if (response) {
                    window.open("../AccountingReport/DateWiseTransactionReport");
                }
                else {

                    $.bigBox({
                        title: "No Data Found",
                        content: "There is no record for the given date",
                        color: "#C46A69",
                        //timeout: 6000,
                        icon: "fa fa-warning shake animated",
                        number: "1",
                        timeout: 3000
                    });

                    
                }

            },
            error: function (response) {

            }

        });
    },
    LoadHead: function () {
        debugger;
        var b = [];
        $.ajax({
            type: "GET",
            url: "/Account/Journal/SecondA_GlComboLoad",
            dataType: "json",
            cache: true,
            async: false,
            contentType: "application/json;charset:utf-8",
            success: function (response, textStatus) {
                b = response.data;
            },
            error: function (textStatus, errorThrown) {
                b = { id: 0, text: "No Data" }
            }

        });
        return b;
    },
    AccountingTopSheet: function () {
        var data = {
            'startDate': $("#startDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14),
            'endDate': $("#txtEndDateDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14)
        };
             $.ajax({
            type: "GET",
            url: "Account/AccountingReport/AccountingTopSheet",
            data: data,
            dataType: "json",
            contentType: "application/json;charset:utf-8",
            success: function (response) {

                if (response) {
                    window.open("../Account/AccountingReport/AccountingTopSheetReport");
                }
                else {

                    $.bigBox({
                        title: "No Data Found",
                        content: "There is no record for the given date",
                        color: "#C46A69",
                        //timeout: 6000,
                        icon: "fa fa-warning shake animated",
                        number: "1",
                        timeout: 3000
                    });

                   
                }

            },
            error: function (response) {

            }

        });
    },
    ShowPaymentSheet: function () {
        debugger;
        var data = {
            'startDate': $("#payFromDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14),
            'endDate': $("#payToDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14)
        };
        $.ajax({
            type: "GET",
            url: "Account/AccountingReport/PaymentSheet",
            data: data,
            dataType: "json",
            contentType: "application/json;charset:utf-8",
            success: function (response) {
                if (response) {
                    window.open("../Account/AccountingReport/PaymentSheetReport");
                }
                else {
                    $.bigBox({
                        title: "No Data Found",
                        content: "There is no record for the given date",
                        color: "#C46A69",
                        //timeout: 6000,
                        icon: "fa fa-warning shake animated",
                        number: "1",
                        timeout: 3000
                    });

                    
                }

            },
            error: function (response) {

            }

        });
        

    }
}

var AccountingReportHelper = {
    
    ReportInit: function () {
    debugger;
        $(".report").hide();

        AccountingReportHelper.loadHead();
        $("#btnTrialBalance").click(function () {
            debugger;
            AccountingReportManager.TrialBalanceReport();
        });
        $("#btnGLTReport").click(function () {
            debugger;
            AccountingReportManager.GLDateWiseTransactionReport();
        });
        $("#btnDwTReport").click(function () {
            debugger;
            AccountingReportManager.DateWiseTransactionReport();
        });
        $("#btnShowTopSheet").click(function () {
            debugger;
            AccountingReportManager.AccountingTopSheet();
        });
        $("#btnShowPaymentSheet").click(function () {
            debugger;
            AccountingReportManager.ShowPaymentSheet();
        });
        $("input[name=report]:radio").change(function () {
            debugger;
            var reportType = $("input[name='report']:checked").val();
            if (reportType === "TrialBalance") {
                $(".report").hide();
                $("#TrialBalance").show();
            }
            else if (reportType === "GLT") {
                $(".report").hide();
                $("#GLT").show();
            }
            else if (reportType === "DwT") {
                $(".report").hide();
                $("#DwT").show();
            }
            else if (reportType === "TS") {
                $(".report").hide();
                $("#TS").show();
            }
            else if (reportType === "Payment") {
                $(".report").hide();
                $("#Payment").show();
            }
        });
        },
    loadHead: function () {
        var headData = AccountingReportManager.LoadHead();
        $("#cmbGLTACHeads").select2({
            placeholder: "Select a A/C Head",
            data: headData
        });
    }

}