import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "../../../node_modules/rxjs";
import { House } from "../_models/house";
import { HouseDetail } from "../_models/houseDetail";
import {  PaginatedResult } from "../_models/pagination";
import { map } from "../../../node_modules/rxjs/operators";

@Injectable()
export class HouseService {
  baseUrl = environment.apiUrl + "house/";

  constructor(private http: HttpClient) {}

  getHouses(page?, itemsPerPage?, houseParams?): Observable<PaginatedResult<House[]>> {
    const paginatedResult: PaginatedResult<House[]> = new PaginatedResult<House[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append("pageNumber", page);
      params = params.append("pageSize", itemsPerPage);
    }
    if(houseParams != null){
      params = params.append("numberRooms", houseParams.numberRooms)
      params = params.append("value", houseParams.value)
      params = params.append("orderBy", houseParams.orderBy)

    }

    return this.http
      .get<House[]>(this.baseUrl + "getHouses", {
        observe: "response",
        params
      }).pipe(
      map(response => {
        paginatedResult.result = response.body;

        if (response.headers.get("Pagination") != null) {
          paginatedResult.pagination = JSON.parse(
            response.headers.get("Pagination")
          );
        }
        return paginatedResult;
      }));
  }

  getHouse(id): Observable<HouseDetail> {
    return this.http.get<HouseDetail>(this.baseUrl + id);
  }
}
