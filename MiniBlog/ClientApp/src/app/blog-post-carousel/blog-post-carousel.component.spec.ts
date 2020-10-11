import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogPostCarouselComponent } from './blog-post-carousel.component';

describe('BlogPostCarouselComponent', () => {
  let component: BlogPostCarouselComponent;
  let fixture: ComponentFixture<BlogPostCarouselComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlogPostCarouselComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlogPostCarouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
