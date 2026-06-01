import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { ApiService } from '../../core/services/api.service';
import { PageHeaderComponent } from '../../shared/components/page-header/page-header.component';

export interface Restaurant {
  id: number;
  name: string;
  address: string;
  phone: string;
  email: string;
  capacity: number;
}

@Component({
  selector: 'app-restaurants-page',
  standalone: true,
  imports: [
    CommonModule,
    PageHeaderComponent,
    MatButtonModule,
    MatCardModule,
    MatTableModule,
    MatIconModule
  ],
  templateUrl: './restaurants.page.html',
  styleUrl: './restaurants.page.scss'
})
export class RestaurantsPageComponent implements OnInit {
  private apiService = inject(ApiService);
  private cdr = inject(ChangeDetectorRef);
  
  restaurants: Restaurant[] = [];
  displayedColumns: string[] = ['name', 'address', 'phone', 'email', 'capacity'];
  isLoading = true;
  errorMessage = '';

  ngOnInit(): void {
    this.loadRestaurants();
  }

  loadRestaurants(): void {
    this.isLoading = true;
    this.apiService.get<Restaurant[]>('Restaurants').subscribe({
      next: (data) => {
        this.restaurants = data;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching restaurants', err);
        this.errorMessage = 'No se pudo conectar con el backend. Por favor verifica que la API esté corriendo.';
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  getTotalCapacity(): number {
    return this.restaurants.reduce((acc, r) => acc + r.capacity, 0);
  }
}
