import { TestBed } from '@angular/core/testing';
import { Warnings } from './services/warning.service';

describe('Warnings', () => {
  let service: Warnings;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Warnings);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
