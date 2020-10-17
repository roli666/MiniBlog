import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BlogPost } from '../../blog-post/shared/blogpost.model';
import { faUser, faCalendar, faComment, faArrowRight } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-blog-post-list-element',
  templateUrl: './blog-post-list-element.component.html',
  styleUrls: ['./blog-post-list-element.component.css']
})
export class BlogPostListElementComponent implements OnInit {
  private http: HttpClient;
  private baseUrl: string;
  private blogPost: Observable<BlogPost>;
  blogPostTitle: Observable<string>;
  blogPostContent: Observable<string>;
  blogPostImage: Observable<string>;
  blogPostId: Observable<string>;
  blogPostCategory: Observable<string>;
  blogPostAuthor: Observable<string>;
  blogPostCreated: Observable<Date>;
  blogPostCommentCount: Observable<number>;
  @Input('blogpost-id') id: string;
  faUser = faUser;
  faCalendar = faCalendar;
  faComment = faComment;
  faArrowRight = faArrowRight;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.blogPost = this.http.get<BlogPost>(this.baseUrl + 'BlogPost/Post/' + this.id);
    this.blogPostTitle = this.blogPost.pipe(map(bp => bp && bp.title))
    this.blogPostContent = this.blogPost.pipe(map(bp => bp && bp.content))
    this.blogPostImage = this.blogPost.pipe(map(bp => bp && bp.backgroundImage))
    this.blogPostId = this.blogPost.pipe(map(bp => bp && bp.id))
    this.blogPostCategory = this.blogPost.pipe(map(bp => bp && bp.category))
    this.blogPostAuthor = this.blogPost.pipe(map(bp => bp && bp.createdBy))
    this.blogPostCreated = this.blogPost.pipe(map(bp => bp && bp.createdOn))
    this.blogPostCommentCount = this.blogPost.pipe(map(bp => bp && bp.commentCount))
  }

}
