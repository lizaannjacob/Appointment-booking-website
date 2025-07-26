import { Component } from '@angular/core'; 
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { HttpClientModule, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form = {
    email: '',
    password: ''
  };

  errorMessage = '';

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit() {
    this.http.post('http://localhost:7005/api/user/login', {
      email: this.form.email,
      password: this.form.password
    }).subscribe({
      next: (res: any) => {
        console.log('Login Response:', res);

        // ✅ Store user email for future use (like booking)
        localStorage.setItem('userEmail', this.form.email);

        if (res.isAdmin) {
          this.router.navigate(['/admin-dashboard']);
        } else if (res.redirectUrl === '/user-dashboard') {
          this.router.navigate(['/user-dashboard']);
        } else {
          this.errorMessage = 'Unknown role. Please contact support.';
        }
      },
      error: () => {
        this.errorMessage = 'Invalid email or password';
      }
    });
  }
}
