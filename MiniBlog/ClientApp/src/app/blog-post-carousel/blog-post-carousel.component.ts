import { Component, OnInit, Inject } from '@angular/core';
import { BlogPost } from '../blog-post/shared/blogpost.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-blog-post-carousel',
  templateUrl: './blog-post-carousel.component.html',
  styleUrls: ['./blog-post-carousel.component.css']
})
export class BlogPostCarouselComponent implements OnInit {
  private http: HttpClient;
  private baseUrl: string;
  public latestBlogPosts: BlogPost[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.GetBlogPosts();
  }

  ngOnInit() {
  }

  GetBlogPosts() {
    this.http.get<BlogPost[]>(this.baseUrl + 'BlogPost').subscribe(result => {
      this.latestBlogPosts = result;
    }, error => console.error(error));
  }

}
