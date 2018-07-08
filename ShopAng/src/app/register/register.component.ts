import { AuthService } from "./../_services/auth.service";
import { Component, OnInit, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) {}

  @Output() cancelRegisterEvent = new EventEmitter<boolean>();

  ngOnInit() {}

  register() {
    this.authService.register(this.model).subscribe(
      () => {
        console.log("register succesful");
      },
      error => {
        console.log(error);
      }
    );
  }

  cancel() {
    this.cancelRegisterEvent.emit(false);
  }
}
