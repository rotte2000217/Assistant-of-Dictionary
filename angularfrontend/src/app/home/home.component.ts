import { Component, OnInit } from '@angular/core';

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
    currentLetter: string;
    graphThis: number;

    constructor() {
        this.currentLetter = this.alphabet[0];
        this.graphThis = HomeComponent.TOP_FIVE;
    }

    ngOnInit(): void {
    }

    onSpecificLetterChange(e: any) {
        this.currentLetter = e.target.value;
    }

    onWhatToGraphChange(e: any) {
        this.graphThis = Number.parseInt(e.target.value);
    }
}
