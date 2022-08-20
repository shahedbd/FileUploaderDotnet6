$(document).ready(function () {
    document.title = 'File Upload';

    $("#tblAttachmentFile").DataTable({
        paging: true,
        select: true,
        "order": [[0, "desc"]],
        dom: 'Blfrtip',


        buttons: [
            'pageLength',
        ],

        "processing": true,
        "serverSide": true,
        "filter": true, //Search Box
        "orderMulti": false,
        "stateSave": true,

        "ajax": {
            "url": "/AttachmentFile/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            { "data": "Id", "name": "Id" },
            {
                data: "ContentType", "name": "ContentType", render: function (data, type, row) {
                    return "<a href='#' onclick=Details('" + row.Id + "');>" + row.ContentType + "</a>";
                }
            },
            { "data": "FileName", "name": "FileName" },
            { "data": "Length", "name": "Length" },
            { "data": "CreatedDate", "name": "CreatedDate" },
            
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-success btn-sm' onclick=DownloadFile('" + row.Id + "'); >Download</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-sm' onclick=Delete('" + row.Id + "'); >Delete</a>";
                }
            }
        ],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

});

