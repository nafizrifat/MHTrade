var createSuppliersManager = {
       //===============================================================
        //If the value of
        //isToUpdateOrCreate =0 then ----Create New company
        //isToUpdateOrCreate =1 then ----Update Company Information
        //===============================================================
        // JSON.stringify(json),
        //  var data = JSON.stringify(createSuppliersHelper.GetData(), createSuppliersHelper.GetData2());
    SaveSupplier: function () {
        debugger;
        $.ajax({
            type: 'POST',
            url: "/Management/Supplier/CreateSupplier",
            data: JSON.stringify(createSuppliersHelper.GetSupplierObj()),
            async:false,
            success: function (response) {

                if (response != null) {
                    $("#myModal #modal-body #rif").html(response.data.Message);
                    $('#myModal').appendTo("body").modal('show');
                    createSuppliersHelper.ClearForm();
                    // viewSupplierManager.GetSupplierData();
                    debugger;
                    try {
                        viewSupplierManager.GetSupplierData();
                    }
                    catch (err) {
                        $("#popup").dialog("close");
                        CreateSupplierInvestmentHelper.loadSupplierCombo();
                    }

                }
            },
            error: function (response) {
                $("#dialog_simple").html(response.data.Message);
                $('#dialog_simple').dialog('open');
            },
            dataType: "json",
            contentType: "application/json",
            //   processData: false,
            //  async: false
        });

    },
    LoadDistrictComboData: function () {
        var b = [];
        $.ajax({
            type: 'get',
            dataType: 'json',
            cache: true,
            async: false,
            url: '/Management/Supplier/DistrictCombo',
            success: function (response, textStatus) {
                b = response.result;
            },
            error: function (textStatus, errorThrown) {
                b = { id: 0, text: "No Data" }
            }
        });
        return b;

    }
}
var createSuppliersHelper = {
    InitCreateSuppliers: function () {
        debugger;
        $("#btnSaveSupplier").click(function () {
            debugger;
            createSuppliersManager.SaveSupplier();
           // viewSupplierManager.GetSupplierData();
        });
        $("#btnClear").click(function () {
            debugger;
            createSuppliersHelper.ClearForm();
        });
        createSuppliersHelper.loadDistrictCombo();
    },
    ClearForm: function () {
        debugger;
        //$('#myModal').modal('show');
        $('#hdnSupplierId').val(0);
        $("#cmbDistrictId").val("").trigger("change");
        $("#txtSupplierName").val('');
        $("#txtMobileNo").val('');
        $("#txtNoteSupplier").val('');
        $('#chkIsActive').removeAttr('checked', 'checked');
    },

    GetSupplierObj: function () {
        var aObj = new Object();
        aObj.SupplierId = $('#hdnSupplierId').val();
        aObj.SupplierName = $("#txtSupplierName").val();
        aObj.MobileNo = $("#txtMobileNo").val();
        aObj.DistrictId = $("#cmbDistrictId").val();
        aObj.Note = $("#txtNoteSupplier").val();
        aObj.IsActive = $("#chkIsActive").is(":checked") == true ? 1 : 0;
        return aObj;
    },
    PopulateSupplierData: function (aObj) {
        debugger;
        createSuppliersHelper.ClearForm();
        $('#hdnSupplierId').val(aObj.SupplierId);
        $("#txtSupplierName").val(aObj.SupplierName);
        $("#txtMobileNo").val(aObj.MobileNo);
        $("#cmbDistrictId").val(aObj.DistrictId).trigger("change");
        $("#txtNoteSupplier").val(aObj.Note);
        if (aObj.IsActive == 1) {
            $("#chkIsActive").prop('checked', 'checked');
        } else {
            $("#chkIsActive").removeProp('checked', 'checked');
        }
    },
    loadDistrictCombo: function () {
        var departmentData = createSuppliersManager.LoadDistrictComboData();
        $("#cmbDistrictId").select2({
            data: departmentData,
            placeholder: 'Select District',
         width: '100%' 
        });
        // $("#cmbAssetCategoryId").val(41).trigger("change");
    },


}