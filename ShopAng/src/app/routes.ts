import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HousesListComponent } from './houses-list/houses-list.component';
import { ContactComponent } from './contact/contact.component';
import { MyOfferComponent } from './my-offer/my-offer.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [

    {path: '', component: HomeComponent},
    {path: 'mieszkania', component: HousesListComponent},
    {path: 'kontakt', component: ContactComponent},
    {
        path:'',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'moje-oferty', component: MyOfferComponent},
        ]
    },
    {path: '**',redirectTo: '', pathMatch: 'full'}

]