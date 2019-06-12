
var GeneralLedgerManager= {
    GetGeneralLedgerDatatable: function (id) {
        var data = { "id": id }
       
        $('#generalLedger').dataTable().fnDestroy();
        $('#generalLedger').DataTable({
            "ajax": {
                "url": "/Account/GeneralLedger/GetLedgerData",
                "data":data,
                "type": "GET",
                "datatype": "json",
                "contentType": 'application/json; charset=utf-8'

            },
            "columns": [
                { "data": "Name", "autoWidth": true },
                { "data": "OpeningAmount", "autoWidth": true },
                { "data": "Debit", "autoWidth": true },
                { "data": "Credit", "autoWidth": true },
                { "data": "ClosingAmount", "autoWidth": true },
                //{
                //    "data": "ClosingDate",
                //    "type": "date ",
                //    "render": function(value) {
                //        if (value === null) return "";

                //        var pattern = /Date\(([^)]+)\)/;
                //        var results = pattern.exec(value);
                //        var dt = new Date(parseFloat(results[1]));

                //        return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
                //    }
                //}
            ]
        });
        GeneralLedgerManager.GetTotal(id);
    },
    GetTotal: function (id) {
        $.ajax({
            type:"GET",
            url: "/Account/GeneralLedger/GetTotal",
            data:{id:id},
            dataType:"json",
            contentType: "application/json;charset:utf-8",
            success:function(response) {
                //alert(JSON.stringify(response.data));
                var date = new Date(parseInt(response.data.ClosingDate.substr(6)));
                var closingDate = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
                var arow = '<tr>'+'<td>'+""+'</td>' + '<td>' + "<b>Total</b>" + '</td>' + '<td>' +'<b>'+ response.data.Debit+'</b>' + '</td>' + '<td>'+'<b>' + response.data.Credit + '</td>' + '<td>' + " " + '</td>' + '</tr>';
                $('#generalLedger').append(arow);
               
            },
            error:function() {
                
            }
        });
    }
}

var GeneralLedgerHelper = {
    GeneralLedgerInit:function() {
        $("input[type=checkbox]").click(function () {
           
            if ($(this).is(':checked')) {
                var id = $(this).attr('id');
                $('input[type="checkbox"]').not(this).prop("checked", false);
                GeneralLedgerManager.GetGeneralLedgerDatatable(id);

            } else {
               
                
            }
        });
    }
    
}
