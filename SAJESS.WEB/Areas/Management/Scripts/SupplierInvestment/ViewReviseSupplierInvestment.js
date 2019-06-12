var ViewReviseSupplierInvestmentManager = {
    GetSupplierInvestmentDataForRevise: function (aSupplierId) {
    
        $('#myTable').dataTable().fnDestroy();
        $('#myTable').DataTable({
            "ajax": {
                "url": "/Management/SupplierInvestment/GetAllSupplierInvestmentBySupplierId",
                "type": "GET",
                "datatype": "json",
                data: { 'supplierId': parseInt(aSupplierId) },
                "contentType": 'application/json; charset=utf-8'

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
                { "data": "Amount", "width": "20%"  },
                { "data": "MemberName", "width": "10%" },
                { "data": "MemberNid", "width": "20%" },
                 { "data": "A_GlTransactionId", "autoWidth": true, "visible": false },
                {
                    "defaultContent":
                        '<button class="btn btn-primary" id="btnEdit" type="button">Edit</button>'

                },
                //{
                //    "defaultContent":
                //        '<button class="btn btn-danger" id="btnDelete" type="button">Delete</button>'

                //}
            ],
            "language": {
            "emptyTable":     "No Data For This Supplier"
        }

        });

    }
}
var ViewReviseSupplierInvestmentHelper = {
    SearchSupplierInvesmentBySupplierId: function() {
        var aSupplierId = $('#cmbSearchSupplierId').val();
        ViewReviseSupplierInvestmentManager.GetSupplierInvestmentDataForRevise(aSupplierId);

        CreateReviseSupplierInvestmentHelper.ClearForm();
        debugger;
        $('#myTable tbody').on('click', 'button', function () {
            $('#popupSupplier').dialog('open');
            var table = $('#myTable').DataTable();
            var data = table.row($(this).parents('tr')).data();
            CreateReviseSupplierInvestmentHelper.populateReviseInvestmentDataEditButton(data);
            // viewAssetCategoryHelper.populateDataForEditButton(data);
        });

    },


    InitViewReviseSupplierInvestment: function () {
        debugger;

     
        //$('#myTable tbody').on('click', 'button', function () {
        //    debugger;
        //    var table = $('#myTable').DataTable();
        //    var data = table.row($(this).parents('tr')).data();
        //    CreateReviseSupplierInvestmentHelper.populateReviseInvestmentDataEditButton(data);
        //    // viewAssetCategoryHelper.populateDataForEditButton(data);
        //});

        //$("#dtFromDate").datepicker({ dateFormat: "dd-mm-yy" }).datepicker("setDate", "0");
        //$("#dtToDate").datepicker({ dateFormat: "dd-mm-yy" }).datepicker("setDate", "0");

    }


}
