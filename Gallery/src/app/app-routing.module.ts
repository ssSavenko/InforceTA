import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { FeedComponent } from './feed/feed.component'; 
import { AlbumComponent } from './album/album.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component'; 

const appRoutes: Routes = [ 
  { path: '', redirectTo: '/feed', pathMatch: 'full' },
  { path: 'feed', component: FeedComponent },
  { path: 'album/:id',  component: AlbumComponent },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { 

  
}
