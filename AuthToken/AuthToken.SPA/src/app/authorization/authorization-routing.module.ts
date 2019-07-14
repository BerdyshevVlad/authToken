import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthorizationComponent } from "./authorization.component";
import { LogInComponent } from "./login/login.component";

const routes: Routes = [
  { path: "", redirectTo: "/authorization/login", pathMatch: "full" },
  {
    path: "",
    component: AuthorizationComponent,
    children: [
      { path: "login", component: LogInComponent }
    ]
  }
];
@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorizationRoutingModule { }
