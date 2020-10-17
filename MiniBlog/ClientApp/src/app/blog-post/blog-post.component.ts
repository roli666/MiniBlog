import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BlogPost } from './shared/blogpost.model';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.css']
})
export class BlogPostComponent implements OnInit {
  private http: HttpClient;
  private baseUrl: string;
  private blogPost: Observable<BlogPost>;
  blogPostTitle: Observable<string>;
  blogPostContent: Observable<string>;

  constructor(private route: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    const blogPostId = this.route.snapshot.paramMap.get('id');
    this.blogPost = this.http.get<BlogPost>(this.baseUrl + 'BlogPost/Post/' + blogPostId);
    this.blogPostTitle = this.blogPost.pipe(map(bp => bp && bp.title))
    this.blogPostContent = this.blogPost.pipe(map(bp => bp && bp.content))
  }

}
