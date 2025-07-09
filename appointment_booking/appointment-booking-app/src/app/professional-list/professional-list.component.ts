import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-professional-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './professional-list.component.html',
  styleUrls: ['./professional-list.component.css']
})
export class ProfessionalListComponent implements OnInit {
  professionals: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<any[]>('https://localhost:7005/api/admin/all-slot-details')
      .subscribe({
        next: (data) => {
          this.professionals = data;
        },
        error: (err) => {
          console.error('Failed to fetch professionals', err);
        }
      });
  }
}
