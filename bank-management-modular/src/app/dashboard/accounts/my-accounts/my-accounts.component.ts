import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../../../shared/services/accounts.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-accounts',
  standalone: false,
  //imports: [],
  templateUrl: './my-accounts.component.html',
  styleUrl: './my-accounts.component.css'
})
export class MyAccountsComponent implements OnInit{

  editMode: { [key: string]: boolean } = {};
  accounts: any[] = [];

  constructor(
    private service: AccountsService,
    private toastr: ToastrService){}

  ngOnInit(): void {
    this.onViewAccounts();
  }
  
    toggleEditMode(field: string) {
    this.editMode[field] = !this.editMode[field];
  }

  onViewAccounts(){
    this.service.getAccountsByCustomer().subscribe({
      next: (res: any) => {
        if (res && res.accounts) {
          this.accounts = res.accounts; 
        } else {
          this.accounts = []; 
        }
      },
      error: (error) => {
        if (error.status === 401) 
          this.toastr.error('Yetkiniz bulunmamaktadır.', 'Hata');
        else 
          this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
          this.accounts = [];
      }
    });
  }

  updateAccount(account: any) {
    const updatedData: any = {
      accountName: account.accountName,
      currencyType: account.currencyType
    };
    this.service.updateAccount(account.accountID, updatedData)
      .subscribe({
        next: (res: any) => {
          this.toastr.success(res.message, 'Başarılı');
          this.onViewAccounts();
        },
        error: (error) => {
          //console.error(error);
          this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
        },
      });
  }

  deleteAccount(account: any) {
    if (confirm('Bu hesabı silmek istediğinizden emin misiniz?')) {
      this.service.deleteAccount(account.accountID)
        .subscribe({
          next: (res: any) => {
            this.toastr.success(res.message, 'Başarılı');
            this.onViewAccounts();
          },
          error: (error) => {
            //console.error(error);
            this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
          },
        });
    }
  }
}
