import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-register-success',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container">
      <h2>Registration Successful</h2>
      <p>You have registered successfully.</p>
      <button (click)="goHome()">Return to Home Page</button>
    </div>
  `,
  styles: [`
    .container {
      max-width: 400px;
      margin: 50px auto;
      padding: 30px;
      border-radius: 10px;
      background-color: #f8f9fa;
      box-shadow: 0 0 10px rgba(0,0,0,0.1);
      font-family: Arial, sans-serif;
      text-align: center;
    }

    h2 {
      margin-bottom: 15px;
      color: #343a40;
    }

    p {
      margin-bottom: 20px;
      color: #555;
    }

    button {
      padding: 12px 24px;
      font-size: 16px;
      background-color: #007bff;
      border: none;
      border-radius: 5px;
      color: white;
      cursor: pointer;
    }

    button:hover {
      background-color: #0056b3;
    }
  `]
})
export class RegisterSuccessComponent {
  constructor(private router: Router) {}

  goHome() {
    this.router.navigate(['/home']);
  }
}
