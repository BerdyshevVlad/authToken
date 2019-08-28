import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/Authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-email-confirm',
  templateUrl: './email-confirm.component.html',
  styleUrls: ['./email-confirm.component.scss']
})
export class EmailConfirmComponent implements OnInit {

  private userId: string;
  private code: string;

  constructor(private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.userId = this.route.snapshot.queryParams["userId"];
    this.code = this.route.snapshot.queryParams["code"];

    this.emailConfirm();
  }

  private emailConfirm() {
    this.authenticationService
      .emailConfirm(this.userId, this.code)
      .subscribe(
        (data: any) => {
          if (data) {
            this.toastr.success(
              "Account was successfully activated",
              "Success!",
              { closeButton: true }
            );
            this.router.navigate(["/authorization/login"]);
          }
        },
        error => {
          throw error;
        }
      );
  }

}
