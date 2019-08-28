import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, EventEmitter, Output } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

const constants = {
  sessionToken: "currentUser"
};

@Injectable({
  providedIn: "root"
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
    const loginForm = {
      Email: email,
      Password: password
    };

    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Credentials': 'true'
    });
    let options = { headers: headers };

    return this.http.post<any>(
      `http://localhost:1194/api/auth/login`,
      JSON.stringify(loginForm), options);
  }

  signup(email: string, password: string): Observable<any> {
    const signupForm = {
      Email: email,
      Password: password
    };

    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Credentials': 'true'
    });
    let options = { headers: headers };

    return this.http.post<any>(
      `http://localhost:1194/api/auth/signup`,
      JSON.stringify(signupForm), options);
  }


  resetPassword(resetForm:any) {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Credentials': 'true'
    });
    let options = { headers: headers };

    return this.http.post<any>(
      `http://localhost:1194/api/auth/resetPassword`,
      JSON.stringify(resetForm), options);
  }


  forgotPassword(email: string) {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Credentials': 'true'
    });
    let options = { headers: headers };

    let forgotPasswordForm = {
      email: email
    };

    return this.http.post<any>(
      `http://localhost:1194/api/auth/forgotPassword`,
      JSON.stringify(forgotPasswordForm), options);
  }

  emailConfirm(userId: string, code: string) {
    const obj = {
      userId: userId,
      code: code
    };

    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Credentials': 'true'
    });
    let options = { headers: headers };

    return this.http.post<any>(
      `http://localhost:1194/api/auth/confirmEmail`, JSON.stringify(obj), options);
  }
}
