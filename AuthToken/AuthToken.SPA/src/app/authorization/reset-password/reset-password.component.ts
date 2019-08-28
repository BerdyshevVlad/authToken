import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/Authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  private code: string;
  private userId: string;

  public resetPasswordForm: FormGroup = new FormGroup({
    password: new FormControl("", [Validators.required])
  });

  constructor(private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.code = this.route.snapshot.queryParams["code"];
    this.userId = this.route.snapshot.queryParams["userId"];

  }


  private resetPassword() {
    if (this.resetPasswordForm.invalid) {
      this.toastr.error("Enter valid password", "Error occured!", {
      });
      return;
    }

    let resetForm = {
      password: this.resetPasswordForm.value.password,
      code: this.code,
      userId: this.userId
    };

    this.authenticationService
      .resetPassword(resetForm)
      .subscribe(
        (data: any) => {
          this.toastr.success("Password is reseted!", "Succeeded!", {
          });
          this.router.navigate(['/']);
        },
        error => {
          throw error;
        }
      );
  }

}
