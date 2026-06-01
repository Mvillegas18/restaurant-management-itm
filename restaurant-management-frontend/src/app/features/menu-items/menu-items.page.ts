import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ApiService } from '../../core/services/api.service';
import { PageHeaderComponent } from '../../shared/components/page-header/page-header.component';

export interface MenuItem {
  id: number;
  name: string;
  description: string;
  price: number;
  category: string | number;
  isAvailable: boolean;
  restaurantId: number;
}

@Component({
  selector: 'app-menu-items-page',
  standalone: true,
  imports: [
    CommonModule,
    PageHeaderComponent,
    MatButtonModule,
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './menu-items.page.html',
  styleUrl: './menu-items.page.scss'
})
export class MenuItemsPageComponent implements OnInit {
  private apiService = inject(ApiService);
  private cdr = inject(ChangeDetectorRef);

  menuItems: MenuItem[] = [];
  displayedColumns: string[] = ['name', 'description', 'category', 'price', 'isAvailable'];
  isLoading = true;
  errorMessage = '';

  ngOnInit(): void {
    this.loadMenuItems();
  }

  loadMenuItems(): void {
    this.isLoading = true;
    this.apiService.get<MenuItem[]>('MenuItems').subscribe({
      next: (data) => {
        this.menuItems = data;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching menu items', err);
        this.errorMessage = 'No se pudo conectar con el backend. Por favor verifica que la API esté corriendo.';
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  getAvailableCount(): number {
    return this.menuItems.filter(item => item.isAvailable).length;
  }

  getUnavailableCount(): number {
    return this.menuItems.filter(item => !item.isAvailable).length;
  }

  getCategoryTranslation(cat: string | number): string {
    const categoriesMap: Record<string, string> = {
      'Starter': 'Entrada',
      'MainCourse': 'Plato Fuerte',
      'Dessert': 'Postre',
      'Drink': 'Bebida',
      '0': 'Entrada',
      '1': 'Plato Fuerte',
      '2': 'Postre',
      '3': 'Bebida'
    };
    return categoriesMap[cat.toString()] || cat.toString();
  }
}
