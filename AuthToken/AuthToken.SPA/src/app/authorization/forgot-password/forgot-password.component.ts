import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/Authentication.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  public forgotPasswordForm: FormGroup;

  constructor(private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit() {
    this.forgotPasswordForm = this.formBuilder.group({
      email: [
        "",
        [
          Validators.required,
          Validators.pattern(
            "^[a-zA-Z0-9]+([-._]?[a-zA-Z0-9]+)*@[a-z0-9]+[-]?([-]?[a-z0-9])+[.]([a-z]{2,})$"
          )
        ]
      ]
    });
  }

  private forgotPassword() {
    if (this.forgotPasswordForm.invalid) {
      this.toastr.error("Email is incorrect", "Error occured!", {
      });
      return;
    }

    this.authenticationService
      .forgotPassword(this.forgotPasswordForm.value.email)
      .subscribe(() => {
        this.router.navigate(["/authorization/login"]);
        this.toastr.error("Check your email. We have sent confirmation letter.", "Error occured!", {
        });
        },
        error => {
          throw error;
        }
      );
  }

}
