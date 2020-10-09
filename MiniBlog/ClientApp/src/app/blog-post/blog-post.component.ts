import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.css']
})
export class BlogPostComponent implements OnInit {
  public blogPosts: BlogPost[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<BlogPost[]>(baseUrl + 'BlogPost').subscribe(result => {
      this.blogPosts = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}

interface BlogPost {
  Title: string;
  Content: string;
}
