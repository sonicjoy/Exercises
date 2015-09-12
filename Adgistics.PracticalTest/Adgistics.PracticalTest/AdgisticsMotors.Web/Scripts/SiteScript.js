$(function () {
    $('#btnPrepareReports').click(function () {
        PrepareReports();
    });

    function PrepareReports() {
        jQuery.support.cors = true;
        $.ajax({
            url: '/api/report/preparereports',
            type: 'GET',
            dataType: 'json',
            success: function () {
                
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }
});