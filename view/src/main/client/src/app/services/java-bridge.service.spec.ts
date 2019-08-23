import { TestBed } from '@angular/core/testing';

import { JavaBridgeService } from './java-bridge.service';

describe('JavaBridgeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: JavaBridgeService = TestBed.get(JavaBridgeService);
    expect(service).toBeTruthy();
  });
});
