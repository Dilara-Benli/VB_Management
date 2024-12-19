import { Component } from '@angular/core';
import { FormBuilder, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { AuthService } from '../../shared/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: false, 
  //imports: [ReactiveFormsModule, CommonModule, FirstKeyPipe, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(
    public formBuilder: FormBuilder, 
    private service: AuthService,
    private toastr: ToastrService){}
  isSubmitted:boolean = false;

  passwordMatchValidator : ValidatorFn = (control:AbstractControl):null =>{
    const password = control.get('password')
    const confirmPassword = control.get('confirmPassword')

    if (password && confirmPassword && password.value != confirmPassword.value)
      confirmPassword?.setErrors({ passwordMismatch: true })
    else
      confirmPassword?.setErrors(null)

    return null;
  }

  form = this.formBuilder.group({
    name : ['', Validators.required],
    lastName : ['', Validators.required],
    identityNumber : ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern(/^\d+$/)]],
    birthDate : ['', Validators.required],
    email : ['', [Validators.required, Validators.email]],
    password : ['', [Validators.required, Validators.minLength(6), Validators.pattern(/(?=.*[^a-zA-Z0-9 ])/)]],
    confirmPassword : [''],
  }, { validators: this.passwordMatchValidator })

  onSubmit(){
    this.isSubmitted = true;
    if(this.form.valid){
      const formData: any = { ...this.form.value };
      formData.identityNumber = Number(formData.identityNumber);
      formData.birthDate = new Date(formData.birthDate).toISOString();

      this.service.register(formData)
      .subscribe({
        next: (res: any) => {
          this.form.reset();
          this.isSubmitted = false;
          this.toastr.success(res.message, 'Başarılı')
        },
        error: (error) => {
          const validationErrors = error.error?.errors;
          if (validationErrors && typeof validationErrors === 'object') {
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
  }  

  hasDisplayableError(controlName: string):Boolean {
    const control = this.form.get(controlName);
    return Boolean(control?.invalid) && (this.isSubmitted || Boolean(control?.touched) || Boolean(control?.dirty))
  }

}
