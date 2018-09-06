import { Component, OnInit } from "@angular/core";
import { AlertifyService } from "../../_services/alertify.service";
import { House } from "../../_models/house";
import { HouseService } from "../../_services/house.service";
import { ActivatedRoute } from "@angular/router";
import { Pagination, PaginatedResult } from "../../_models/pagination";

@Component({
  selector: "app-houses-list",
  templateUrl: "./houses-list.component.html",
  styleUrls: ["./houses-list.component.scss"]
})
export class HousesListComponent implements OnInit {
  houses: House[];
  houseParams: any = {};
  pagination: Pagination;


  constructor(
    private houseService: HouseService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.houses = data["houses"].result;
      this.pagination = data["houses"].pagination;
    });

    this.houseParams.numberRooms = 0;
    this.houseParams.value = 0;
    this.houseParams.orderBy = "default";
  }

  resetFilters() {
    this.houseParams.numberRooms = 0;
    this.houseParams.value = 0;
    this.loadHouses();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadHouses();
  }

  loadHouses() {
    this.houseService
      .getHouses(this.pagination.currentPage, this.pagination.itemsPerPage,this.houseParams)
      .subscribe((res: PaginatedResult<House[]>) => {
        this.houses = res.result;
        this.pagination = res.pagination;
      }),
      error => {
        this.alertify.error(error);
      };
  }
}
