import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../../../shared/services/profile.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-profile',
  standalone: false,
  //imports: [],
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.css'
})
export class MyProfileComponent implements OnInit{

  editMode: { [key: string]: boolean } = {};
  customerData: any = {};
  newPassword: string = '';
  originalCustomerData: any = {};

  constructor(
    private service: ProfileService,
    private router: Router,
    private toastr: ToastrService){}

  ngOnInit(): void { 
    this.onCustomerInfo();
  }
  
    toggleEditMode(field: string) {
    this.editMode[field] = !this.editMode[field];
  }

  onCustomerInfo() {
    this.service.customerInfo().subscribe({
      next: (res: any) => {
        console.log(res);
        this.customerData = res.customer; 
      },
      error: (error) => {
        if (error.status === 401) 
          this.toastr.error('Yetkiniz bulunmamaktadır.', 'Hata');
        else 
          this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
      }
    });
  }

  updateCustomer(){
    const updatedData: any = {
      name: this.customerData.CustomerName,
      lastName: this.customerData.CustomerLastName,
      birthDate: this.customerData.CustomerBirthDate,
      identityNumber: this.customerData.CustomerIdentityNumber,
      email: this.customerData.CustomerEmail
    };
    if (this.newPassword && this.newPassword.trim() !== '') {
      updatedData.password = this.newPassword;
    }

    this.service.updateCustomer(this.customerData.CustomerID, updatedData)
      .subscribe({
        next: (res: any) => {
          this.toastr.success(res.message, 'Başarılı');
          this.originalCustomerData = { ...this.customerData };
          this.newPassword = '';
        },
        error: (error) => {
          const validationErrors = error.error?.errors;
          if (error.status === 401)
            this.toastr.error('Yetkiniz bulunmamaktadır.', 'Hata');
          else if (validationErrors && typeof validationErrors === 'object') {
            Object.keys(validationErrors).forEach((key) => {
              const errorMessages = validationErrors[key];
              errorMessages.forEach((err: string) => {
                this.toastr.error(err, 'Hata');
              });
            });
          } else {
            console.error(error);
            this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
          }
        },
      });
  }
  
  deleteCustomer() {
    if (confirm('Bu profili silmek istediğinizden emin misiniz?')) {
      this.service.deleteCustomer(this.customerData.CustomerID).subscribe({
        next: () => {
          localStorage.removeItem('token');
          window.location.href = '/auth/login';
          //this.router.navigateByUrl('/auth/login');
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
}
