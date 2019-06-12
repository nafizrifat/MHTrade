var d=0;
var c=0;


$(document).ready(function () {
    JournalEntryHelper.InitCreditVoucher();
});

var transactionList = [];
var count = 1;

var JournalEntryManager = {
    SaveJournal: function () {
        debugger;
        var data = JSON.stringify({ 'journalList': JournalEntryHelper.GetDataObj() });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "/Account/Journal/SaveTransaction",
            data: data,
            success: function (response) {
             // if (response != null) {
                // window.location.href = response;
               
                transactionList = [];
                $("#addFirstItem").find("input,button,textarea,select").attr("enable", "enable");
                JournalEntryHelper.PopuleTableData();
                  alert("Transaction Saved Successfully.");
             // }
            },
            error: function (response) {
                $("#dialog_simple").html(response.data.Message);
                $('#dialog_simple').dialog('open');
            }
        });
    },
    loadvoucher:function() {
        $.ajax({
          
                type: "Get",
                dataType: "json",
                cache: true,
                async: false,
                url: "/Account/Journal/Loadvoucher",
                success: function (response, textStatus) {
                    $("#txtVoucher").val(response.data);
                },
                error: function (textStatus, errorThrown) {
                    alert("Can not load voucher");
                }
            });
        
    },
    loadA_GlComboData: function () {
        var b = [];
        $.ajax({
            type: "Get",
            dataType: "json",
            cache: true,
            async: false,
            url: "/Account/Journal/A_GlComboLoad",
            success: function (response, textStatus) {
                b = response.result;
            },
            error: function (textStatus, errorThrown) {
                b = { id: 0, text: "No Data" }
            }
        });
        return b;

    },
    LoadSecondA_GlComboData: function () {
        debugger;
        var id = $("#cmbA_GlAccount").val();
        var b = [];
        $.ajax({
            type: "Get",
            dataType: "json",
            cache: true,
            async: false,
            data:{id:id},
            url: "/Account/Journal/SecondA_GlComboLoad",
            success: function (response, textStatus) {
                b = response.data;
            },
            error: function (textStatus, errorThrown) {
                b = { id: 0, text: "No Data" }
            }
        });
        return b;

    },
    loadVoutcherNumber: function () {
       
        var b = [];
        $.ajax({
            type: "get",
            dataType: "json",
            cache: true,
            async: false,
            url: "/Accounting/CreditVoucher/LoadVoutcherNumber",
            success: function (response, textStatus) {
               
                b = response.result;
               $("#txtVoucherNo").val(response);
            },
            error: function (textStatus, errorThrown) {
               

            }
        });

    }
}

