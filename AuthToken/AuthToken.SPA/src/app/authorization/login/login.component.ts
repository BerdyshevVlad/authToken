import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/Authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LogInComponent implements OnInit {

  private userData: any;

  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.login();
  }

  private login() {
    this.authenticationService.login("berdyshev1997@gmail.com", "Qwe123!!").subscribe((data) => {
      this.userData = data;
      console.log(this.userData);
    });
  }

}
