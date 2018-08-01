import { Component, OnInit } from "@angular/core";
import { HouseService } from "../../_services/house.service";
import { AlertifyService } from "../../_services/alertify.service";
import { ActivatedRoute } from "@angular/router";
import { HouseDetail } from "../../_models/houseDetail";

@Component({
  selector: "app-house-detail",
  templateUrl: "./house-detail.component.html",
  styleUrls: ["./house-detail.component.scss"]
})
export class HouseDetailComponent implements OnInit {
  house: HouseDetail;
  constructor(
    private houseService: HouseService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.house = data["house"];
    });
  }
}
