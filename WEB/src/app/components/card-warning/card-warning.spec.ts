import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardWarning } from './card-warning';

describe('CardWarning', () => {
  let component: CardWarning;
  let fixture: ComponentFixture<CardWarning>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardWarning]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardWarning);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
