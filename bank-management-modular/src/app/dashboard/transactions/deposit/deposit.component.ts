import { Component } from '@angular/core';
import { TransactionsService } from '../../../shared/services/transactions.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-deposit',
  standalone: false,
  //imports: [],
  templateUrl: './deposit.component.html',
  styleUrl: './deposit.component.css'
})
export class DepositComponent {

  accountData: any = {};
  newBalance: number | null = null;
  
  constructor(
    private service: TransactionsService,
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

  deposit(){
    const transactionRequest = {
      accountID: this.accountData.accountID,
      amount: this.accountData.amount,
      explanation: this.accountData.explanation,
    };

    if (!this.accountData.accountID || !this.accountData.amount || !this.accountData.explanation) {
      this.toastr.error('Lütfen tüm alanları doldurun.', 'Hata');
      return;
    }

    this.service.deposit(transactionRequest).subscribe({
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
