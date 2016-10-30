import { Component } from '@angular/core';

import { Word } from './word';

const WORDS: Word[] = [
    { name: 'pizza' },
    { name: 'taco' }
]

@Component({
    moduleId: module.id,
    selector: 'word-list',
    templateUrl: 'word-list.component.html'
})
export class WordListComponent {
    words: Word[] = WORDS;
}