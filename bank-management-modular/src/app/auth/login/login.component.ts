import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(
    public formBuilder: FormBuilder,
    private service: AuthService,
    private router: Router,
    private toastr: ToastrService){}
  isSubmitted:boolean = false;

  form = this.formBuilder.group({
    email : ['', Validators.required],
    password : ['', Validators.required]
  })

  onSubmit(){
    this.isSubmitted = true;
    if(this.form.valid){
      this.service.login(this.form.value)
      .subscribe({
        next: (res: any) => {
          localStorage.setItem('token', res.token);
          this.router.navigateByUrl('/dashboard');
        },
        error: (error) => {
          this.toastr.error(error.error?.message || 'Bir hata oluştu.', 'Hata');
        }
      })
    }
  }  

  hasDisplayableError(controlName: string):Boolean {
    const control = this.form.get(controlName);
    return Boolean(control?.invalid) && (this.isSubmitted || Boolean(control?.touched) || Boolean(control?.dirty))
  }
}
