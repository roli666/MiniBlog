import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminEditBlogPostsComponent } from './admin-edit-blog-posts.component';

describe('AdminEditBlogPostsComponent', () => {
  let component: AdminEditBlogPostsComponent;
  let fixture: ComponentFixture<AdminEditBlogPostsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminEditBlogPostsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminEditBlogPostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
