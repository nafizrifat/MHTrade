$(document).ready(function () {
    ProcessHelper.processInit();
});
var ProcessManager = {
    GetAllCostCenterDd: function() {
        var b = [];
        $.ajax({
            type: "GET",
            datatype: "json",
            url: "/Account/ProcessJournal/GetAllCostCenterDd",
            cache: true,
            async: false,
            success: function(response) {
                b = response.data;
            },
            error: function() {
                b = { id: 0, text: "No Data" }
            }
        });
        return b;
    },
    ProcessUnprocessedData: function (unprocessedData,costCenterId) {
   
        $.ajax({
            type: "POST",
            datatype: "json",
            url: "/Account/ProcessJournal/ProcessUnprocessedData",
            //data:{data:data},
            data: JSON.stringify({ unprocessedData: unprocessedData, costCenterId: costCenterId }),
            cache: true,
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response != null) {
                    $("#myModal #modal-body #rif").html("Process done successfully");
                    $('#myModal').appendTo("body").modal('show');
                    ViewProcessManager.GetAllUnProcessedData(costCenterId);
                }
            },
            error: function () {
                $("#myModal #modal-body #rif").html("Sorry !some error occured .");

                $('#myModal').appendTo("body").modal('show');
            }
        });
    }
}
var ProcessHelper = {
    processInit: function() {
        ProcessHelper.getAllCostCenterDd();
        $("#cmbCostCenterId").select2().on("change", function (e) {
            var CostCenterId = $("#cmbCostCenterId").val();
            if (isNaN(CostCenterId)) {
                CostCenterId = 1;
            }
            ViewProcessManager.GetAllUnProcessedData(CostCenterId);
        });
        $('#btnProcess').click(function () {
            var costCenterId = $("#cmbCostCenterId").val();
            var unprocessedData = [];
                var rows = $("#myTable").dataTable().fnGetNodes();
                for (var i = 0; i < rows.length; i++) {
                    var table = $('#myTable').DataTable();
                    var data = table.row(i).data();
                    unprocessedData.push(data);
                }
                ProcessManager.ProcessUnprocessedData(unprocessedData, costCenterId);
        });
        $('#btnRefresh').click(function () {
            $("#cmbCostCenterId").select2().on("change", function(e) {
                var CostCenterId = $("#cmbCostCenterId").val();
                if (isNaN(CostCenterId)) {
                    CostCenterId = 1;
                }
                ViewProcessManager.GetAllUnProcessedData(CostCenterId);
            });
        });
        $('#btnReprocess').click(function () {
           ViewProcessManager.ReProcessedData();
        });
    },
    getAllCostCenterDd:function() {
       var processdataDd = ProcessManager.GetAllCostCenterDd();
       
        $("#cmbCostCenterId").select2({
            placeholder: "Select a Process",
            data: processdataDd
        });
    }
}
var ViewProcessManager= {
    GetAllUnProcessedData: function (CostCenterId) {        
        var data = { "CostCenterId": CostCenterId }
           
            $('#myTable').dataTable().fnDestroy();
            $('#myTable').DataTable({
                "ajax": {
                    "url": "/Account/ProcessJournal/GetAllUnprocessedData",
                    "data": data,
                    "type": "GET",
                    "datatype": "json",
                    "contentType": 'application/json; charset=utf-8'
                },
                "columns": [
                { "data": "Id", "autoWidth": true },
                    { "data": "Particular", "autoWidth": true },
                    { "data": "Amount", "autoWidth": true },
                    { "data": "ProcessStatus", "autoWidth": true },
                    { "data": "A_GlTransactionId", "autoWidth": true }
                ],
                "columnDefs": [
                {
                    targets: [2],
                    render: function(data, type, row) {
                        return data != '2' ? 'Unprocessed' : 'Processed';
                    }
                },
                 {
                     targets: [0],
                     "visible": false
                 },
                 {
                     targets: [4],
                     "visible": false
                 }]
            });

        },
    ReProcessedData: function() {
        $.ajax({
            type: "POST",
            datatype: "json",
            url: "/Account/ProcessJournal/ReprocessedData",
            cache: true,
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response != null) {
                    $("#myModal #modal-body #rif").html("Process done successfully");
                    $('#myModal').appendTo("body").modal('show');
                }
            },
            error: function () {
                $("#myModal #modal-body #rif").html("Sorry !some error occured .");
                $('#myModal').appendTo("body").modal('show');
            }
        });
    }
}