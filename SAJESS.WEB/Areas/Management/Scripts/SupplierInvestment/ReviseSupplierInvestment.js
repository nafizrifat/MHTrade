$(document).ready(function () {
    debugger;
    $("#cmbSearchSupplierId").change(function () {
        debugger;
        var toSearchSupplierId = $('#cmbSearchSupplierId').val();
        if (toSearchSupplierId=="") {
            $("#saveAssetModal #modal-body #rif").html("Please Select Supplier Id");
            $("#saveAssetModal #modal-body #rif").css('color', 'red');
            $('#saveAssetModal').appendTo("body").modal('show');
            return;
        }

        ViewReviseSupplierInvestmentHelper.SearchSupplierInvesmentBySupplierId();
    });
    ViewReviseSupplierInvestmentHelper.InitViewReviseSupplierInvestment();
    CreateReviseSupplierInvestmentHelper.InitReviseCreateSupplierInvestment();

    $('#popupSupplier').dialog({
        autoOpen: false,
        // width: 800,
        width: "70%",
        resizable: false,
        modal: true,
        title: ""
     
    });
});