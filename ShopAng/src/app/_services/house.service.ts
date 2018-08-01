import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import {
  HttpClient,
  HttpHeaders
} from "@angular/common/http";
import { Observable } from "../../../node_modules/rxjs/Observable";
import { House } from "../_models/house";
import { HouseDetail } from "../_models/houseDetail";

@Injectable()
export class HouseService {
  baseUrl = environment.apiUrl + "house/";

  constructor(private http: HttpClient) {}

  getHouses(): Observable<House[]> {
    return this.http.get<House[]>(this.baseUrl + "getHouses");
  }

  getHouse(id): Observable<HouseDetail> {
    return this.http.get<HouseDetail>(this.baseUrl + id);
  }
}
