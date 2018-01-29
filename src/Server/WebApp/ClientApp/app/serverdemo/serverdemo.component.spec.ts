import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerdemoComponent } from './serverdemo.component';

describe('ServerdemoComponent', () => {
    let component: ServerdemoComponent;
  let fixture: ComponentFixture<ServerdemoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
        declarations: [ ServerdemoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
      fixture = TestBed.createComponent(ServerdemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
