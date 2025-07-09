import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-email-verify',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="verify-container">
      <h2>Email Verification</h2>
      <p *ngIf="!isVerified">Verifying your email...</p>
      <p *ngIf="isVerified" class="success">Email verified successfully!</p>
      <p *ngIf="error" class="error">Verification failed: {{ error }}</p>
      <button *ngIf="isVerified" (click)="goHome()">Go to Home</button>
    </div>
  `,
  styles: [`
    .verify-container {
      max-width: 500px;
      margin: 80px auto;
      padding: 30px;
      border-radius: 10px;
      background-color: #f8f9fa;
      box-shadow: 0 0 10px rgba(0,0,0,0.1);
      text-align: center;
      font-family: Arial, sans-serif;
    }

    h2 {
      margin-bottom: 20px;
    }

    .success {
      color: green;
      margin: 20px 0;
    }

    .error {
      color: red;
      margin: 20px 0;
    }

    button {
      padding: 10px 20px;
      font-size: 16px;
      background-color: #007bff;
      color: white;
      border: none;
      border-radius: 6px;
      cursor: pointer;
    }

    button:hover {
      background-color: #0056b3;
    }
  `]
})
export class EmailVerifyComponent implements OnInit {
  isVerified = false;
  error: string | null = null;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    const token = this.route.snapshot.queryParamMap.get('token');

    if (token) {
      // Simulate email verification logic
      setTimeout(() => {
        if (token === 'validtoken123') { // Replace with real backend check
          this.isVerified = true;
        } else {
          this.error = 'Invalid or expired verification link.';
        }
      }, 1500);
    } else {
      this.error = 'No token found.';
    }
  }

  goHome() {
    this.router.navigate(['/home']);
  }
}