import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { PageHeaderComponent } from '../../shared/components/page-header/page-header.component';

@Component({
  selector: 'app-tables-page',
  standalone: true,
  imports: [CommonModule, PageHeaderComponent, MatButtonModule, MatCardModule, MatChipsModule],
  templateUrl: './tables.page.html',
  styleUrl: './tables.page.scss'
})
export class TablesPageComponent {}
