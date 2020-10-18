import { Component, OnInit, Inject } from '@angular/core';
import { BlogPost } from '../blog-post/shared/blogpost.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin-edit-blog-posts',
  templateUrl: './admin-edit-blog-posts.component.html',
  styleUrls: ['./admin-edit-blog-posts.component.css']
})
export class AdminEditBlogPostsComponent implements OnInit {
  blogPosts: BlogPost[];
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.http.get<BlogPost[]>(this.baseUrl + 'BlogPost/GetAllBlogPost').subscribe(result => {
      this.blogPosts = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  Delete(id: string)
  {
    const index = this.blogPosts.map(bp => bp.id).indexOf(id);
    if (index > -1) {
      this.blogPosts.splice(index, 1);
    }
    this.http.get<BlogPost>(this.baseUrl + 'BlogPost/Delete/' + id).subscribe(result => {
      console.log("deleted:" + result);
    }, error => console.error(error));
  }

}
