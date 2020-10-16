import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Image } from '../app/blog-post/shared/image.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getUserAvatar() {
    return this.http.get<Image>(this.baseUrl + "Image");
  }
}
