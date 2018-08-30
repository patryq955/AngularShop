import { AuthService } from "../../_services/auth.service";
import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { AlertifyService } from "../../_services/alertify.service";
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from "@angular/forms";
import { User } from "../../_models/user";
import { Route, Router } from "@angular/router";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  user: User;
  registerForm: FormGroup;
  maxDate: Date;

  constructor(
    private authService: AuthService,
    private alertifyService: AlertifyService,
    private fb: FormBuilder,
    private route: Router
  ) {
    this.maxDate = new Date();
    this.maxDate.setDate(this.maxDate.getDate() - 365);
  }

  @Output()
  cancelRegisterEvent = new EventEmitter<boolean>();

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group(
      {
        gender: ["Mężczyzna"],
        username: ["", Validators.required],
        knownAs: ["", Validators.required],
        city: ["", Validators.required],
        dateOfBirth: ["", Validators.required],
        password: ["", [Validators.required, Validators.minLength(4)]],
        confirmPassword: ["", Validators.required]
      },
      {
        validator: this.checkPasswordConfirmValidation
      }
    );
  }

  checkPasswordConfirmValidation(g: FormGroup) {
    return g.get("password").value === g.get("confirmPassword").value
      ? null
      : { mismatch: true };
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(
        () => {
          this.alertifyService.succes("Rejestracja zakonczona pomyślnie");
        },
        error => {
          this.alertifyService.error(error.error);
        },
        () => {
          this.authService.login(this.user).subscribe(() => {
            this.cancelRegisterEvent.emit(true);
            this.route.navigate(["/mieszkania"]);
          });
        }
      );
    }
  }

  cancel() {
    this.cancelRegisterEvent.emit(true);
  }
}
