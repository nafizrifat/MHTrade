$(document).ready(function() {
    CreateNewGlHelper.CreateNewGlInit();
});
var CreateNewGlManager= {
    FillParentPropertyUsingId: function (id) {
        debugger;
        var data = id;
        $.ajax({
            type: "GET",
            url: "/Account/COA/FillParentPropertyUsingId",
            data: { id: id },
            success: function(response) {
                //alert("What the dush ! ");
                $("#txtParent").val(response.data.Name);
                $("#txtParentCode").val(response.data.Code);
                $("#txtNewChildCode").val(response.data.CodeOfNewChild);
                $("#hdnparentId").val(response.data.ParentId);
            },
            error:function(response) {
                alert("Error ! ");
            }

        });
    },
   CreateNode: function () {
        $.ajax({
            type: "POST",
            url: "/Account/COA/CreateNode",
            data: JSON.stringify(CreateNewGlHelper.GetNewNode()),
            dataType: "json",
            contentType: "application/json",
            cache: true,
            async: false,
            success: function (response) {
                //$("#myModal #modal-body #rif").html(response.data.Message);
                //$('#myModal').appendTo("body").modal('show');
                if (response.data.Status) {
                    $.bigBox({
                        title: "Congratiulation",
                        content: "What the dush",
                        color: "#739E73",
                       // timeout: 3000,
                        icon: "fa fa-check",
                        number: "4"
                    }, function () {
                        var closedthis = function () {
                            $.smallBox({
                                title: "Great! You just closed that last alert!",
                                content: "This message will be gone in 5 seconds!",
                                color: "#739E73",
                                iconSmall: "fa fa-cloud",
                               // timeout: 5000
                            });
                        };
                        closedthis();
                        e.preventDefault();

                    });
                    CreateNewGlHelper.Clear();
                    location.reload(true);
                   
                }
            },
            error: function (response) {
                alert("Error ! ");
            }

        });
    }
}
var CreateNewGlHelper= {
    CreateNewGlInit: function () {
        $('#txtParent').attr('readonly', true);
        $('#txtParentCode').attr('readonly', true);
        $('#txtNewChildCode').attr('readonly', true);
        $("input[type=checkbox]").click(function () {
            if ($(this).is(':checked')) {
              var id = $(this).attr('id');
                $('input[type="checkbox"]').not(this).prop("checked", false);
                CreateNewGlManager.FillParentPropertyUsingId(id);

            } else {
                CreateNewGlHelper.Clear();
            }
        });
        $("#btnSaveBlockInfo").on("click", function() {
            CreateNewGlManager.CreateNode();
        });

        
    },
    Clear: function() {

        $("#hdnId").val("");
        $("#hdnparentId").val("");
        $("#txtParent").val("");
        $("#txtParentCode").val("");
        $("#txtNewChild").val("");
        $("#txtNewChildCode").val("");
    },
    GetNewNode:function() {
        var aObj = new Object();
        aObj.A_GlAccountId = $("#hdnId").val();
        aObj.ParentId = $("#hdnparentId").val();
        aObj.Name = $("#txtNewChild").val();
        aObj.Code = $("#txtNewChildCode").val();
        return aObj;
    }
   

}