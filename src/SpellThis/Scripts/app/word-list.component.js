"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var word_service_1 = require('./word.service');
var WordListComponent = (function () {
    function WordListComponent(wordService) {
        this.wordService = wordService;
    }
    WordListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.wordService
            .getAll()
            .then(function (words) { return _this.words = words; });
    };
    WordListComponent.prototype.play = function (word) {
        console.log(word.name + " has been played.");
    };
    WordListComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'word-list',
            templateUrl: 'word-list.component.html',
            providers: [word_service_1.WordService]
        }), 
        __metadata('design:paramtypes', [word_service_1.WordService])
    ], WordListComponent);
    return WordListComponent;
}());
exports.WordListComponent = WordListComponent;
