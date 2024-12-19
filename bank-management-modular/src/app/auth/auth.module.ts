import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { FirstKeyPipe } from '../shared/pipes/first-key.pipe'; // Paylaşılan pipe
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule, 
    ReactiveFormsModule, 
    RouterLink,
    FirstKeyPipe,
    AuthRoutingModule 
  ]
})
export class AuthModule {}
