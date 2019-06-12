
var CreateSupplierInvestmentManager = {
    SaveSupplierInvestment: function () {
        //===============================================================
        //If the value of
        //isToUpdateOrCreate =0 then ----Create New company
        //isToUpdateOrCreate =1 then ----Update Company Information
        //===============================================================
        // JSON.stringify(json),
        //  var data = JSON.stringify(createAssetCategoryHelper.GetData(), createAssetCategoryHelper.GetData2());
        //   JSON.stringify(CreateSupplierInvestmentHelper.GetSupplierInvestmentData());

        var supplierId = $('#cmbSupplierId').val();
        var reason = $("#txtReason").val();
        var amount = $("#txtAmount").val();
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
            data: JSON.stringify(CreateSupplierInvestmentHelper.GetSupplierInvestmentObj()),

            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.data.Status) {
                    $("#saveAssetModal #modal-body #rif").html(response.data.Message);
                    $('#saveAssetModal').appendTo("body").modal('show');
                    ViewSupplierInvestmentManager.GetSupplierInvestmentData();
                    CreateSupplierInvestmentHelper.ClearForm();
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
                //$.each(response.result, function (key, value) {
                //    var bus = { text: value.text, id: value.id };
                //    b.push(bus);
                // });

            },
            error: function (textStatus, errorThrown) {
                b = { id: 0, text: "No Data" }
            }
        });
        return b;

    },
}

var CreateSupplierInvestmentHelper = {

    InitCreateSupplierInvestment: function () {
        CreateSupplierInvestmentHelper.loadSupplierCombo();
        $("#btnSaveSupplierInvestment").click(function () {
            CreateSupplierInvestmentManager.SaveSupplierInvestment();
        });
        $("#btnSupplierInvestment").click(function () {
            CreateSupplierInvestmentHelper.ClearForm();
        });

        debugger;
        // var date = new Date();
        //  $("#dtTransactionDate").val = date.toISOString('dd.MM.yyyy');
        // $("#dtTransactionDate").datepicker('setDate', new Date());
        $("#dtTransactionDate").datepicker({ dateFormat: "dd-mm-yy" }).datepicker("setDate", "0");
    },

    loadSupplierCombo: function () {
        var departmentData = CreateSupplierInvestmentManager.LoadSupplierComboData();
        $("#cmbSupplierId").select2({
            data: departmentData,
            placeholder: 'Select Supplier'
        });
    },


    ClearForm: function () {
        debugger;
        $('#hdnSupplierInvestmentId').val(0);
        $('#hdnA_GlTransactionId').val(0);
        $("#cmbSupplierId").val("").trigger("change");
        $('#txtReason').val('');
        $("#txtDesignationName").val('');
        $("#txtAmount").val('');
        $("#txtMemberName").val('');
        $("#txtMemberNid").val('');
        $("#dtTransactionDate").datepicker({ dateFormat: "dd-mm-yy" }).datepicker("setDate", "0");
    },

    GetSupplierInvestmentObj: function () {
   
        var aObj = new Object();
        aObj.SupplierInvestmentId = $('#hdnSupplierInvestmentId').val();
        aObj.SupplierId = $('#cmbSupplierId').val();
        aObj.Reason = $("#txtReason").val();
        aObj.Amount = $("#txtAmount").val();
        aObj.MemberName = $("#txtMemberName").val();
        aObj.MemberNid = $("#txtMemberNid").val();
        aObj.A_GlTransactionId = $("#hdnA_GlTransactionId").val();
       
        //aObj.TransactionDate2 = $("#dtTransactionDate").datepicker("getDate").toISOString("dd-MM-yyyy").slice(0, -14);
        aObj.TransactionDate = $("#dtTransactionDate").datepicker('getDate');
     //   var date = $("#dtTransactionDate").datepicker({ dateFormat: 'dd,MM,yyyy' }).val();
        debugger;
        return aObj;
    },
    populateSupplierInvestmentDataEditButton: function (aObj) {
        CreateSupplierInvestmentHelper.ClearForm();
        $('#hdnSupplierInvestmentId').val(aObj.SupplierInvestmentId);
        $("#cmbSupplierId").val(aObj.SupplierId).trigger("change");
        $('#txtReason').val(aObj.Reason);
        $('#txtAmount').val(aObj.Amount);
        $('#txtMemberName').val(aObj.MemberName);
        $('#txtMemberNid').val(aObj.MemberNid);
        $('#hdnA_GlTransactionId').val(aObj.A_GlTransactionId);

        var dt = new Date(parseInt(aObj.TransactionDate.substr(6)));
        var dateVisited = dt.getDate() + "-" + (dt.getMonth() + 1) + "-" + dt.getFullYear();
        $('#dtTransactionDate').val(dateVisited);
    },

    addNewSupplier: function () {

        $('#popup').dialog('open');
    }

}