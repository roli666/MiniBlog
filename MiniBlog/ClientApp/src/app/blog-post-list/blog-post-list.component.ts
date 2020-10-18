import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BlogPost } from '../blog-post/shared/blogpost.model';
import { map } from 'rxjs/operators';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-blog-post-list',
  templateUrl: './blog-post-list.component.html',
  styleUrls: ['./blog-post-list.component.css']
})
export class BlogPostListComponent implements OnInit {

  isAuthenticated: Observable<boolean>;
  blogPosts: Observable<string[]>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private authorizeService: AuthorizeService) {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.blogPosts = http.get<BlogPost[]>(baseUrl + 'BlogPost/GetAllBlogPost').pipe(map(bps => bps.map(bp => bp.id)));
  }

  ngOnInit() {
  }

}
