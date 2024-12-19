import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAccountComponent } from './create-account/create-account.component';
import { MyAccountsComponent } from './my-accounts/my-accounts.component';

const routes: Routes = [
  { path: 'create-account', component: CreateAccountComponent },
  { path: 'my-accounts', component: MyAccountsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountsRoutingModule {}
