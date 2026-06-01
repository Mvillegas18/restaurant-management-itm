import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { RouterLink, RouterLinkActive } from '@angular/router';

type NavItem = {
  label: string;
  icon: string;
  route: string;
};

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, MatListModule, MatIconModule, MatDividerModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  readonly navItems: NavItem[] = [
    { label: 'Restaurantes', icon: 'storefront', route: '/restaurants' },
    { label: 'Menú', icon: 'restaurant_menu', route: '/menu-items' },
    { label: 'Mesas', icon: 'table_bar', route: '/tables' },
    { label: 'Reservas', icon: 'event', route: '/reservations' },
    { label: 'Pedidos', icon: 'receipt_long', route: '/orders' },
    { label: 'Clientes', icon: 'people', route: '/customers' },
    { label: 'Autenticación', icon: 'lock', route: '/auth' }
  ];
}
