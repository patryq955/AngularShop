import { AlertifyService } from "./_services/alertify.service";
import { AuthService } from "./_services/auth.service";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { FormsModule } from "@angular/forms";
import { RegisterComponent } from "./register/register.component";
import { HomeComponent } from "./home/home.component";

@NgModule({
  declarations: [AppComponent, NavComponent, RegisterComponent, HomeComponent],
  imports: [BrowserModule, FormsModule, HttpClientModule],
  providers: [AuthService, AlertifyService],
  bootstrap: [AppComponent]
})
export class AppModule {}
