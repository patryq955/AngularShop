import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { HousesListComponent } from "./houses/houses-list/houses-list.component";
import { ContactComponent } from "./contact/contact.component";
import { MyOffersComponent } from "./houses/my-offers/my-offers.component";
import { AuthGuard } from "./_guards/auth.guard";
import { NewOfferComponent } from "./houses/new-offer/new-offer.component";
import { HouseDetailComponent } from "./houses/house-detail/house-detail.component";
import { HouseDetailResolver } from "./_resolves/house-detail.resolver";
import { HouseListResolver } from "./_resolves/houses-list.resolver";
import { MemberEditComponent } from "./members/member-edit/member-edit.component";
import { MemberEditResolver } from "./_resolves/member-edit.resolver";
import { PreventUnsavedChanges } from "./_guards/prevent-unsaved-changes.guard";

export const appRoutes: Routes = [
  { path: "", component: HomeComponent },
  {
    path: "mieszkania",
    component: HousesListComponent,
    resolve: { houses: HouseListResolver }
  },
  {
    path: "mieszkanie/:id",
    component: HouseDetailComponent,
    resolve: { house: HouseDetailResolver }
  },
  // { path: "kontakt", component: ContactComponent },
  {
    path: "", 
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      { path: "moje-oferty", component: MyOffersComponent },
      { path: "dodaj", component: NewOfferComponent },
      {
        path: "edycja/profil",
        component: MemberEditComponent,
        resolve: { user: MemberEditResolver },
        canDeactivate: [PreventUnsavedChanges]
      },
      {path:"kontakt",component: ContactComponent, data: {roles: ['Admin']}}
    ]
  },
  { path: "**", redirectTo: "", pathMatch: "full" }
];
