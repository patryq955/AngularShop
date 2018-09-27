import { AlertifyService } from "../_services/alertify.service";
import { AuthService } from "../_services/auth.service";
import { Component, OnInit, TemplateRef } from "@angular/core";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { Router } from "@angular/router";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.scss"]
})
export class NavComponent implements OnInit {
  model: any = {};
  modalRef: BsModalRef;
  registerMode = false;
  photoUrl: string;

  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService,
    private modalService: BsModalService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.currentPhoto.subscribe(
      photoUrl => (this.photoUrl = photoUrl)
    );
  }

  login() {
    this.authService.login(this.model).subscribe(
      data => {
        this.alertifyService.succes("Logowanie pomyślne");
      },
      error => {
        this.alertifyService.error(error.error);
        console.log(error);
      }
    );
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertifyService.message("Wylogowano");
    this.model.password = "";
    this.model.userName = "";
    this.router.navigate(["/home"]);
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
