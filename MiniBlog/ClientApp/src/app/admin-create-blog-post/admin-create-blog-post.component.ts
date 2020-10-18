import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BlogPost } from '../blog-post/shared/blogpost.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-admin-create-blog-post',
  templateUrl: './admin-create-blog-post.component.html',
  styleUrls: ['./admin-create-blog-post.component.css']
})
export class AdminCreateBlogPostComponent implements OnInit {
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
    if (blogPostId) {
      this.blogPost = this.http.get<BlogPost>(this.baseUrl + 'BlogPost/Post/' + blogPostId);
      this.blogPostTitle = this.blogPost.pipe(map(bp => bp && bp.title))
      this.blogPostContent = this.blogPost.pipe(map(bp => bp && bp.content))
    }
  }

  onSubmit() {
    //TODO
  }
}
