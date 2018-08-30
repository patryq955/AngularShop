import { Injectable } from "@angular/core";
import {
  Resolve,
  Router,
  ActivatedRouteSnapshot
} from "@angular/router";
import { House } from "../_models/house";
import { HouseService } from "../_services/house.service";
import { AlertifyService } from "../_services/alertify.service";
import { Observable } from "rxjs/Observable";
import { catchError } from "rxjs/operators";
import { of } from "rxjs/observable/of";

@Injectable()
export class HouseListResolver implements Resolve<House[]> {
  pageNumber=1;
  pageSize = 5;
  
  constructor(
    private houseService: HouseService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<House[]> {
    return this.houseService.getHouses(this.pageNumber,this.pageSize).pipe(
        catchError(error =>{
            this.alertify.error("Problem z danymi");
            this.router.navigate(['/kontakt']);
            return of(null);
        })
    );
  }
}
