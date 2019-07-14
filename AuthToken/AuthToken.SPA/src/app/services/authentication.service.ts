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
      //.pipe(
      //  map(response => {
      //    if (response.token && response.user) {
      //      const user = <any>{
      //        token: response.token,
      //        user: response.user
      //      };

      //      return user;
      //    }
      //  })
      //);
  }

}
