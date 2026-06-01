import { Routes } from '@angular/router';
import { CustomersPageComponent } from './features/customers/customers.page';
import { MenuItemsPageComponent } from './features/menu-items/menu-items.page';
import { RestaurantsPageComponent } from './features/restaurants/restaurants.page';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'restaurants' },
  { path: 'restaurants', component: RestaurantsPageComponent },
  { path: 'menu-items', component: MenuItemsPageComponent },
  { path: 'customers', component: CustomersPageComponent },
  { path: '**', redirectTo: 'restaurants' }
];
