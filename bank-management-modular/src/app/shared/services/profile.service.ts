import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' //uygulamanın her yerinde kullanılabilir halde
})
export class ProfileService {
  constructor(private http:HttpClient) { }

  baseURL = 'https://localhost:7135'

  getHeaders() {
    return { 'Authorization': `Bearer ${localStorage.getItem('token')}` };
  }
  
  customerInfo() {
    const headers = this.getHeaders();
    return this.http.get(this.baseURL + '/api/Customer/DisplayCustomerInfo', { headers });
  }

  updateCustomer(customerID: number, updatedData: any) {
    const headers = this.getHeaders();
    return this.http.put(`https://localhost:7135/api/Customer/UpdateCustomer/${customerID}`, updatedData, { headers });
  }

  deleteCustomer(customerID: number) {
    const headers = this.getHeaders();
    return this.http.delete(`https://localhost:7135/api/Customer/DeleteCustomer/${customerID}`, { headers });
  }
}
