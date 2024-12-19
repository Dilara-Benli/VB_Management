import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' //uygulamanın her yerinde kullanılabilir halde
})
export class AuthService {
  constructor(private http:HttpClient) { }

  baseURL = 'https://localhost:7135'

  register(formData:any): Observable<any> {
    return this.http.post(this.baseURL+'/api/Customer/Register',formData);
  }

  login(formData:any): Observable<any> {
    return this.http.post(this.baseURL+'/api/Customer/Login',formData);
  }
}
