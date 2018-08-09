import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {
  MatFormFieldModule,
  MatAutocompleteModule,
  MatInputModule,
  MatCardModule,
  MatToolbarModule,
  MatButtonModule,
  MatCheckboxModule,
  MatIconModule,
  MatMenuModule,
  MatSlideToggleModule,
  MatListModule
} from '@angular/material';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AirportListComponent } from './airport/airport-list-component';
import { AdminComponent } from './admin/admin.component';
import { LoginComponent } from './login/login.component';
import { BaseService } from './services/base-service';
import { AirportService } from './services/airport-service';
import { FlightService } from './services/flight-service';
import { StatsService } from './services/stats-service';
import { AuthService } from './services/auth-service';
import { AuthInterceptor } from './services/auth-interceptor-service'

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AirportListComponent,
    AdminComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatInputModule,
    MatCardModule,
    MatToolbarModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatMenuModule,
    MatSlideToggleModule,
    MatListModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'admin', component: AdminComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent, pathMatch: 'full' }

    ])
  ],
  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl },
    BaseService,
    AirportService,
    FlightService,
    StatsService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
