
var CreateReviseSupplierInvestmentManager = {
    ReviseSupplierInvestment: function () {
        debugger;
        //===============================================================
        //If the value of
        //isToUpdateOrCreate =0 then ----Create New company
        //isToUpdateOrCreate =1 then ----Update Company Information
        //===============================================================
        // JSON.stringify(json),
        //  var data = JSON.stringify(createAssetCategoryHelper.GetData(), createAssetCategoryHelper.GetData2());
        //   JSON.stringify(CreateReviseSupplierInvestmentHelper.GetSupplierInvestmentData());

        var supplierId = $('#cmbSupplierIdRevise').val();
        var reason = $("#txtReasonRevise").val();
        var amount = $("#txtAmountRevise").val();
        if ((supplierId == "") || (reason == "") || (amount == "")) {
            var text = "";
            var count = 0;
            if (supplierId == "") {
                text += "- Supplier";
                count++;
            }
            if (reason == "") {
                text += "- Reason";
                count++;
            }
            if (amount == "") {
                text += "- Amount";
                count++;
            }
            if (count == 1) {
                text += " is ";
            } else {
                text += " are ";
            }
            $("#saveAssetModal #modal-body #rif").html("WARNING: The Following Filed(s) " + text + " Required!");
            $("#saveAssetModal #modal-body #rif").css('color', 'red');

            $('#saveAssetModal').appendTo("body").modal('show');
            return;
        }
        $.ajax({
            type: 'POST',
            url: " /Management/SupplierInvestment/CreatSupplierInvestment",
            data: JSON.stringify(CreateReviseSupplierInvestmentHelper.GetReviseSupplierInvestmentData()),

            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.data.Status) {
                        $("#saveAssetModal #modal-body #rif").html(response.data.Message);

                        $('#saveAssetModal').appendTo("body").modal('show');
                        //  ViewSupplierInvestmentManager.GetSupplierInvestmentData();

                        CreateReviseSupplierInvestmentHelper.ClearForm();
                        $("#popupSupplier").dialog("close");
                        ViewReviseSupplierInvestmentHelper.SearchSupplierInvesmentBySupplierId();
                    } else {
                        $("#saveAssetModal #modal-body #rif").html(response.data.Message);

                        $('#saveAssetModal').appendTo("body").modal('show');
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

    LoadSupplierComboData: function () {

        var b = [];
        $.ajax({
            type: 'get',
            dataType: 'json',
            cache: true,
            async: false,
            url: '/Management/Supplier/SuppliersCombo',
            success: function (response, textStatus) {
                b = response.result;
            },
            error: function (textStatus, errorThrown) {
                b = { id: 0, text: "No Data" }
            }
        });
        return b;

    },
    DeleteInvestment: function () {
        $.ajax({
            type: 'POST',
            url: " /Management/SupplierInvestment/DeleteSupplierInvestment",
            data: JSON.stringify(CreateReviseSupplierInvestmentHelper.GetReviseSupplierInvestmentData()),
            success: function (response) {
                debugger;
                if (response != null) {
                    $("#saveAssetModal #modal-body #rif").html(response.data.Message);
                    $('#saveAssetModal').appendTo("body").modal('show');
                    CreateReviseSupplierInvestmentHelper.ClearForm();
                    $("#popupSupplier").dialog("close");
                    ViewReviseSupplierInvestmentHelper.SearchSupplierInvesmentBySupplierId();
                }
            },
            error: function (response) {
                $("#dialog_simple").html(response.data.Message);
                $('#dialog_simple').dialog('open');
            },
            dataType: "json",
            contentType: "application/json"
        });

    }
}
var CreateReviseSupplierInvestmentHelper = {

    InitReviseCreateSupplierInvestment: function () {
        CreateReviseSupplierInvestmentHelper.loadSupplierCombo();
        $("#btnInvestmentDelete").click(function () {
            debugger;
            CreateReviseSupplierInvestmentManager.DeleteInvestment();
        });
        $("#btnReviseSupplierInvestment").click(function () {
            var supplierInvestmentId = $("#hdnSupplierInvestmentIdRevise").val();
            if (supplierInvestmentId == 0) {
                $("#saveAssetModal #modal-body #rif").html("WARNING: Please Select A Invesment!");
                $("#saveAssetModal #modal-body #rif").css('color', 'red');
                $('#saveAssetModal').appendTo("body").modal('show');
                return;
            }

            CreateReviseSupplierInvestmentManager.ReviseSupplierInvestment();
        });
        $("#btnReviseSupplierInvestmentClear").click(function () {
            CreateReviseSupplierInvestmentHelper.ClearForm();
        });

        $("#dtTransactionDateRevise").datepicker({ dateFormat: "dd-mm-yy" }).datepicker("setDate", "0");
    },

    loadSupplierCombo: function () {
        debugger;
        var departmentData = CreateReviseSupplierInvestmentManager.LoadSupplierComboData();
        $("#cmbSupplierIdRevise").select2({
            data: departmentData,
            placeholder: 'Select Supplier'
        });
        $("#cmbSearchSupplierId").select2({
            data: departmentData,
            placeholder: 'Select Supplier'
        });
    },

    ClearForm: function () {
        debugger;
        $('#hdnSupplierInvestmentIdRevise').val(0);
        $("#cmbSupplierIdRevise").val("").trigger("change");
        $('#txtReasonRevise').val('');
        $("#txtAmountRevise").val('');
        $("#txtMemberNameRevise").val('');
        $("#txtMemberNidRevise").val('');
        $("#dtTransactionDateRevise").datepicker({ dateFormat: "dd-mm-yy" }).datepicker("setDate", "0");
    },

    GetReviseSupplierInvestmentData: function () {
        debugger;
        var aObj = new Object();
        aObj.SupplierInvestmentId = $('#hdnSupplierInvestmentIdRevise').val();
        aObj.SupplierId = $('#cmbSupplierIdRevise').val();
        aObj.Reason = $("#txtReasonRevise").val();
        aObj.Amount = $("#txtAmountRevise").val();
        aObj.MemberName = $("#txtMemberNameRevise").val();
        aObj.MemberNid = $("#txtMemberNidRevise").val();
        //  aObj.TransactionDate = $("#dtTransactionDateRevise").datepicker("getDate").toISOString("dd-MM-yyyy").slice(0, -14);
        aObj.TransactionDate = $("#dtTransactionDateRevise").datepicker('getDate');
        return aObj;
    },
    populateReviseInvestmentDataEditButton: function (aObj) {
        CreateReviseSupplierInvestmentHelper.ClearForm();
        $('#hdnSupplierInvestmentIdRevise').val(aObj.SupplierInvestmentId);
        $("#cmbSupplierIdRevise").val(aObj.SupplierId).trigger("change");
        $('#txtReasonRevise').val(aObj.Reason);
        $('#txtAmountRevise').val(aObj.Amount);
        $('#txtMemberNameRevise').val(aObj.MemberName);
        $('#txtMemberNidRevise').val(aObj.MemberNid);
        $('#hdnA_GlTransactionId').val(aObj.A_GlTransactionId);

        var dt = new Date(parseInt(aObj.TransactionDate.substr(6)));
        var dateVisited = dt.getDate() + "-" + (dt.getMonth() + 1) + "-" + dt.getFullYear();
        $('#dtTransactionDateRevise').val(dateVisited);
    }



}