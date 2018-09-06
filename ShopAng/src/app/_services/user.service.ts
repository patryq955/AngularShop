import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { Observable } from "../../../node_modules/rxjs";
import { User } from "../_models/user";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class UserService {
  baseUrl = environment.apiUrl + "user/";

  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + "getUsers");
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + id);
  }

  updateUser(id: number, user: User) {
    return this.http.put(this.baseUrl + id, user);
  }

  setMainPhoto(userId: number, id: number) {
    return this.http.post(
      this.baseUrl + userId + "/photos/" + id + "/setMain",
      {}
    );
  }

  deletePhtoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + userId + "/photos/" + id, {});
  }
}
