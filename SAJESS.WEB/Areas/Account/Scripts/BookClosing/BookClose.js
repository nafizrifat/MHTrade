var BookCloseHelper = {

    GeneralLedgerInit:function() {}
    
};
var BookCloseManager = {

    GetFiscalYear: function () {
     $.ajax({
            type:"GET",
            url: "/Account/BookValue/GetFiscalYear",
            dataType:"json",
            contentType: "application/json;charset:utf-8",
            success:function(response) {
                
               
            },
            error:function() {
                
            }
        });

    }

}
