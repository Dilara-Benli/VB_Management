import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-logout',
  standalone: false,
  //imports: [],
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})
export class LogoutComponent implements OnInit{

  ngOnInit(): void { 
    this.onLogout();
  }
  
  onLogout() {
    localStorage.removeItem('token');
    window.location.href = '/auth/login';
  }
}
