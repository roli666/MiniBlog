import { Component, OnInit } from '@angular/core';
import { faFacebook, faTwitter, faInstagram, faGoogle } from '@fortawesome/free-brands-svg-icons';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isAuthenticated: Observable<boolean>;
  userName: Observable<string>;
  isExpanded = false;
  faFacebook = faFacebook;
  faTwitter = faTwitter;
  faInstagram = faInstagram;
  faGoogle = faGoogle;

  constructor(private authorizeService: AuthorizeService) {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
