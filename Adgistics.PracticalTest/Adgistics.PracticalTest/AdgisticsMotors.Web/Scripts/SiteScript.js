$(function () {
    if ($('#total').html().length > 0) $('#btnPrepareReports').prop('disabled', true);
    $('#reportsReady').hide();
    $('#btnPrepareReports').click(function () {
        $(this).prop('disabled', true);
        $('#btnGetTopPerformer').prop('disabled', true);
        $('#btnGetLowStocker').prop('disabled', true);
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
                
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }

    function GetReports(reportType) {
        $.ajax({
            url: '/api/report/getreport',
            type: 'GET',
            data: { 'reportType' : reportType },
            dataType: 'json',
            success: function (report) {
                $('#tableContent').empty();
                $.each(report, function (intValue, currentElement) {
                    var currentRow = '<tr><td>' + currentElement.DealershipIdentifier + '</td><td>' + currentElement.AvailableStock + '</td><td>' + currentElement.TotalSales + '</td></tr>';
                    $('#tableContent').append(currentRow);
                });
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }


    // Declare a proxy to reference the hub.
    var data = $.connection.dataHub;
    var totalDealerships = 0;
    data.client.addTotalToPage = function (total) {
        $('#total').html(total + ' total dealerships');
        totalDealerships = total;
    };

    data.client.addQueueStatusToPage = function (completed, processing, failed) {
        if (totalDealerships > 0) {
            var completedPc = completed / totalDealerships * 100;
            var processingPc = processing / totalDealerships * 100;
            var failedPc = failed / totalDealerships * 100;

            $('#completedBar').width(completedPc + '%');
            $('#completedSpan').html(completed + ' completed');
            $('#completedPcSpan').html(completedPc.toFixed(2) + '% completed');

            $('#processingBar').width(processingPc + '%');
            $('#processingSpan').html(processing + ' in processing');

            $('#failedBar').width(failedPc + '%');
            $('#failedSpan').html(failed + ' failed');
        }
    };

    data.client.completeDataCollection = function () {
        $('#btnPrepareReports').prop('disabled', false);
        $('#btnGetTopPerformer').prop('disabled', false);
        $('#btnGetLowStocker').prop('disabled', false);
        $('#reportsReady').show();
    }
    // Start the connection.
    $.connection.hub.start();
});