import { Component } from '@angular/core';
import { AccountsService } from '../../../shared/services/accounts.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-account',
  standalone: false,
  //imports: [],
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.css'
})
export class CreateAccountComponent {

  accountData: any = {};

  constructor(
    private service: AccountsService,
    private toastr: ToastrService){}

  createAccount() {
    const newAccount = {
      accountName: this.accountData.accountName,
      currencyType: this.accountData.currencyType
    };

    if (!this.accountData.accountName || !this.accountData.currencyType) {
      this.toastr.error('Lütfen tüm alanları doldurun.', 'Hata');
      return;
    }

    this.service.createAccount(newAccount)
      .subscribe({
        next: (res: any) => {
          this.toastr.success(res.message, 'Başarılı');
          this.accountData = { accountName: '', currencyType: '' };
        },
        error: (error) => {
          if (error.status === 401) 
            this.toastr.error('Yetkiniz bulunmamaktadır.', 'Hata');
          else 
            this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
        }
      });
  }
}
