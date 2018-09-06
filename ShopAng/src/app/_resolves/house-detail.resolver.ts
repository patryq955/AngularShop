import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from "@angular/router";
import { House } from "../_models/house";
import { HouseService } from "../_services/house.service";
import { AlertifyService } from "../_services/alertify.service";
// import { Observable, of } from "../../../node_modules/rxjs";
import { catchError } from "rxjs/operators";
import { HouseDetail } from "../_models/houseDetail";
import { Observable, of } from "rxjs";

@Injectable()
export class HouseDetailResolver implements Resolve<HouseDetail> {
  constructor(
    private houseService: HouseService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<HouseDetail> {
    return this.houseService.getHouse(route.params["id"]).pipe(
      catchError(error => {
        this.alertify.error("Problem z danymi");
        this.router.navigate(["/mieszkania"]);
        return of(null);
      })
    );
  }
}
