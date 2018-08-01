import { Injectable } from "@angular/core";
import {
  Resolve,
  Router,
  ActivatedRouteSnapshot
} from "@angular/router";
import { House } from "../_models/house";
import { HouseService } from "../_services/house.service";
import { AlertifyService } from "../_services/alertify.service";
import { Observable } from "../../../node_modules/rxjs/Observable";
import { catchError } from "rxjs/operators";
import { of } from "rxjs/observable/of";
import { HouseDetail } from "../_models/houseDetail";

@Injectable()
export class HouseDetailResolver implements Resolve<HouseDetail> {
  constructor(
    private houseService: HouseService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<HouseDetail> {
    return this.houseService.getHouse(route.params["id"]).pipe(
        catchError(error =>{
            this.alertify.error("Problem z danymi");
            this.router.navigate(['/mieszkania']);
            return of(null);
        })
    );
  }
}
