var viewSupplierManager = {

    GetSupplierData: function () {
        $('#myTable').dataTable().fnDestroy();
        $('#myTable').DataTable({
            "ajax": {
                "url": "/Management/Supplier/GetAllSuppliers",
                "type": "GET",
                "datatype": "json",
                "contentType": 'application/json; charset=utf-8'

            },
            "columns": [
                { "data": "SupplierId", "autoWidth": true, "visible": false },
                { "data": "SupplierName", "autoWidth": true },
                { "data": "MobileNo", "autoWidth": true },
                 { "data": "DistrictName", "autoWidth": true },
                 { "data": "DistrictId", "autoWidth": true, "visible": false },
                 { "data": "Note", "autoWidth": true },
                { "data": "IsActive", "autoWidth": true },
                
               
               {
                   "defaultContent":
                       '<button class="btn btn-primary" id="btnEdit" type="button">Edit</button>'

               }],
            "columnDefs": [
            {
                // The `data` parameter refers to the data for the cell (defined by the
                // `data` option, which defaults to the column being worked with, in
                // this case `data: 0`.
                targets: [6],
                render: function (data, type, row) {
                    debugger;
                    return data == '1' ? 'Active' : 'Inactive'
                }
            },

            ],
        });

    }
}

var viewSupplierHelper = {

    InitSupplierHelper: function () {
        debugger;
        viewSupplierManager.GetSupplierData();
        $('#myTable tbody').on('click', 'button', function () {
            debugger;
            var table = $('#myTable').DataTable();
            var data = table.row($(this).parents('tr')).data();
            createSuppliersHelper.PopulateSupplierData(data);
        });
    },


}
