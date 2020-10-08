import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router }  from '@angular/router';

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

    constructor(
        private router: Router,
        private formBuilder: FormBuilder
    ) {
        this.uploadForm = this.formBuilder.group({
            filePicker: new FormControl('', Validators.required)
        });

        this.uploadStatus = UploadComponent.UPLOAD_NOT_STARTED;
    }

    ngOnInit(): void {
    }

    onUploadFormSubmit(e: any) {
        e.preventDefault();

        if(this.uploadForm.valid) {
            this.uploadStatus = UploadComponent.UPLOAD_PENDING;
        }
        else {
            this.uploadStatus = UploadComponent.UPLOAD_FAILED;
        }
    }

    onUploadFormGoBackClick(e: any) {
        e.preventDefault();
        this.navigateToHome();
    }

    private navigateToHome() {
        return Promise.resolve(this.router.navigateByUrl(''));
    }
}
