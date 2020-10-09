import { Component, OnChanges, SimpleChanges, Input, Output, EventEmitter } from '@angular/core';

import { UploadComponent } from '../upload/upload.component';

@Component({
    selector: 'upload-statusmessage',
    templateUrl: './statusmessage.component.html',
    styleUrls: ['./statusmessage.component.css']
})
export class StatusMessageComponent implements OnChanges {
    isSuccessful: boolean;
    isFailed: boolean;
    isPending: boolean;

    @Input() currentStatus: number;
    @Input() wordsAdded: number;

    @Output() btnClick: EventEmitter<any>;

    constructor() {
        this.btnClick = new EventEmitter<any>();
        this.isSuccessful = false;
        this.isFailed = false;
        this.isPending = false;
    }

    ngOnChanges(changes: SimpleChanges): void {
        if ('currentStatus' in changes) {
            const status = changes['currentStatus'].currentValue;

            this.isSuccessful = this.isStatus(UploadComponent.UPLOAD_SUCCESS, status);
            this.isFailed = this.isStatus(UploadComponent.UPLOAD_FAILED, status);
            this.isPending = this.isStatus(UploadComponent.UPLOAD_PENDING, status);
        }
    }

    onOkayClick(e: any) {
        e.preventDefault();
        this.btnClick.emit(e);
    }

    private isStatus(expected: number, actual: number): boolean {
        return (expected === actual);
    }
}
