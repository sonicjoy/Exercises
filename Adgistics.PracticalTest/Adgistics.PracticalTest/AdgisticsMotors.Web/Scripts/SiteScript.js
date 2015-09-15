$(function () {
    $('#btnPrepareReports').click(function () {
        $(this).prop('disabled', true);
        PrepareReports();
    });

    $('#btnGetTopPerformer').click(function () {
        GetReports('top_performer');
    });

    $('#btnGetLowStocker').click(function () {
        GetReports('low_stocker');
    });

    function PrepareReports() {
        $.ajax({
            url: '/api/report/preparereports',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $('#report').html(data);
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }

    function GetReports(type) {
        $.ajax({
            url: '/api/report/getreport/type',
            type: 'GET',
            dataType: 'json',
            success: function (data) {

            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }


    // Declare a proxy to reference the hub.
    var data = $.connection.dataHub;
    // Create a function that the hub can call to broadcast messages.
    data.client.addQueueStatusToPage = function (status) {
        $('#status').html(status);
    };

    data.client.completeDataCollection = function () {
        $('#btnPrepareReports').prop('disabled', false);
        $('#btnGetTopPerformer').prop('disabled', true);
        $('#btnGetLowStocker').prop('disabled', true);
    }
    // Start the connection.
    $.connection.hub.start();
});