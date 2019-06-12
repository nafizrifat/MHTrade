$(document).ready(function () {
    debugger;
    //$("#btnSave").click(function () {
       
    //    createSuppliersManager.SaveSupplier();
       
    //    CreateSupplierInvestmentHelper.loadSupplierCombo();
    //});
    
    ViewSupplierInvestmentHelper.InitSupplierInvestment();
    CreateSupplierInvestmentHelper.InitCreateSupplierInvestment();
  

    $("#btnAddNewSupplier").click(function () {
        createSuppliersHelper.ClearForm();
       
        CreateSupplierInvestmentHelper.addNewSupplier();
    });
    createSuppliersHelper.InitCreateSuppliers();    

    $('#popup').dialog({
        autoOpen: false,
        width: 600,
        resizable: false,
        modal: true,
        title: ""
     
    });
});