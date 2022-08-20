
var loadCommonModal = function (url) {
    $("#SmallModalDiv").load(url, function () {
        $("#SmallModal").modal("show");
    });
};

var loadMediumModal = function (url) {
    $("#MediumModalDiv").load(url, function () {
        $("#MediumModal").modal("show");
    });
};

var loadBigModal = function (url) {
    $("#BigModalDiv").load(url, function () {
        $("#BigModal").modal("show");
    });
};

var loadExtraBigModal = function (url) {
    $("#ExtraBigModalDiv").load(url, function () {
        $("#ExtraBigModal").modal("show");
    });
};


