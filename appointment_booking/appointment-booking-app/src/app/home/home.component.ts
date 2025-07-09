import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule],
  template: `
    <div class="background">
      <div class="card">
        <h1>Welcome to <span>Tax Appointment Portal</span></h1>
        <p class="subtitle">Easily book appointments with our expert tax professionals.</p>
        <div class="button-group">
          <button routerLink="/login">Login</button>
          <button routerLink="/signup">Signup</button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .background {
      height: 100vh;
      background: linear-gradient(to right, #e3f2fd, #ffffff);
      display: flex;
      align-items: center;
      justify-content: center;
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

    .card {
      background: white;
      padding: 40px 60px;
      border-radius: 16px;
      box-shadow: 0 8px 24px rgba(0,0,0,0.1);
      text-align: center;
      max-width: 500px;
      width: 90%;
    }

    h1 {
      font-size: 2rem;
      color: #333;
      margin-bottom: 10px;
    }

    h1 span {
      color: #007bff;
    }

    .subtitle {
      color: #555;
      font-size: 1rem;
      margin-bottom: 30px;
    }

    .button-group {
      display: flex;
      justify-content: center;
      gap: 20px;
    }

    button {
      padding: 12px 30px;
      font-size: 16px;
      border: none;
      background-color: #007bff;
      color: white;
      border-radius: 8px;
      cursor: pointer;
      transition: background-color 0.25s ease-in-out;
    }

    button:hover {
      background-color: #0056b3;
    }
  `]
})
export class HomeComponent {}
