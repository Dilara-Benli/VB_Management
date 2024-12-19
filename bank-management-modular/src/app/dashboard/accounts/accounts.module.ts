import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AccountsRoutingModule } from './accounts-routing.module';
import { CreateAccountComponent } from './create-account/create-account.component';
import { MyAccountsComponent } from './my-accounts/my-accounts.component';

@NgModule({
  declarations: [
    CreateAccountComponent,
    MyAccountsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    AccountsRoutingModule
  ]
})
export class AccountsModule {}
