import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/Authentication.service';
import { User } from '../../shared/models/user.model';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LogInComponent implements OnInit {

  private userData: User;

  public loginForm: FormGroup = new FormGroup({
    email: new FormControl("", [
      Validators.required,
      Validators.pattern(
        "^[a-zA-Z0-9]+([-._]?[a-zA-Z0-9]+)*@[a-z0-9]+[-]?([-]?[a-z0-9])+[.]([a-z]{2,})$"
      )
    ]),
    password: new FormControl("", [Validators.required])
  });

  constructor(private authenticationService: AuthenticationService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {

  }

  private async login() {
    //this.authenticationService.login("berdyshev1997@gmail.com", "Qwe123!!").subscribe((data: User) => {
    //  this.userData = data;
    //  console.log(this.userData);
    //});

    if (this.loginForm.invalid) {
      this.toastr.error("Enter valid email or password", "Error occured!", {
      });
      return;
    }

    this.userData = await this.authenticationService.login(this.loginForm.value.email, this.loginForm.value.password).toPromise();

    localStorage.setItem("user", JSON.stringify(this.userData));

    this.router.navigate(['/']);    //!!!!!!!!!!!!!!!!!!!
  }

}
