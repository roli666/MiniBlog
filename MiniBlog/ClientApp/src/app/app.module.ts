import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { FooterComponent } from './footer/footer.component';
import { BlogPostCarouselComponent } from './blog-post-carousel/blog-post-carousel.component';
import { ImagePreloadDirective } from './image-preload.directive';
import { NgbPaginationModule, NgbAlertModule, NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { CommentsComponent } from './comments/comments.component';
import { BlogPostListComponent } from './blog-post-list/blog-post-list.component';
import { BlogPostListElementComponent } from './blog-post-list/blog-post-list-element/blog-post-list-element.component';

library.add(fas, fab);

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BlogPostComponent,
    FooterComponent,
    BlogPostCarouselComponent,
    ImagePreloadDirective,
    CommentsComponent,
    BlogPostListComponent,
    BlogPostListElementComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    FontAwesomeModule,
    NgbPaginationModule,
    NgbAlertModule,
    NgbCarouselModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'blogpost/:id', component: BlogPostComponent, canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
