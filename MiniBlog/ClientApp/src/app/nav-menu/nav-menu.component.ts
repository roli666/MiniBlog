import { Component } from '@angular/core';
import { faFacebook, faTwitter, faInstagram, faGoogle } from '@fortawesome/free-brands-svg-icons';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { map } from 'rxjs/operators';
import { ImageService } from '../../image-service/image-service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isAuthenticated: Observable<boolean>;
  isAdmin: Observable<boolean>;
  userName: Observable<string>;
  userAvatar: Observable<string>;
  isExpanded = false;
  faFacebook = faFacebook;
  faTwitter = faTwitter;
  faInstagram = faInstagram;
  faGoogle = faGoogle;

  constructor(private authorizeService: AuthorizeService, private imageService: ImageService) {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.isAdmin = this.authorizeService.isAdmin();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.userAvatar = this.imageService.getUserAvatar().pipe(map(img => img && img.imagePath));
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
