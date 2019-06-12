$(document).ready(function () {
    CashOrBankPaymentHelper.CashOrBankPaymentInit();
});
var transactionList = [];
var count = 1;
var CashOrBankPaymentManager = {
    LoadHeads: function () {
        debugger;
        var type = parseInt($("#cmbCashOrBank").val());
        var b = [];
        $.ajax({
            type: "GET",
            url: "/Account/CashBankReceivePayment/LoadAllAccountHead",
            data: { type: type },
            cache: true,
            async: false,
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                b = response.data;
            },
            error: function () {
                b = { id: 0, data: " No Head Found" }
            }
        });
        return b;
    },
    loadvoucher: function () {
       
        $.ajax({
           
            type: "Get",
            dataType: "json",
            cache: true,
            async: false,
            url: "/Account/Journal/Loadvoucher",
            success: function (response) {
                $("#txtvoucherNoCB").val(response.data);
            },
            error: function (textStatus, errorThrown) {
                alert("Can not load voucher");
            }
        });

    },
    SavePaymentTransaction: function () {
       
        var cashOrBankHead = $("#cmbachead").val();
        var aparticular = $("#txtParticularsCB").val();
        var voucherNo = $("#txtvoucherNoCB").val();
        var aTransactionDate = $("#dtpTransactionDate").datepicker("getDate").toISOString("dd-MM-yyyy").slice(0, -14);
        var data = JSON.stringify({ 'data': CashOrBankPaymentHelper.GetDataObj(), 'cashOrBankHead': cashOrBankHead, 'voucherNo': voucherNo, 'transactionDate': aTransactionDate, 'particular': aparticular });

        $.ajax({
            type: "POST",
            url: "/Account/CashBankReceivePayment/SavePaymentTransaction",
            data: data,
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                
                alert(response.Data.Message);
                if (response.Data.Status) {
                    CashOrBankPaymentHelper.ClearAll();
                }
            },
            error: function () {
                alert("error.");
            }
        });
    },
    LoadOtherHeads: function () {
       
        var id = parseInt($("#cmbCashOrBank").val());
        var b = [];
        $.ajax({
            type: "GET",
            url: "/Account/CashBankReceivePayment/LoadOtherHeads",
            data: { id: id },
            cache: true,
            async: false,
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                b = response.data;
            },
            error: function () {
                b = { id: 0, data: "No Heads" }
            }
        });
        return b;
    }
}
var CashOrBankPaymentHelper = {
    CashOrBankPaymentInit: function () {
       
        $("#txtcashorbank").attr("readonly", "true");
        CashOrBankPaymentHelper.loadCashOrBankDd();
        CashOrBankPaymentManager.loadvoucher();
        $("#cmbCashOrBank").select2().on("change", function (e) {
            CashOrBankPaymentHelper.loadHeads();
            var receiveType = parseInt($("#cmbCashOrBank").val());
            if (receiveType === 1) {
                $("#txtcashorbank").val("Cash");
                $("#txtParticularsCB").val("Payment from Cash");
            }
            if (receiveType === 2) {
                $("#txtcashorbank").val("Bank");
                  $("#txtParticularsCB").val("Payment from Bank");
            }
            CashOrBankPaymentHelper.loadOtherHeads();
        });
        $("#btnAdd").click(function () {
            CashOrBankPaymentHelper.AddTransection();
        });

        $('#tbodycrdamntid2 tbody').on('click', '#btnRemove', function (e) {
           
            var table = $('#tbodycrdamntid2').DataTable();
            var data = table.row($(this).parents('tr')).data();
            CashOrBankPaymentHelper.removeARow(data);

        });
        $("#btnSaveTransactionData").click(function () {
            CashOrBankPaymentManager.SavePaymentTransaction();
        });
        $("#btnClearData").click(function () {
            CashOrBankPaymentHelper.ClearAll();
        });
    },
    GetDataObj: function () {
       
        var tblData = $('#tbodyCBRecPay tr:has(td)').map(function (i, v) {
            var $td = $('td', this);
            return {
                id: ++i,
                A_GlAccountId: $td.eq(1).text(),
                Name: $td.eq(2).text(),
                Code: $td.eq(3).text(),
                Particulars: $td.eq(4).text(),
                CreditAmount: $td.eq(5).text(),
                TransactionDate: $td.eq(6).text(),
                ChequeNumber: $td.eq(7).text()
            }
        }).get();
        debugger;
        return tblData;
    },
    myFunction2: function (aData) {
        for (var i = transactionList.length - 1; i >= 0; i--) {
            if (transactionList[i].countid == aData) {
                transactionList.splice(i, 1);
            }
        }
        CashOrBankPaymentHelper.PopuleTableData();
    },
    loadHeads: function () {
        var data = CashOrBankPaymentManager.LoadHeads();
        $("#cmbachead").select2({
            placeholder: "Select Account",
            data: data
        });
    },
    loadOtherHeads: function () {
        var data = CashOrBankPaymentManager.LoadOtherHeads();
        $("#cmbA_GlAccount").select2({
            placeholder: "Select Account",
            data: data
        });
    },
    loadCashOrBankDd: function () {
        var d = [
            { id: 1, text: 'Cash' },
            { id: 2, text: 'Bank' }
        ];
        $("#cmbCashOrBank").select2({
            placeholder: "Select .",
            data: d
        });
    },
    AddTransection: function () {
        var amount = $("#nbramount").val();
        var aGlAccountId = $("#cmbA_GlAccount").val();
        var nameAndCode = $("#cmbA_GlAccount :selected").text();
        var index = nameAndCode.indexOf("(");
        var aGlAccountName = nameAndCode.substring(index, nameAndCode.Lenght - 1);
        var aCode = nameAndCode.substring(nameAndCode.indexOf("(") + 1, nameAndCode.indexOf(")"));
        var aparticular = $("#txtParticulars").val();
        var aTransactionDate = $("#dtpTransactionDate").datepicker("getDate").toISOString("dd-MM-yyyy").slice(0, -14);
        var chequeno = $("#txtchequeNo").val();
        var aData = {
            countid: count,
            A_GlAccountId: aGlAccountId,
            Name: aGlAccountName,
            Code: aCode,
            Amount: amount,
            Description: aparticular,
            TransactionDate: aTransactionDate,
            ChequeNumber: chequeno
        }
        transactionList.push(aData);
        count++;
        CashOrBankPaymentHelper.PopuleTableData();
        CashOrBankPaymentHelper.ClearCurrentTransection();
    },
    PopuleTableData: function () {
        var tr;
        $("#tbodycrdamntid3").empty();
        for (var i = 0; i < transactionList.length; i++) {
            tr = $("<tr/>");
            tr.append("<td hidden>" + transactionList[i].countid + "</td>");
            tr.append("<td hidden>" + transactionList[i].A_GlAccountId + "</td>");
            tr.append("<td>" + transactionList[i].Name + "</td>");
            tr.append("<td>" + transactionList[i].Code + "</td>");
            tr.append("<td>" + transactionList[i].Description + "</td>");
            tr.append("<td>" + transactionList[i].Amount + "</td>");
            tr.append("<td>" + transactionList[i].TransactionDate + "</td>");
            tr.append("<td>" + transactionList[i].ChequeNumber + "</td>");
            tr.append("<td>" + "<button type='button' id='btn' class='btn btn-danger' onClick='CashOrBankPaymentHelper.myFunction2(" + transactionList[i].countid + ");' >" + "Remove" + "</button>" + "</td>");
            $("#tbodycrdamntid3").append(tr);
        }
    },
    ClearAll: function () {
       
        count = 0;
        transactionList = [];
        CashOrBankPaymentHelper.PopuleTableData();
        CashOrBankPaymentHelper.ClearCurrentTransection();
    },
    ClearCurrentTransection: function () {
        $("#nbramount").val("");
        $("#txtchequeNo").val("");
        $("#txtParticulars").val("");
        $("#txtchequeNo").val("");
        $("#cmbA_GlAccount").val("");
    }
}