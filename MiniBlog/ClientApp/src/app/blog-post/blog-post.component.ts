import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BlogPost } from './shared/blogpost.model';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.css']
})
export class BlogPostComponent implements OnInit {
  private http: HttpClient;
  private baseUrl: string;
  public blogPosts: BlogPost[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.GetBlogPosts();
  }

  ngOnInit() {
  }

  GetBlogPosts() {
    this.http.get<BlogPost[]>(this.baseUrl + 'BlogPost').subscribe(result => {
      this.blogPosts = result;
    }, error => console.error(error));
  }

}
