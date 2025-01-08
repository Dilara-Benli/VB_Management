import { Component } from '@angular/core';
import { TransactionsService } from '../../../shared/services/transactions.service';
import { AccountsService } from '../../../shared/services/accounts.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-withdraw',
  standalone: false,
  //imports: [],
  templateUrl: './withdraw.component.html',
  styleUrl: './withdraw.component.css'
})
export class WithdrawComponent {
  
  accountData: any = {};
  customerAccounts: any[] = []; 
  newBalance: number | null = null;

  constructor(
    private transactionService: TransactionsService,
    private accountService: AccountsService,
    private toastr: ToastrService){}

  onAmountInput(event: any): void {
    const value = event.target.value;
    // Noktadan sonra sadece 2 basamağa izin ver
    if (value.includes('.') && value.split('.')[1].length > 2) {
      event.target.value = parseFloat(value).toFixed(2);
      this.accountData.amount = event.target.value;
    }
  }

  blockInvalidCharacters(event: KeyboardEvent): void {
    const forbiddenKeys = ['-', '+', 'e', 'E'];
    if (forbiddenKeys.includes(event.key)) {
      event.preventDefault();
    }
  }

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

  withdraw() {
    const transactionRequest = {
      accountID: this.accountData.accountID,
      amount: this.accountData.amount,
      explanation: this.accountData.explanation,
    };

    if (!this.accountData.accountID || !this.accountData.amount || !this.accountData.explanation) {
      this.toastr.error('Lütfen tüm alanları doldurun.', 'Hata');
      return;
    }

    this.transactionService.withdraw(transactionRequest).subscribe({
      next: (res: any) => {
        this.newBalance = res.newBalance;
        this.toastr.success(res.message, 'Başarılı');
        this.accountData.accountID = null;
        this.accountData.amount = null;
        this.accountData.explanation = '';
      },
      error: (error) => {
        if (error.status === 401)
          this.toastr.error('Yetkiniz bulunmamaktadır.', 'Hata');
        else {
          console.error(error);
          this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
        }
      },
    });
  }
}
