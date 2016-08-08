angular
    .module("productManagement")
    .controller("ProductEditCtrl",
                ProductEditCtrl);

function ProductEditCtrl(productResource) {
    var vm = this;
    vm.product = {};
    vm.message = '';

    productResource.get({ id: 5 },
        function (data) {
            vm.product = data;
            vm.originalProduct = angular.copy(data);
        },
        function (resp) {
            vm.message = resp.statusText + "\r\n";
            if (resp.data.exceptionMessage) {
                vm.message += resp.data.exceptionMessage;
            }
        });
    if (vm.product && vm.product.productId) {
        vm.title = "Edit: " + vm.product.productName;
    }
    else {
        vm.title = "New Product";
    }

    vm.submit = function () {
        vm.message = '';
        if (vm.product.productId) {
            vm.product.$update({ id: vm.product.productId },
                function (data) {
                    vm.message = "... Update complete";
                },
                function (resp) {
                    vm.message = resp.statusText + "\r\n";
                    if (resp.data.modelState) {
                        for (var key in resp.data.modelState) {
                            vm.message += resp.data.modelState[key] + "\r\n";
                        }
                    }
                    //if (resp.data.exceptionMessage) {
                    //    vm.message += resp.data.exceptionMessage;
                    //}
                });
        }
        else {
            vm.product.$save(
                function (data) {
                    vm.originalProduct = angular.copy(data);

                    vm.message = "... Save complete";
                },
                function (resp) {
                    vm.message = resp.statusText + "\r\n";
                    if (resp.data.exceptionMessage) {
                        vm.message += resp.data.exceptionMessage;
                    }
                });

        }
    };

    vm.cancel = function (editForm) {
        editForm.$setPristine();
        vm.product = angular.copy(vm.originalProduct);
        vm.message = '';
    };


}