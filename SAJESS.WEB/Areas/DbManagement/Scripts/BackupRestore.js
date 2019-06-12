$(document).ready(function () {
    BackupRestoreHelper.BackupRestoreInit();
});
var BackupRestoreManager = {
    BackupWithoutFile: function(fileLocation) {
        $.ajax({
            type: "POST",
            datatype: "json",
            url: "/DbManagement/BackupRestore/BackupWithoutFile",
            cache: true,
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                $.bigBox({
                    title: "Information",
                    content: "ডাটা ব্যাকআপ সফল ভাবে সম্পূর্ণ হয়েছে" +
                        "<br /> System Backup Successfuly Done",
                    color: "#009933",
                    //timeout: 6000,
                    icon: "fa fa-successful shake animated",
                    number: "1",
                    timeout: 3000
                });
            },
            error: function() {
                $.bigBox({
                    title: "Error !!!",
                    content: "ডাটা ব্যাকআপ অসম্পূর্ণ" +
                        "<br /> System Backup Un-Successful",
                    color: "#ff0066",
                    //timeout: 6000,
                    icon: "fa fa-warning shake animated",
                    number: "1",
                    timeout: 3000
                });
            }
        });
    }
}
var BackupRestoreHelper = {
    BackupRestoreInit: function() {
        $('#btnBackup').click(function() {
            BackupRestoreManager.BackupWithoutFile();
        });
    }
}

