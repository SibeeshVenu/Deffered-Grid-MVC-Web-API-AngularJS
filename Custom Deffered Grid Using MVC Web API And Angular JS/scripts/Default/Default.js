(function () {
    'use strict';
    angular
      .module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache'])
      .controller('AppCtrl', function ($http, $timeout) {
          var DynamicItems = function () {
              this.loadedPages = {};
              this.numItems = 0;
              this.PAGE_SIZE = 10;
              this.fetchNumItems_();
          };
          DynamicItems.prototype.getItemAtIndex = function (index) {
              var pageNumber = Math.floor(index / this.PAGE_SIZE);
              var page = this.loadedPages[pageNumber];

              if (page) {
                  return page[index % this.PAGE_SIZE];
              } else if (page !== null) {
                  this.fetchPage_(pageNumber);
              }
          };
          DynamicItems.prototype.getLength = function () {
              return this.numItems;
          };
          DynamicItems.prototype.fetchPage_ = function (pageNumber) {
              this.loadedPages[pageNumber] = null;
              $timeout(angular.noop, 300).then(angular.bind(this, function () {
                  var thisObj = this;
                  this.loadedPages[pageNumber] = [];
                  var pageOffset = pageNumber * this.PAGE_SIZE;
                  var myData;
                  var url = '';
                  url = 'api/DataAPI/' + pageOffset;
                  $http({
                      method: 'GET',
                      url: url,
                  }).then(function successCallback(response) {
                      // this callback will be called asynchronously
                      // when the response is available
                      myData = JSON.parse(response.data);
                      pushLoadPages(thisObj, myData)
                  }, function errorCallback(response) {
                      console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                  });
                  function pushLoadPages(thisObj, servData) {
                      if (servData != undefined) {
                          for (var i = 0; i < servData.length; i++) {
                              thisObj.loadedPages[pageNumber].push(servData[i]);
                          }
                      }
                  }
              }));
          };
          DynamicItems.prototype.fetchNumItems_ = function () {
              $timeout(angular.noop, 300).then(angular.bind(this, function () {
                  this.numItems = 1000;
              }));
          };
          this.dynamicItems = new DynamicItems();
      });
})();