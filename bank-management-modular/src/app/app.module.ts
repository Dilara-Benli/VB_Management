import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent // AppComponent artık modüle dahil ediliyor
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule, 
    BrowserAnimationsModule, 
    ToastrModule.forRoot({
      positionClass: 'toast-top-center', 
    }),
  ],
  bootstrap: [AppComponent], // AppComponent başlangıç bileşeni olarak ayarlanır
})
export class AppModule {}


