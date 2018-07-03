import { Observable } from "rxjs/Observable";
import { AuthService } from "../_services/auth.service";
import { FormGroup, FormControl } from "@angular/forms/src/model";
import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"]
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.model).subscribe(
      data => {
        console.log("succes");
      },
      error => {
        console.log(error);
      }
    );
  }

  logout() {
    this.authService.userToken = null;
    localStorage.removeItem("token");
    console.log("Logout");
    this.model.password = "";
    this.model.userName = "";
  }

  loggedIn(): boolean {
    const token = localStorage.getItem("token");
    return !!token;
  }
}
