import { Component } from '@angular/core';
import { TransactionsService } from '../../../shared/services/transactions.service';
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
  
  constructor(
    private service: TransactionsService,
    private toastr: ToastrService){}
  
  blockInvalidCharacters(event: KeyboardEvent): void {
    const forbiddenKeys = ['-', '+', 'e', 'E'];
    if (forbiddenKeys.includes(event.key)) {
      event.preventDefault();
    }
  }    
  
    checkBalance() {
    if (!this.accountData.accountID) {
      this.toastr.error('Hesap numarası girilmedi', 'Hata');
      return;
    }
    this.service.checkBalance(this.accountData.accountID).subscribe({
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
