import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthorizationComponent } from "./authorization.component";
import { LogInComponent } from "./login/login.component";
import { SignupComponent } from "./signup/signup.component";
import { ResetPasswordComponent } from "./reset-password/reset-password.component";
import { ForgotPasswordComponent } from "./forgot-password/forgot-password.component";
import { EmailConfirmComponent } from "./email-confirm/email-confirm.component";

const routes: Routes = [
  { path: "", redirectTo: "/authorization/login", pathMatch: "full" },
  {
    path: "",
    component: AuthorizationComponent,
    children: [
      { path: "login", component: LogInComponent },
      { path: "signup", component: SignupComponent },
      { path: "reset-password", component: ResetPasswordComponent },
      { path: "forgot-password", component: ForgotPasswordComponent },
      { path: "email-confirm", component: EmailConfirmComponent }
    ]
  }
];
@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorizationRoutingModule { }
