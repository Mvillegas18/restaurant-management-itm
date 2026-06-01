import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { PageHeaderComponent } from '../../shared/components/page-header/page-header.component';

@Component({
  selector: 'app-menu-items-page',
  standalone: true,
  imports: [CommonModule, PageHeaderComponent, MatButtonModule, MatCardModule, MatChipsModule],
  templateUrl: './menu-items.page.html',
  styleUrl: './menu-items.page.scss'
})
export class MenuItemsPageComponent {}
