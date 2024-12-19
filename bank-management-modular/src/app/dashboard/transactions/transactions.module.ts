import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TransactionsRoutingModule } from './transactions-routing.module';
import { BalanceCheckComponent } from './balance-check/balance-check.component';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawComponent } from './withdraw/withdraw.component';
import { TransferComponent } from './transfer/transfer.component';
import { TransactionHistoryComponent } from './transaction-history/transaction-history.component';


@NgModule({
  declarations: [
    BalanceCheckComponent,
    DepositComponent,
    WithdrawComponent,
    TransferComponent,
    TransactionHistoryComponent
  ],
  imports: [
    CommonModule,
    FormsModule, 
    TransactionsRoutingModule
  ]
})
export class TransactionsModule {}
