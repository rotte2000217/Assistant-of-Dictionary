import { Component, OnInit, Input } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Label } from 'ng2-charts';

import { WordCounts } from '../models';

@Component({
    selector: 'home-chart',
    templateUrl: './chart.component.html',
    styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {
    chartType: string = 'bar';
    chartOptions: ChartOptions = {
        scales: {
            yAxes: [
                {
                    ticks: {
                        beginAtZero: true
                    }
                }
            ]
        }
    };

    chartData: Array<ChartDataSets>;
    chartLabels: Array<Label>;

    @Input() chartName: string;
    @Input() wordCounts: Array<WordCounts>;

    constructor() {
        this.chartData = [{ data: null, label: null }];
    }

    ngOnInit(): void {
        this.chartData[0].label = this.chartName;

        const data = [];
        const labels = [];
        this.wordCounts.forEach(({ letter, wordCount }) => {
            data.push(wordCount);
            labels.push(letter);
        });

        this.chartData[0].data = data;
        this.chartLabels = labels;
    }

}
