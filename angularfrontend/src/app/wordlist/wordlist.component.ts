import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'home-wordlist',
    templateUrl: './wordlist.component.html',
    styleUrls: ['./wordlist.component.css']
})
export class WordListComponent implements OnInit {

    @Input() wordListTitle: string;
    @Input() wordList: Array<string>;

    constructor() { }

    ngOnInit(): void {
    }

}
