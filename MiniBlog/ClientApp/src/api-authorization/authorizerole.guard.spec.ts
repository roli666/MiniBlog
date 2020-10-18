import { TestBed, inject } from '@angular/core/testing';
import { AuthorizeRoleGuard } from './authorizerole.guard';

describe('AuthorizeRoleGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthorizeRoleGuard]
    });
  });

  it('should ...', inject([AuthorizeRoleGuard], (guard: AuthorizeRoleGuard) => {
    expect(guard).toBeTruthy();
  }));
});
