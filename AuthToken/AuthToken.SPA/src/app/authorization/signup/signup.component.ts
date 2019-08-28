import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/Authentication.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  public signupForm: FormGroup = new FormGroup({
    email: new FormControl("", [
      Validators.required,
      Validators.pattern(
        "^[a-zA-Z0-9]+([-._]?[a-zA-Z0-9]+)*@[a-z0-9]+[-]?([-]?[a-z0-9])+[.]([a-z]{2,})$"
      )
    ]),
    password: new FormControl("", [Validators.required]),
    confirmPassword: new FormControl("", [Validators.required])
  });

  constructor(private authenticationService: AuthenticationService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
  }

  private signup() {
    if (this.signupForm.invalid) {
      this.toastr.error("Enter valid email or password", "Error occured!", {
      });
      return;
    }
    if (this.signupForm.value.password != this.signupForm.value.confirmPassword) {
      this.toastr.error("Passwords do not match", "Error occured!", {
      });
      return;
    }

    this.authenticationService.signup(this.signupForm.value.email, this.signupForm.value.password).subscribe(() => {
       console.log("Registration succeeded.");
       },
       error => {
         throw error;
       });
  }

}
