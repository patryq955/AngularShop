import { AlertifyService } from './../_services/alertify.service';
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
  constructor(private authService: AuthService,private alertifyService: AlertifyService) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.model).subscribe(
      data => {
        this.alertifyService.succes("Logowanie pomyślne");
      },
      error => {
        this.alertifyService.error(error);
      }
    );
  }

  logout() {
    this.authService.userToken = null;
    localStorage.removeItem("token");
    this.alertifyService.message("Wylogowano");
    this.model.password = "";
    this.model.userName = "";
  }

  loggedIn(): boolean {
    return this.authService.loggedIn();
  }
}
