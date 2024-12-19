import { Component, OnInit } from '@angular/core';
import { TransactionsService } from '../../../shared/services/transactions.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-transaction-history',
  standalone: false,
  //imports: [],
  templateUrl: './transaction-history.component.html',
  styleUrl: './transaction-history.component.css'
})
export class TransactionHistoryComponent implements OnInit{

  transactions: any[] = [];
  
  constructor(
    private service: TransactionsService,
    private toastr: ToastrService){}

    ngOnInit(): void {
      this.transactionHistory();
    }
  
    transactionHistory(){ 
    this.service.transactionHistory().subscribe({
      next: (res: any) => {
        if (res && res.transactions){
          this.transactions = res.transactions;
        } else {
          this.transactions = []; 
        }
      },
      error: (error) => {
        if (error.status === 401) 
          this.toastr.error('Yetkiniz bulunmamaktadır.', 'Hata');
        else 
          this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
          this.transactions = [];
      }
    });
  }
}
