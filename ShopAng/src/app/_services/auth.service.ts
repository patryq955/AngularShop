import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Observable } from "rxjs/Observable";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpRequest } from "@angular/common/http";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/throw";

@Injectable()
export class AuthService {
  baseUrl = "http://localhost:5000/api/auth/";

  userToken: any;
  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http
      .post<string>(this.baseUrl + "login", model, this.getHeaders())
      .map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem("token", user.tokenString);
          this.userToken = user.tokenString;
        }
      }).catch(this.handleError);
  }

  register(model: any) {
    return this.http.post(this.baseUrl + "register", model, this.getHeaders()).catch(this.handleError);
  }

  private getHeaders() {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }
    return httpOptions;
  }

  private handleError(error: any){
    const applicationError = error.headers.get('Application-Error');
    if(applicationError){
      return Observable.throw(applicationError);
    }
    let modelStateError ='';
    if(error.error){
      for(const key in error.error){
        if(error.error[key]){
          modelStateError+= error.error[key]+'\n';
        }
      }
    }
    return Observable.throw(modelStateError || "Błąd serwera");
  }
}
