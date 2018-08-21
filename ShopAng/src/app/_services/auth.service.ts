import { Observable } from "rxjs/Observable";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/throw";
import { BehaviorSubject } from "rxjs";
import { JwtHelperService } from "@auth0/angular-jwt";
import { map } from "rxjs/operators";
import { environment } from "../../environments/environment";
import { User } from "../_models/user";

@Injectable()
export class AuthService {
  baseUrl = environment.apiUrl + "auth/";

  private jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  photoUrl = new BehaviorSubject<string>("../../assets/user.png");
  currentPhoto = this.photoUrl.asObservable();

  constructor(private http: HttpClient) {}

  changeMemberPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  login(model: any) {
    return this.http
      .post<string>(this.baseUrl + "login", model, this.getHeaders())
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem("token", user.tokenString);
            localStorage.setItem("user", JSON.stringify(user.user));
            this.decodedToken = this.jwtHelper.decodeToken(user.tokenString);
            this.currentUser = user.user;
            this.changeMemberPhoto(this.currentUser.urlPhoto);
          }
        })
      )
      .catch(this.handleError);
  }

  register(model: any) {
    return this.http
      .post(this.baseUrl + "register", model, this.getHeaders())
      .catch(this.handleError);
  }

  loggedIn() {
    let token = localStorage.getItem("token");

    return !this.jwtHelper.isTokenExpired(token);
  }

  private getHeaders() {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    };
    return httpOptions;
  }

  private handleError(error: any) {
    const applicationError = error.headers.get("Application-Error");
    if (applicationError) {
      return Observable.throw(applicationError);
    }
    let modelStateError = "";
    if (error.error) {
      for (const key in error.error) {
        if (error.error[key]) {
          modelStateError += error.error[key] + ". \n";
        }
      }
    }
    return Observable.throw(modelStateError || "Błąd serwera");
  }
}
