import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { ApiService } from '../../core/services/api.service';
import { PageHeaderComponent } from '../../shared/components/page-header/page-header.component';

export interface Customer {
  id: number;
  name: string;
  email: string;
  phone: string;
}

@Component({
  selector: 'app-customers-page',
  standalone: true,
  imports: [
    CommonModule,
    PageHeaderComponent,
    MatButtonModule,
    MatCardModule,
    MatTableModule,
    MatIconModule
  ],
  templateUrl: './customers.page.html',
  styleUrl: './customers.page.scss'
})
export class CustomersPageComponent implements OnInit {
  private apiService = inject(ApiService);
  private cdr = inject(ChangeDetectorRef);

  customers: Customer[] = [];
  displayedColumns: string[] = ['id', 'name', 'email', 'phone'];
  isLoading = true;
  errorMessage = '';

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.isLoading = true;
    this.apiService.get<Customer[]>('Customers').subscribe({
      next: (data) => {
        this.customers = data;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching customers', err);
        this.errorMessage = 'No se pudo conectar con el backend. Por favor verifica que la API esté corriendo.';
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }
}
