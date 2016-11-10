import { Component, OnInit } from '@angular/core';

import { Word } from './word';
import { WordService } from './word.service';

@Component({
    moduleId: module.id,
    selector: 'word-list',
    templateUrl: 'word-list.component.html',
    providers: [WordService]
})
export class WordListComponent implements OnInit {
    words: Word[];

    constructor(private wordService: WordService) {

    }

    ngOnInit(): void {
        this.wordService
            .getAll()
            .then(words => this.words = words);
    }

    play(word: Word): void {
        console.log(`${word.name} has been played.`)
    }

}