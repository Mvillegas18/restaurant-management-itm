import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { PageHeaderComponent } from '../../shared/components/page-header/page-header.component';

@Component({
  selector: 'app-reservations-page',
  standalone: true,
  imports: [CommonModule, PageHeaderComponent, MatButtonModule, MatCardModule, MatChipsModule],
  templateUrl: './reservations.page.html',
  styleUrl: './reservations.page.scss'
})
export class ReservationsPageComponent {}
