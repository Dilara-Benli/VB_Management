import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' //uygulamanın her yerinde kullanılabilir halde
})
export class TransactionsService {
  constructor(private http:HttpClient) { }

  baseURL = 'https://localhost:7135'

  getHeaders() {
    return { 'Authorization': `Bearer ${localStorage.getItem('token')}` };
  }
  
  checkBalance(accountID: number) {
    const headers = this.getHeaders();
    return this.http.get(`https://localhost:7135/api/Transaction/CheckBalance/${accountID}`, { headers });
  }

  deposit(transactionRequest: any) {
    const headers = this.getHeaders(); 
    return this.http.post(this.baseURL + '/api/Transaction/Deposit', transactionRequest, { headers });
  }

  withdraw(transactionRequest: any) {
    const headers = this.getHeaders(); 
    return this.http.post(this.baseURL + '/api/Transaction/Withdraw', transactionRequest, { headers });
  }

  transactionHistory(){
    const headers = this.getHeaders();
    return this.http.get(this.baseURL + '/api/Transaction/TransactionHistory', { headers });
  }

  transfer(transferRequest: any) {
    const headers = this.getHeaders(); 
    return this.http.post(this.baseURL + '/api/Transaction/TransferMoney', transferRequest, { headers });
  }
}
