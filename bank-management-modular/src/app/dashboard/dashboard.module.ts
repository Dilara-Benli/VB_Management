import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    DashboardComponent, 
    HomeComponent,    
  ],
  imports: [
    CommonModule, 
    RouterModule,
    DashboardRoutingModule 
  ]
})
export class DashboardModule {}
