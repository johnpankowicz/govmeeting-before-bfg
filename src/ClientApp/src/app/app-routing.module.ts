import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component'
import { AboutComponent } from './about/about.component';
import { ViewMeetingComponent } from './viewmeeting/viewmeeting.component'
import { AddtagsComponent } from './addtags/addtags.component'
import { FixasrComponent } from './fixasr/fixasr.component'
import { VolunteerComponent } from './volunteer/volunteer.component'
import { HomedemoComponent } from './temppages/homedemo.component'
import { ServerdemoComponent } from './temppages/serverdemo.component'
import { OtherfeaturesComponent } from './temppages/otherfeatures.component'
import { SearchComponent } from './temppages/search.component'
import { RegisterGovComponent } from './temppages/registergov.component'

const routes: Routes = [

  { path: '', redirectTo: 'homedemo', pathMatch: 'full' },
  { path: 'homedemo', component: HomedemoComponent },
  { path: 'about', component: AboutComponent },
  { path: 'viewmeeting', component: ViewMeetingComponent },
  { path: 'addtags', component: AddtagsComponent },

  { path: 'fixasr', component: FixasrComponent },
  //{ path: 'fixasr', loadChildren: '../fixasr/fixasr.module#FixasrModule' },

  { path: 'home', component: HomeComponent },
  { path: 'volunteer', component: VolunteerComponent },
  { path: 'serverdemo', component: ServerdemoComponent },
  { path: 'otherfeatures', component: OtherfeaturesComponent },
  { path: 'search', component: SearchComponent },
  { path: 'registergov', component: RegisterGovComponent },
  //{ path: 'matsamp', component: MatsampComponent },
  { path: '**', redirectTo: 'homedemo' }

];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
