import { Routes } from '@angular/router';
import { AuthPageComponent } from './features/auth/auth.page';
import { CustomersPageComponent } from './features/customers/customers.page';
import { MenuItemsPageComponent } from './features/menu-items/menu-items.page';
import { OrdersPageComponent } from './features/orders/orders.page';
import { ReservationsPageComponent } from './features/reservations/reservations.page';
import { RestaurantsPageComponent } from './features/restaurants/restaurants.page';
import { TablesPageComponent } from './features/tables/tables.page';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'restaurants' },
  { path: 'auth', component: AuthPageComponent },
  { path: 'restaurants', component: RestaurantsPageComponent },
  { path: 'menu-items', component: MenuItemsPageComponent },
  { path: 'tables', component: TablesPageComponent },
  { path: 'reservations', component: ReservationsPageComponent },
  { path: 'orders', component: OrdersPageComponent },
  { path: 'customers', component: CustomersPageComponent },
  { path: '**', redirectTo: 'restaurants' }
];
