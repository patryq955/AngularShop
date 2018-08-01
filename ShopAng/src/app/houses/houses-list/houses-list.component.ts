import { Component, OnInit } from "@angular/core";
import { AlertifyService } from "../../_services/alertify.service";
import { House } from "../../_models/house";
import { HouseService } from "../../_services/house.service";
import { ActivatedRoute } from "../../../../node_modules/@angular/router";

@Component({
  selector: "app-houses-list",
  templateUrl: "./houses-list.component.html",
  styleUrls: ["./houses-list.component.scss"]
})
export class HousesListComponent implements OnInit {
  houses: House[];
  constructor(
    private houseService: HouseService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.houses = data["houses"];
    });
  }
}
