import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Observable } from "rxjs/Observable";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import "rxjs/add/operator/map";

@Injectable()
export class AuthService {
  baseUrl = "http://localhost:5000/api/auth/";

  userToken: any;
  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http
      .post<string>(this.baseUrl + "login", model,  this.getHeaders()  )
      .map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem("token", user.tokenString);
          this.userToken = user.tokenString;
        }
      });
  }

  register(model: any) {
    return this.http.post(this.baseUrl + "register", model, this.getHeaders());
  }

  private getHeaders() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
      })
    };

    return httpOptions;
  }
}
