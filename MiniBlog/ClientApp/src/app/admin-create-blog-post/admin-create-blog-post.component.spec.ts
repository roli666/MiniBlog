import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCreateBlogPostComponent } from './admin-create-blog-post.component';

describe('AdminCreateBlogPostComponent', () => {
  let component: AdminCreateBlogPostComponent;
  let fixture: ComponentFixture<AdminCreateBlogPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminCreateBlogPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminCreateBlogPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
