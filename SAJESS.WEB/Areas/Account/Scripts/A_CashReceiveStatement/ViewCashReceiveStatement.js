


$(document).ready(function () {
    $("#btnSearch").on("click", function () {
       
        var fromDate = $("#fromDate").datepicker('getDate');
        var toDate = $("#toDate").datepicker("getDate");
        if (fromDate != null && toDate != null) {
            viewCashReceiveManager.CashReceiveDatatable(fromDate, toDate);
        } else {
            alert("Please enter from and to date.");
        }

    });
    
      });

var viewCashReceiveManager = {
    CashReceiveDatatable: function (fromDate, toDate) {
       
        
        var data = { 'startDate': $("#fromDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14), 'endDate': $("#toDate").datepicker("getDate").toISOString("yyyy-MM-dd").slice(0, -14) };
       // var data = { "fromDate": fromDate, "toDate": toDate }
        $('#cashReceiveStatement').dataTable().fnDestroy();
        $('#cashReceiveStatement').DataTable({
            //"scrollX": true,
            "ajax": {
                "url": "/Account/A_CashReceiveStatement/GetAllCashReceiveStatement",
                "type": "GET",
                "data": data,
                "datatype": "json",
                "contentType": 'application/json; charset=utf-8'
            },
            "columns": [
                { "data": "Code", "autoWidth": true },
                { "data": "DebitAmount", "autoWidth": true },
                { "data": "CreditAmount", "autoWidth": true }
            ]
        });
    }
}