import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router }  from '@angular/router';

import { DictionaryService } from '../dictionary.service';

@Component({
    selector: 'app-upload',
    templateUrl: './upload.component.html',
    styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
    static UPLOAD_NOT_STARTED: number = 0;
    static UPLOAD_PENDING: number  = 1;
    static UPLOAD_SUCCESS: number  = 2;
    static UPLOAD_FAILED: number  = 3;
    public constants = UploadComponent;

    uploadForm: FormGroup;
    uploadStatus: number;
    numWordsAdded: number;

    constructor(
        private router: Router,
        private formBuilder: FormBuilder,
        private dictionaryService: DictionaryService
    ) {
        this.uploadForm = this.formBuilder.group({
            filePicker: new FormControl('', Validators.required)
        });

        this.uploadStatus = UploadComponent.UPLOAD_NOT_STARTED;
        this.numWordsAdded = 0;
    }

    ngOnInit(): void {
    }

    onUploadFormSubmit(e: any) {
        e.preventDefault();

        if(this.uploadForm.valid) {
            this.uploadStatus = UploadComponent.UPLOAD_PENDING;

            // Accessing via `uploadForm` only gets the filename...
            const fileInput = e.target.children['FilePicker'];
            const uploadThis = fileInput.files[0];

            const contentData = new FormData();
            contentData.append('wordListFile', uploadThis);

            this.dictionaryService.postDictionaryList(contentData)
                .subscribe(wordsAdded => {
                    if (wordsAdded === undefined) {
                        this.uploadStatus = UploadComponent.UPLOAD_FAILED;
                        this.numWordsAdded = 0;
                        return;
                    }

                    this.uploadStatus = UploadComponent.UPLOAD_SUCCESS;
                    this.numWordsAdded = wordsAdded;
                });
        }
        else {
            this.uploadStatus = UploadComponent.UPLOAD_FAILED;
        }
    }

    onUploadFormGoBackClick(e: any) {
        e.preventDefault();
        this.navigateToHome();
    }

    onStatusMessageBtnClick() {
        this.navigateToHome();
    }

    private navigateToHome() {
        return Promise.resolve(this.router.navigateByUrl(''));
    }
}
