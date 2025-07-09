import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';

interface Slot {
  slotId: number;
  slotStart: string;
  slotEnd: string;
  status: 'available' | 'booked' | 'unavailable';
}

interface TaxProfessional {
  id: number;
  name: string;
}

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  taxPros: TaxProfessional[] = [];
  selectedTaxProId: number | null = null;
  availableSlots: Slot[] = [];
  userEmail: string = localStorage.getItem('userEmail') || '';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.loadProfessionals();
  }

  goToMyBookings(): void {
    this.router.navigate(['/my-bookings']);
  }

  loadProfessionals(): void {
    this.http.get<TaxProfessional[]>('https://localhost:7005/api/admin/get-professionals')
      .subscribe({
        next: data => this.taxPros = data,
        error: err => console.error('Error loading professionals:', err)
      });
  }

  loadSlotsForSelectedProfessional(): void {
    if (!this.selectedTaxProId) return;

    this.http.get<Slot[]>(`https://localhost:7005/api/user/available-slots-by-professional?professionalId=${this.selectedTaxProId}`)
      .subscribe({
        next: data => this.availableSlots = data,
        error: err => console.error('Error loading slots:', err)
      });
  }

  bookSlot(slotId: number, proName: string, time: string): void {
    const url = `https://localhost:7005/api/user/book-slot?slotId=${slotId}&email=${encodeURIComponent(this.userEmail)}`;

    this.http.put<{ message: string }>(url, {})
      .subscribe({
        next: response => {
          if (response?.message === "Slot booked successfully") {
            this.router.navigate(['/appointment-success'], {
              queryParams: {
                time: time,
                pro: proName
              }
            });
          } else {
            alert('Unexpected response from server.');
          }
        },
        error: err => {
          console.error('Booking failed:', err);
          alert('Failed to book slot');
        }
      });
  }

  selectedTaxProName(): string {
    const selectedPro = this.taxPros.find(p => p.id === this.selectedTaxProId);
    return selectedPro ? selectedPro.name : '';
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
