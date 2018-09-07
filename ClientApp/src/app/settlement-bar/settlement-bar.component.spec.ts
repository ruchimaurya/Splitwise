import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SettlementBarComponent } from './settlement-bar.component';

describe('SettlementBarComponent', () => {
  let component: SettlementBarComponent;
  let fixture: ComponentFixture<SettlementBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SettlementBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SettlementBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
