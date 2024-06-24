import { Routes } from '@angular/router';
import { BeerListComponent } from './beer-list/beer-list.component';
import { BeerComponent } from './beer/beer.component';

export const routes: Routes = [
    { path: 'beers', component: BeerListComponent },
    { path: 'beer/:id', component: BeerComponent },
    { path: '', redirectTo: '/beers', pathMatch: 'full' },
  ];
