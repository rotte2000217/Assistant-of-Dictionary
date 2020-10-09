import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatusMessageComponent } from './statusmessage.component';

describe('StatusMessageComponent', () => {
  let component: StatusMessageComponent;
  let fixture: ComponentFixture<StatusMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatusMessageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StatusMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