var JournalEntryHelper = {
    InitCreditVoucher: function () {
        $("#secondtable").hide();
        var date =new Date();
        debugger;
        $("#dtpTransactionDate").val=date.toISOString('dd.MM.yyyy');
        $("#cmbA_GlAccount").select2().on("change", function () {
          
        JournalEntryHelper.loadSecondA_GlComboData();
        });
        
       
        $("#btnAddTransection").click(function () {
            debugger;
            var aglId = parseInt($("#cmbA_GlAccount").val());
            var amount = parseInt($("#txtAmount").val());
            if (isNaN(amount)) {
                amount = 0;
            }
            if (isNaN(aglId)) {
                aglId = 0;
            }
            //$('table[id="addCreditBillItem"]').attr("disable", "true");
            if (aglId != 0 && amount != 0) {
                $("#addFirstItem").find("input,button,textarea,select").attr("disabled", "disabled");
                JournalEntryHelper.AddTransection();
                $("#secondtable").show();
                var nature = $("#cmbEntryNature").val();
                if (nature == "Debit") {
                    $("#secondEntryNature").val("Credit");
                    $("#secondEntryNature").attr("disabled", "true");
                }
                else if (nature == "Credit") {
                    $("#secondEntryNature").val("Debit");
                    $("#secondEntryNature").attr("disabled", "true");
                }
            } else {
                {
                    alert("Please select an account head and enter an amount.");
                }
            }
            
        });
        $("#btnSaveJournal").click(function () {
            var debit = parseFloat($("#txtTotalDebit").val());
            var credit = parseFloat($("#txtTotalCredit").val());
            if (debit != credit) {
                alert("Debit and credit balance are not equal.")
            }
            else if (debit === credit) {
                JournalEntryManager.SaveJournal();
                }
           
        });
        $("#btncngvoucher").click(function () {
            var voucher = parseInt($("#txtVoucher").val());
            if (d === c) {
                 ++voucher;
                 $("#txtVoucher").val(voucher);
                 d = 0;
                c = 0;
            } else {
                alert("Voucher number can not be saved as for voucher no. " + voucher+" debit and credit amount is different.");
            }
           });
        JournalEntryManager.loadvoucher();
        //$("#btnClearLedger").click(function () {
        //   
        //    CreateLedgerHelper.ClearForm();
        //});
        JournalEntryHelper.loadA_GlAccount();
        $("#btnsecondAddTransection").click(function () {
            JournalEntryHelper.AddSecondTransection();
        });
    },

    GetDataObj: function () {
       
        var tblData = $('#tNameJournals tr:has(td)').map(function (i, v) {
            var $td = $('td', this);
            return {
                id: ++i,
                A_GlAccountId: $td.eq(1).text(),
                Code: $td.eq(2).text(),
                Name: $td.eq(3).text(),
                EntryType: "2",
                Particulars: $td.eq(4).text(),
                DebitAmount: $td.eq(5).text(),
                CreditAmount: $td.eq(6).text(),
                TransactionDate: $td.eq(7).text(),
                VoucharNumber: $td.eq(8).text(),
                ChequeNumber: $td.eq(9).text()
        }
        }).get();
        return tblData;
    },
    myFunction2: function (aData) {
        for (var i = transactionList.length - 1; i >= 0; i--) {
            if (transactionList[i].countid == aData) {
                if (transactionList[i].EntryType == "Debit")
                {
                    d = d - transactionList[i].Amount;
                }
                else if (transactionList[i].EntryType == "Credit") {
                    c = c - transactionList[i].Amount;
                }
                transactionList.splice(i, 1);
            }
        }
        debugger;
        if (c != d) {
            $("#secondtable").show();
        }
        JournalEntryHelper.PopuleTableData();
    },
    loadA_GlAccount: function () {
        var ledgerData = JournalEntryManager.loadA_GlComboData();
        $("#cmbA_GlAccount").select2({
            data: ledgerData,
            placeholder: "Select A/H"
        });
    },
    AddTransection: function () {
        if ($("#cmbEntryNature").val() === "Debit") {
            d += parseInt($("#txtAmount").val());
        } else {
            c += parseInt($("#txtAmount").val());
        }
        var aGlAccountId = $("#cmbA_GlAccount").val();
        var nameAndCode = $("#cmbA_GlAccount :selected").text();

        var index = nameAndCode.indexOf("(");
        var aGlAccountName = nameAndCode.substring(index, nameAndCode.Lenght - 1);
        var aCode = nameAndCode.substring(nameAndCode.indexOf("(") + 1, nameAndCode.indexOf(")"));
       
        var aAmount = $("#txtAmount").val();
        var avoucher = $("#txtVoucher").val();
       
        var entryType = $("#cmbEntryNature :selected").text();
        var aTransactionDate = $("#dtpTransactionDate").datepicker("getDate").toISOString("dd-MM-yyyy").slice(0, -14);
        var aData = {
            countid: count,
            GlAccountId: aGlAccountId,
            GLAccountNmae: aGlAccountName,
            GlAccountCode: aCode,
            Amount: aAmount,
            VoucharNumber: avoucher,
            ChequeNumber:null,
            Particulars: null,
            TransactionDate: aTransactionDate,
            EntryType: entryType
        }
        transactionList.push(aData);
        count++;
        JournalEntryHelper.PopuleTableData();
       // JournalEntryHelper.ClearCurrentTransection();

    },
    AddSecondTransection: function () {
        debugger;
        if ($("#secondEntryNature").val() === "Debit") {
            d += parseInt($("#txtSecondAmount").val());
        } else {
            c += parseInt($("#txtSecondAmount").val());
        }
        var aGlAccountId = $("#secondcmbA_GlAccount").val();
        var nameAndCode = $("#secondcmbA_GlAccount :selected").text();

        var index = nameAndCode.indexOf("(");
        var aGlAccountName = nameAndCode.substring(index, nameAndCode.Lenght - 1);
        var aCode = nameAndCode.substring(nameAndCode.indexOf("(") + 1, nameAndCode.indexOf(")"));
        var aParticulars = $("#txtParticulars").val();
        var aAmount = $("#txtSecondAmount").val();
        var avoucher = $("#txtVoucher").val();
        var chequeNo = $("#txtsecondcheque").val();
        var entryType = $("#secondEntryNature").val();
        var aTransactionDate = $("#dtpTransactionDate").datepicker("getDate").toISOString("dd-MM-yyyy").slice(0, -14);
        var aData = {
            countid: count,
            GlAccountId: aGlAccountId,
            GLAccountNmae: aGlAccountName,
            GlAccountCode: aCode,
            Amount: aAmount,
            VoucharNumber: avoucher,
            ChequeNumber:chequeNo,
            Particulars: aParticulars,
            TransactionDate: aTransactionDate,
            EntryType: entryType
        }
        transactionList.push(aData);
        count++;
        if(c==d)
        {
            $("#addFirstItem").find("input,button,textarea,select").attr("enabled", "enabled");
            $("#secondtable").hide();
        }
        JournalEntryHelper.PopuleTableData();
        JournalEntryHelper.ClearSecondTransection();

    },
    ClearCurrentTransection: function () {
        $("#hdnAcLedgerName").val("");
        $("#cmbAcLedgerId").val("").trigger("change");
        $("#txtParticulars").val("");
        $("#txtAmount").val("");
    },
    ClearSecondTransection: function () {
        $("#hdnsecondName").val("");
        $("#secondcmbA_GlAccount").val("").trigger("change");
        $("#txtParticulars").val("");
        $("#txtSecondAmount").val("");
    },
    PopuleTableData: function () {
        var tr;
        var totalDebitAmount = 0;
        var totalCreditAmount = 0;
        $("#tbodycrdamntid2").empty();
        for (var i = 0; i < transactionList.length; i++) {
            if (transactionList[i].EntryType === "Debit") {
                totalDebitAmount += parseFloat(transactionList[i].Amount);
            } else {
                totalCreditAmount += parseFloat(transactionList[i].Amount);
            }
            tr = $("<tr/>");
            tr.append("<td hidden>" + transactionList[i].countid + "</td>");
            tr.append("<td hidden>" + transactionList[i].GlAccountId + "</td>");
            tr.append("<td hidden>" + transactionList[i].GlAccountCode + "</td>");
            tr.append("<td>" + transactionList[i].GLAccountNmae + "</td>");
            tr.append("<td>" + transactionList[i].Particulars + "</td>");
            if (transactionList[i].EntryType === "Debit") {
                tr.append("<td>" + transactionList[i].Amount + "</td>");
            } else {
                tr.append("<td>" + 0 + "</td>");
            }
            if (transactionList[i].EntryType === "Credit") {
                tr.append("<td>" + transactionList[i].Amount + "</td>");
            } else {
                tr.append("<td>" + 0 + "</td>");
            }
            tr.append("<td>" + transactionList[i].TransactionDate + "</td>");
            tr.append("<td>" + transactionList[i].VoucharNumber + "</td>");
            tr.append("<td>" + transactionList[i].ChequeNumber + "</td>");
            tr.append("<td>" + "<button type='button' id='btn' class='btn btn-danger' onClick='JournalEntryHelper.myFunction2(" + transactionList[i].countid + ");' >" + "Remove" + "</button>" + "</td>");
            $("#tbodycrdamntid2").append(tr);
        }
        $("#txtTotalDebit").val(totalDebitAmount);
        $("#txtTotalCredit").val(totalCreditAmount);

    },
    loadSecondA_GlComboData: function () {
        debugger;
        var GLdata = JournalEntryManager.LoadSecondA_GlComboData();
        $("#secondcmbA_GlAccount").select2({
            placeholder: "Select..",
            data: GLdata
        });
    },
}