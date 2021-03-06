import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AuthGuard } from "./_guards/auth.guard";
import { appRoutes } from "./routes";
import { NgxImageGalleryModule } from "ngx-image-gallery";
import { FileUploadModule } from "ng2-file-upload";


//Component
import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { RegisterComponent } from "./members/register/register.component";
import { HomeComponent } from "./home/home.component";
import { HousesListComponent } from "./houses/houses-list/houses-list.component";
import { ContactComponent } from "./contact/contact.component";
import { MyOffersComponent } from "./houses/my-offers/my-offers.component";
import { HouseDetailComponent } from "./houses/house-detail/house-detail.component";
import { NewOfferComponent } from "./houses/new-offer/new-offer.component";
import { MemberEditComponent } from "./members/member-edit/member-edit.component";
import { PhotoEditorComponent } from "./members/photo-editor/photo-editor.component";

//Services
import { AlertifyService } from "./_services/alertify.service";
import { AuthService } from "./_services/auth.service";
import { UserService } from "./_services/user.service";
import { HouseService } from "./_services/house.service";

//Bootstrap
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { ModalModule } from "ngx-bootstrap/modal";
import { HouseComponent } from "./houses/house/house.component";
import { JwtModule } from "@auth0/angular-jwt";
import { HouseDetailResolver } from "./_resolves/house-detail.resolver";
import { HouseListResolver } from "./_resolves/houses-list.resolver";
import { MemberEditResolver } from "./_resolves/member-edit.resolver";
import { TabsModule } from "ngx-bootstrap/tabs";
import { PreventUnsavedChanges } from "./_guards/prevent-unsaved-changes.guard";
import { MessageTimePipe } from "./_pipes/messageTimePipe";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    RegisterComponent,
    HomeComponent,
    HousesListComponent,
    ContactComponent,
    MyOffersComponent,
    NewOfferComponent,
    HouseComponent,
    HouseDetailComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    MessageTimePipe
  ],
  imports: [
    BrowserModule,
    NgxImageGalleryModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    ButtonsModule.forRoot(),
    ReactiveFormsModule,
    TabsModule.forRoot(),
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5000"],
        blacklistedRoutes: ["localhost:5000/api/auth"]
      }
    }),
    FileUploadModule,
    BrowserAnimationsModule
  ],
  providers: [
    AuthService,
    AlertifyService,
    AuthGuard,
    UserService,
    HouseService,
    HouseDetailResolver,
    HouseListResolver,
    MemberEditResolver,
    PreventUnsavedChanges
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
