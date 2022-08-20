var Details = function (id) {
    var url = "/AttachmentFile/Details?id=" + id;
    $('#titleBigModal').html("File Details");
    loadBigModal(url);
};

var DownloadFile = function (id) {
    location.href = "/AttachmentFile/DownloadFile?id=" + id;
};

var ClearSelection = function () {
    $("#AttachmentFile").val(null);
}

var AddEdit = function (id) {
    var url = "/AttachmentFile/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Attachment File");
    }
    else {
        $('#titleBigModal').html("Upload Files");
    }
    loadBigModal(url);
};

var AddNewAttachmentFile = function () {
    if ($("#AttachmentFile").val() === "" || $("#AttachmentFile").val() === null) {
        Swal.fire({
            title: 'Choose Files field can not be null or empty.',
            type: "warning",
            onAfterClose: () => {
                $("#AttachmentFile").focus();
            }
        });
        return;
    }

    var files = $("#AttachmentFile").get(0).files;
    var fileData = new FormData();
    for (var i = 0; i < files.length; i++) {
        fileData.append("AttachmentFile", files[i]);
    }

    $("#btnUploadFile").val("Uploading...");
    $('#btnUploadFile').attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "/AttachmentFile/AddNewAttachmentFile",
        data: fileData,
        processData: false,
        contentType: false,
        success: function (result) {
            Swal.fire({
                title: result,
                type: "success",
                onAfterClose: () => {
                    $("#AttachmentFile").val(null);
                    $("#btnUploadFile").val("Upload File");
                    $('#btnUploadFile').removeAttr('disabled');

                    $("#divAlertArea").load(location.href + " #divAlertArea");
                    location.reload();
                }
            });
        }
    });
};

var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/AttachmentFile/Delete?id=" + id,
                success: function (result) {
                    var message = "File has been deleted successfully. File ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        type: "success",
                        onAfterClose: () => {
                            location.reload();
                        }
                    });
                }
            });
        }
    });
};
