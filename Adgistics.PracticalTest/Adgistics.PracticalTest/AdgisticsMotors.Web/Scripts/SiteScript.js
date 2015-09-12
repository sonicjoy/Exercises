$(function () {
    $('#btnPrepareReports').click(function () {
        PrepareReports();
    });

    function PrepareReports() {
        $.get('/api/report/preparereports');
    }
});