import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' //uygulamanın her yerinde kullanılabilir halde
})
export class AccountsService {
  constructor(private http:HttpClient) { }

  baseURL = 'https://localhost:7135'

  getHeaders() {
    return { 'Authorization': `Bearer ${localStorage.getItem('token')}` };
  }
  
  createAccount(accountData: any) {
    const headers = this.getHeaders(); 
    return this.http.post(this.baseURL + '/api/Account/CreateAccount', accountData, { headers });
  }

  getAccountsByCustomer() {
    const headers = this.getHeaders();
    return this.http.get(this.baseURL + '/api/Account/GetAccountsByCustomer', { headers });
  }

  updateAccount(accountID: number, updatedData: any) {
    const headers = this.getHeaders();
    return this.http.put(`https://localhost:7135/api/Account/UpdateAccount/${accountID}`, updatedData, { headers });
  }

  deleteAccount(accountID: number) {
    const headers = this.getHeaders();
    return this.http.delete(`https://localhost:7135/api/Account/DeleteAccount/${accountID}`, { headers });
  }
}
