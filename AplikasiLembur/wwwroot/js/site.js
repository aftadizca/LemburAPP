// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".modal").each(function (l) { $(this).on("show.bs.modal", function (l) { var o = $(this).attr("data-easein"); "shake" == o ? $(".modal-dialog").velocity("callout." + o) : "pulse" == o ? $(".modal-dialog").velocity("callout." + o) : "tada" == o ? $(".modal-dialog").velocity("callout." + o) : "flash" == o ? $(".modal-dialog").velocity("callout." + o) : "bounce" == o ? $(".modal-dialog").velocity("callout." + o) : "swing" == o ? $(".modal-dialog").velocity("callout." + o) : $(".modal-dialog").velocity("transition." + o) }) });

function ValidateForm(formid, urlpost, nameAction) { 

        $(formid).on("submit", function (e) {

            e.preventDefault();

            if ($(formid + " input.form-control.valid").length === $(formid +" input.form-control").length) {

                AnimateSubmitButton(formid + " button[type='submit']", true); 

                $.post(urlpost,
                    $(formid).serialize(),
                
                    function (data) {

                        console.log("incoming data: " + JSON.stringify(data));
                        AnimateSubmitButton(formid + " button[type='submit']", false);

                        var name = [];
                        $(formid + " input").map(function () {
                            name.push($(this).attr("name"));
                        });
                   
                        try {
                            if (data["type"] === "error") {
                                var results = JSON.parse(data["data"]);
                                for (var i = 0; i < name.length; i++) {

                                    if (typeof results[name[i]] != "undefined" && results[name[i]].length > 0) {

                                        $("span[data-valmsg-for='" + name[i].toString() + "']").removeClass("field-validation-valid")
                                            .addClass("field-validation-error").html("<span>" + results[name[i]] + "</span>").show();
                                        $("input[name='" + name[i] + "']").removeClass("valid").addClass("input-validation-error").val("").focus();
                                    }
                                }
                            } else if (data["type"] === "msg") { 
                                $(".modal").modal("hide");
                                ShowMessageDialog(data["messageType"], data["message"], nameAction);
                                $("#messageDialogInfo").on('hidden.bs.modal',
                                    function() {
                                        $(formid + " input.form-control").val("");
                                    });

                            } else if (data["type"] === "url") {
                                window.location.replace(data["data"]);
                            }
                        } catch (e) {
                            ShowMessageDialog("", "Something wrong");
                            console.log(data);
                        }
                    }); 
            }
            e.stopPropagation();
        });
    }


$("button[data-dismiss='modal']").click(function () {
    AnimateSubmitButton($(this).siblings("button[type='submit']"), false);
});

function ShowMessageDialog(type, msg, name, timeout) { 
    if (typeof timeout === 'undefined') {
        timeout = 2500;
    }
    if (type === "information") {
        $("#messageDialogInfo strong").html(msg);
        $("#messageDialogInfo").modal('show');
        $("#messageDialogInfo").attr("name", name);
        setTimeout(function () { $("#messageDialogInfo").modal('hide');}, timeout)
    } else {
        $("#messageDialogError strong").html(msg);
        $("#messageDialogError").modal('show');
        $("#messageDialogError").attr("name", name);
    }
}


    function ShowConfirmationDialog(actionToConfirm, data, message) {
        $("#confirmationDialog").attr("confirm", actionToConfirm).attr("data", data);
        $("#confirmationDialog .modal-body").html(message);
        $("#confirmationDialog").modal('show');
    }

function HideModal() {
    $(".modal").modal('hide');
} 

function AnimateSubmitButton(selector, animate) {
    if (animate === true) {
        $(selector).html("<img src='/images/wait2.svg' style ='align: center' height='24px' width='" + $(selector).width() + "'>").prop("disabled", true);
    } else {
        $(selector).html($(selector).attr("data-name")).prop("disabled", false);
    }
}  

$(document).ready(function() {
    $("body").tooltip({ selector: '[data-tooltip=show]' });

    $(".fa-cog").mouseover(function () {
        $(this).toggleClass("fa-spin");
    }).mouseout(function () { $(this).toggleClass("fa-spin"); });

    $('.modal').on('shown.bs.modal', function () {
        $(this).find('[autofocus]').focus(); 
    }); 
});

