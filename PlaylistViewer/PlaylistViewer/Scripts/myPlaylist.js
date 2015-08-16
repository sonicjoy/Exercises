var app = angular.module('myPlaylist', []);
app.controller('playlistViewerCtrl', function ($scope, $http) {
    $scope.getPlaylist = function (url) {
        var getPlaylistPath = "api/Playlist/Get/";
        $scope.playlist = undefined;
        $scope.error = undefined;
        if (url == "") url = undefined;
        $http({
            url: getPlaylistPath,
            method: "GET",
            params: { url: url }
        }).success(function (response) { $scope.playlist = response; })
        .error(function (response) { $scope.error = response; });
    }
});
