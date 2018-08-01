import { AlertifyService } from "../_services/alertify.service";
import { Observable } from "rxjs/Observable";
import { AuthService } from "../_services/auth.service";
import { FormGroup, FormControl } from "@angular/forms/src/model";
import { Component, OnInit, TemplateRef } from "@angular/core";
import { NgForm } from "@angular/forms";
import {
  BsModalRef,
  BsModalService
} from "ngx-bootstrap/modal";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"]
})
export class NavComponent implements OnInit {
  model: any = {};
  modalRef: BsModalRef;
  registerMode = false;

  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService,
    private modalService: BsModalService
  ) {}

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
    localStorage.removeItem("token");
    this.alertifyService.message("Wylogowano");
    this.model.password = "";
    this.model.userName = "";
  }

  loggedIn(): boolean {
    return this.authService.loggedIn();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(
      template,
      Object.assign({}, { class: "modalRegister" })
    );
  }

  cancelRegister(registerMode: boolean) {
    if (registerMode) {
      this.modalRef.hide();
      this.modalRef = null;
    }
  }
}
