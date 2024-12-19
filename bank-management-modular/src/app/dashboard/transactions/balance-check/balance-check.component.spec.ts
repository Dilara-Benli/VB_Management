import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BalanceCheckComponent } from './balance-check.component';

describe('BalanceCheckComponent', () => {
  let component: BalanceCheckComponent;
  let fixture: ComponentFixture<BalanceCheckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BalanceCheckComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BalanceCheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
