import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { Observable } from "../../../node_modules/rxjs/Observable";
import { User } from "../_models/user";
import {
  HttpClient,
  HttpHeaders
} from "@angular/common/http";



@Injectable()
export class UserService {
  baseUrl = environment.apiUrl + "user/";

  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + "getUsers");
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + "user/" + id);
  }
}