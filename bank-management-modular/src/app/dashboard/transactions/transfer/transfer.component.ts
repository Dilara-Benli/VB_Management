import { Component } from '@angular/core';
import { TransactionsService } from '../../../shared/services/transactions.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-transfer',
  standalone: false,
  //imports: [],
  templateUrl: './transfer.component.html',
  styleUrl: './transfer.component.css'
})
export class TransferComponent {

  accountData: any = {};
  fromAccountNewBalance: number | null = null;
  toAccountNewBalance: number | null = null;

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

  transfer() {
    const transferRequest = {
      sourceAccountID: this.accountData.sourceAccountID,
      targetAccountID: this.accountData.targetAccountID,
      amount: this.accountData.amount,
      explanation: this.accountData.explanation
    };

    if (!this.accountData.sourceAccountID || !this.accountData.targetAccountID || !this.accountData.amount || !this.accountData.explanation) {
      this.toastr.error('Lütfen tüm alanları doldurun.', 'Hata');
      return;
    }

    this.service.transfer(transferRequest).subscribe({
      next: (res: any) => {
        this.fromAccountNewBalance = res.fromAccountNewBalance;
        this.toAccountNewBalance = res.toAccountNewBalance;
        this.toastr.success(res.message, 'Başarılı');
        this.accountData.sourceAccountID = null;
        this.accountData.targetAccountID = null;
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
