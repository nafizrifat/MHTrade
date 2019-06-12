var ViewSupplierInvestmentManager = {


    GetSupplierInvestmentData: function () {
        debugger;
        var dateObj = new Object();
        var aFromDate = $("#dtFromDate").datepicker('getDate');
       var aToDate = $("#dtToDate").datepicker('getDate');
        dateObj.FromDate = $("#dtFromDate").val();
        dateObj.ToDate = $("#dtToDate").val();
        
        //dateObj.FromDate = $("#dtFromDate").val();
        //dateObj.ToDate = $("#dtToDate").val();
       
     //   dateObj.TransactionDate = $("#dtFromDate").datepicker('getDate');
        if (aToDate < aFromDate) {

            $("#saveAssetModal #modal-body #rif").html("WARNING: To Date Must Be Greater Than From Date");
            $("#saveAssetModal #modal-body #rif").css('color', 'red');

            $('#saveAssetModal').appendTo("body").modal('show');
            return;
        }
       

        //var toDate = $('#dtToDate').val();
        $('#myTable').dataTable().fnDestroy();
        $('#myTable').DataTable({
            "ajax": {
                "url": "/Management/SupplierInvestment/GetAllSupplierInvestment",
               // data: JSON.stringify(dateObj),
                data: { 'FromDate': dateObj.FromDate, 'ToDate': dateObj.ToDate },
                "type": "GET",
                "datatype": "json",
                "contentType": 'application/json; charset=utf-8',

            },
            "columns": [
                { "data": "SupplierInvestmentId", "autoWidth": true, "visible": false },
                { "data": "SupplierId", "autoWidth": true, "visible": false },
                { "data": "SupplierName", "width": "20%" },
                {
                    "data": "TransactionDate",
                    "width": "20%",
                    "render": function(d) {
                        return moment(d).format("DD/MM/YYYY");
                    }
                },
                { "data": "Reason", "width": "30%" },
                //{ "data": "Note", "autoWidth": true },
                { "data": "Amount", "width": "20%"  },
                { "data": "MemberName", "width": "10%" },
                { "data": "MemberNid", "width": "20%" },
                 { "data": "A_GlTransactionId", "autoWidth": true, "visible": false },
                {
                    "defaultContent":
                        '<button class="btn btn-primary" id="btnEdit" type="button">Edit</button>'

                }],
            "order": [[0, "desc"]]

        });

    }
}

var ViewSupplierInvestmentHelper = {
    InitSupplierInvestment: function () {

        
        $("#btnFilterTable").click(function () {
       

            ViewSupplierInvestmentManager.GetSupplierInvestmentData();
        });
        debugger;
        $("#dtFromDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", "-30");
        $("#dtToDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", "0");
        ViewSupplierInvestmentManager.GetSupplierInvestmentData();
        $('#myTable tbody').on('click', 'button', function () {
            debugger;
            var table = $('#myTable').DataTable();
            var data = table.row($(this).parents('tr')).data();
            CreateSupplierInvestmentHelper.populateSupplierInvestmentDataEditButton(data);
            // viewAssetCategoryHelper.populateDataForEditButton(data);
        });

        

    },


}
