import {BrowserModule} from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {EventListComponent} from './pages/event-list/event-list.component';
import {EventEditComponent} from './pages/event-edit/event-edit.component';
import {AboutComponent} from './pages/about/about.component';
import {ContactComponent} from './pages/contact/contact.component';
import {TopMenuComponent} from './components/top-menu/top-menu.component';
import {RouterModule, Routes} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CoreModule} from '../core/core.module';
import {HomeComponent} from './pages/home/home.component';
import {SortedColumnComponent} from './components/sorted-column/sorted-column.component';
import {NoDataFoundComponent} from './components/no-data-found/no-data-found.component';
import {PaginationComponent} from './components/pagination/pagination.component';


const appRoutes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'events',
    component: EventListComponent
  },
  {
    path: 'events/new',
    component: EventEditComponent
  },
  {
    path: 'events/edit/:id',
    component: EventEditComponent
  },
  {path: 'about', component: AboutComponent},
  {path: 'contact', component: ContactComponent},

];

@NgModule({
  declarations: [
    AppComponent,
    EventListComponent,
    EventEditComponent,
    AboutComponent,
    ContactComponent,
    TopMenuComponent,
    HomeComponent,
    SortedColumnComponent,
    NoDataFoundComponent,
    PaginationComponent,
  ],
  imports: [
    BrowserModule, RouterModule, HttpClientModule, FormsModule, CoreModule, ReactiveFormsModule,
    RouterModule.forRoot(appRoutes, {enableTracing: false}),
  ],
  providers: [{provide: LOCALE_ID, useValue: 'en-au'}],
  bootstrap: [AppComponent]
})
export class AppModule {
}
