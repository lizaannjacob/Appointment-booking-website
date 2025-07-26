import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-bookings',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {
  bookedAppointments: any[] = [];
  userEmail: string = '';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const storedEmail = localStorage.getItem('userEmail');
    if (storedEmail) {
      this.userEmail = storedEmail;
      this.fetchBookings();
    } else {
      alert('No user email found. Please login.');
      this.router.navigate(['/login']);
    }
  }

  fetchBookings() {
    this.http.get<any[]>(`http://localhost:7005/api/user/my-appointments?email=${this.userEmail}`)
      .subscribe({
        next: data => {
          this.bookedAppointments = data;
        },
        error: err => {
          console.error('Error fetching bookings:', err);
          alert('Failed to load bookings');
        }
      });
  }

  goBack() {
    this.router.navigate(['/user-dashboard']);
  }

  logout() {
    localStorage.removeItem('userEmail');
    this.router.navigate(['/home']);
  }
}
