import { TestBed } from '@angular/core/testing';

import { UserSettingsService } from './location.service';

describe('LocationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserSettingsService = TestBed.get(UserSettingsService);
    expect(service).toBeTruthy();
  });
});
