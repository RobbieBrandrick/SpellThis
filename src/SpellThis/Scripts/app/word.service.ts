import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Word } from './word';

@Injectable()
export class WordService {

    private wordURL = 'http://localhost:32771/api/spellthis';
    private headers = new Headers([{ 'Content-Type': 'application/json' }, { 'Access-Control-Allow-Origin': '*' }]);

    constructor(private http: Http) {

    }

    getAll(): Promise<Word[]> {
        return this.http
            .get(this.wordURL)
            .toPromise()
            .then(response => response.json() as Word[])
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

}