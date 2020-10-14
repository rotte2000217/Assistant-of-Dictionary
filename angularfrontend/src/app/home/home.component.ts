import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';

import {
    AllStatistics,
    LongestWords,
    ShortestWords,
    WordCounts
} from '../models';
import { DictionaryService } from '../dictionary.service';

interface LoadingError {
    failed: boolean;
    error: Error;
}

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    static TOP_FIVE: number = 1;
    static ALL_OF_THEM: number = 2;
    public constants = HomeComponent;

    alphabet: Array<string> = 'abcdefghijklmnopqrstuvwxyz'.split('');
    topChartLabel: string = 'Top Five';
    allChartLabel: string = 'All Words';
    shortListLabel: string = 'Top 50 Shortest Words';
    longListLabel: string = 'Top 50 Longest Words';
    currentLetter: string;
    errorInfo: LoadingError;
    graphThis: number;
    loading: boolean;

    allWordCounts: Array<WordCounts>;
    topFiveWordCounts: Array<WordCounts>;
    letterStats: AllStatistics;
    longestWords: LongestWords;
    shortestWords: ShortestWords;

    constructor(
        private dictionaryService: DictionaryService
    ) {
        this.currentLetter = this.alphabet[0];
        this.errorInfo = { failed: false, error: null };
        this.graphThis = HomeComponent.TOP_FIVE;
        this.loading = true;
    }

    ngOnInit(): void {
        forkJoin({
            allCounts: this.dictionaryService.getAllWordCounts(),
            topCounts: this.dictionaryService.getTopWordCounts(5),
            allStats: this.dictionaryService.getAllLetterStatistics(),
            longestWords: this.dictionaryService.getTopFiftyLongest(),
            shortestWords: this.dictionaryService.getTopFiftyShortest()
        })
        .subscribe(data => {
            // Check for failed.
            let failures = false;
            for (const [k, v] of Object.entries(data)) {
                if (v === undefined) {
                    console.error(`API ERROR: ${k}`);
                    failures = true;
                }
            }
            if (failures) {
                this.errorInfo.failed = true;
                this.errorInfo.error = new Error('Unable to contact server!');
                return;
            }

            this.allWordCounts = data.allCounts;
            this.topFiveWordCounts = data.topCounts;
            this.letterStats = data.allStats;
            this.longestWords = data.longestWords;
            this.shortestWords = data.shortestWords;

            this.loading = false;
        });
    }

    onSpecificLetterChange(e: any) {
        this.currentLetter = e.target.value;
    }

    onWhatToGraphChange(e: any) {
        this.graphThis = Number.parseInt(e.target.value);
    }
}
