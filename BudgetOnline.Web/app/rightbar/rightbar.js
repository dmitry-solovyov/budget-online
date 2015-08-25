(function () {
    'use strict';

    var controllerId = 'rightbar';

    angular.module('app')
        .controller(controllerId, ['$location', '$route', '$q', 'common', rightbar]);

    function rightbar($location, $route, $q, common) {
        var logger = common.logger.getLogger(controllerId);

        var vm = this;
        
        vm.title = 'HELO!';
    }
})();