import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../environments/environment';
import {
    AllStatistics,
    LetterStatistics,
    LongestWords,
    ShortestWords,
    WordCounts
} from './models';

@Injectable({
    providedIn: 'root'
})
export class DictionaryService {
    static DICTIONARY_API_URL: string = environment.baseUrl + '/api/dictionary';

    constructor(private httpClient: HttpClient) { }

    getAllWordCounts(): Observable<WordCounts[]> {
        const url = `${DictionaryService.DICTIONARY_API_URL}/all-letters`;
        return this.httpClient
            .get<LetterStatistics[]>(url)
            .pipe(
                map(array => array.map(j => ({ letter: j.letter, wordCount: j.numberWordsBeginningWith })))
            );
    }

    getTopWordCounts(howMany: number): Observable<WordCounts[]> {
        const url = `${DictionaryService.DICTIONARY_API_URL}/top-letters/${howMany}`;
        return this.httpClient
            .get<LetterStatistics[]>(url)
            .pipe(
                map(array => array.map(j => ({ letter: j.letter, wordCount: j.numberWordsBeginningWith })))
            );
    }

    getAllLetterStatistics(): Observable<AllStatistics> {
        const url = `${DictionaryService.DICTIONARY_API_URL}/all-statistics`;
        return this.httpClient
            .get<LetterStatistics[]>(url)
            .pipe(
                map(array => array.reduce((result: AllStatistics, current) =>
                    ({...result, [current.letter]: current}), {})
                )
            );
    }

    getTopFiftyLongest(): Observable<LongestWords> {
        const url = `${DictionaryService.DICTIONARY_API_URL}/longest-words/50`;
        return this.httpClient
            .get<LongestWords>(url);
    }

    getTopFiftyShortest(): Observable<ShortestWords> {
        const url = `${DictionaryService.DICTIONARY_API_URL}/shortest-words/50`;
        return this.httpClient
            .get<ShortestWords>(url);
    }
}
