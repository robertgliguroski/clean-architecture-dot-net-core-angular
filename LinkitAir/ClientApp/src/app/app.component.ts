import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { AuthService } from './services/auth-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  contextRoute: string;

  constructor(public auth: AuthService, public router: Router) {
  }

  logout(): boolean {
     if (this.auth.logout()) {
      this.router.navigate([""]);
    }
    return false;
  }

}
