$(document).ready(function () {

    $("#chkAllDate").click(function () {
        debugger;
    
        if ($("#chkAllDate").is(':checked')) {
            $("#idDateRange").hide();
          
        } else {
            $("#idDateRange").show();
        }
    });
    SuppliersInvestmentSummaryHelper.InitSuppliersInvestmentSummary();
});

var SuppliersInvestmentSummaryManager = {
    GetReport: function () {
        debugger;
        var supplierId = $('#cmbSupplierId').val();
        var fromDate = $('#dtFromDate').val();
        var toDate = $('#dtToDate').val();
       var checkAllDate = $("#chkIsActive").is(":checked") == true ? 1 : 0;
       if( checkAllDate==0 && fromDate> toDate)
        {
            $("#saveAssetModal #modal-body #rif").html("WARNING: From date must be lower than To Date!");
            $("#saveAssetModal #modal-body #rif").css('color', 'red');

            $('#saveAssetModal').appendTo("body").modal('show');
            return;
        }
        $.ajax({
            type: 'post',
            url: "  /Management/ManagementReport/ReportManagementReportSummaryData",
            data:SuppliersInvestmentSummaryHelper.getReportData(),

            success: function (response) {
                if (response != null) {
                    if (response) {
                        window.open("../ManagementReport/ReportManagementReportSummaryReport");
                    }
                    else {

                        $.bigBox({
                            title: "No Data Found",
                            content: "প্রদত্ত ডাঁটার কোন হিসাব মেলেনি" +
                                "There is no record for the given criteria",
                            color: "#C46A69",
                            //timeout: 6000,
                            icon: "fa fa-warning shake animated",
                            number: "1",
                            timeout: 3000
                        });


                    }
                }
            },
            error: function (response) {
                $("#dialog_simple").html(response.data.message);
                $('#dialog_simple').dialog('open');
            },
            datatype: "json",
            contenttype: "application/json",
           
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
    }
}

var SuppliersInvestmentSummaryHelper = {

    InitSuppliersInvestmentSummary: function () {
        SuppliersInvestmentSummaryHelper.loadSupplierCombo();
        $("#btnGetReport").click(function () {
            SuppliersInvestmentSummaryManager.GetReport();
        });
        $("#btnClear").click(function () {
            SuppliersInvestmentSummaryHelper.ClearForm();
        });

        debugger;
        $("#dtFromDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", "0");
        $("#dtToDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", "0");
    },

    loadSupplierCombo: function () {
        var departmentData = SuppliersInvestmentSummaryManager.LoadSupplierComboData();
        $("#cmbSupplierId").select2({
            data: departmentData,
            placeholder: 'Select Supplier'
        });
    },
    ClearForm: function () {
        $("#dtFromDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", "0");
        $("#dtToDate").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", "0");
        $('#chkAllDate').removeAttr('checked', 'checked');
        $("#cmbSupplierId").val("").trigger("change");
        $("#idDateRange").show();
    },

    getReportData: function () {
        debugger;
        var aObj = new Object();
        aObj.SupplierId = $('#cmbSupplierId').val();
        aObj.FromDate = $('#dtFromDate').val();
        aObj.ToDate = $('#dtToDate').val();
        aObj.AllDate = $("#chkAllDate").is(":checked") == true ? 1 : 0;
       
        return aObj;
    },
   

    
}