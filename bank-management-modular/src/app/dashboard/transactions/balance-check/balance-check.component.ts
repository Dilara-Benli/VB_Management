import { Component } from '@angular/core';
import { TransactionsService } from '../../../shared/services/transactions.service';
import { AccountsService } from '../../../shared/services/accounts.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-balance-check',
  standalone: false,
  //imports: [],
  templateUrl: './balance-check.component.html',
  styleUrl: './balance-check.component.css'
})
export class BalanceCheckComponent {
  
  accountData: any = {};
  customerAccounts: any[] = []; 
  
  constructor(
    private transactionService: TransactionsService,
    private accountService: AccountsService,
    private toastr: ToastrService){}
  
  ngOnInit() {
    this.loadCustomerAccounts();
  }

  loadCustomerAccounts() {
    this.accountService.getAccountsByCustomer().subscribe({
      next: (res: any) => {
        if (res && res.accounts) {
          this.customerAccounts = res.accounts;
        } else {
          this.customerAccounts = [];
        }
      },
      error: (error) => {
        if (error.status === 401)
          this.toastr.error('Yetkiniz bulunmamaktadır.', 'Hata');
        else
          this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
        this.customerAccounts = [];
      }
    });
  }
  
  checkBalance() {
    if (!this.accountData.accountID) {
      this.toastr.error('Hesap numarası girilmedi', 'Hata');
      return;
    }
    this.transactionService.checkBalance(this.accountData.accountID).subscribe({
      next: (res: any) => {
        console.log(res);
        this.accountData.accountBalance = res.accountBalance;
        this.accountData.accountID = null;
      },
      error: (error) => {
        if (error.status === 401)
          this.toastr.error('Yetkiniz bulunmamaktadır.', 'Hata');
        else
          this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
        this.accountData.accountBalance;
      }
    });
  }
  
}
