import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

interface TaxProfessional {
  id: number;
  name: string;
  email: string;
  phone: string;
  incomeTaxFilingSpecialist: boolean;
  corporateTaxConsultant: boolean;
  investmentTaxPlanningAdvisor: boolean;
}

interface Slot {
  slotId: number;
  professionalId: number;
  slotStart: string;
  slotEnd: string;
  status: 'available' | 'booked' | 'unavailable';
}

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule, CommonModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  taxPros: TaxProfessional[] = [];
  selectedTaxPro: TaxProfessional | null = null;
  //selectedTaxProId: number | null = null;
  name: string = '';
  startTime: string = '';
  endTime: string = '';
  slotDate: string = '';
  slots: { [taxProId: number]: Slot[] } = {};

  newTaxPro: TaxProfessional = {
    id: 0,
    name: '',
    email: '',
    phone: '',
    incomeTaxFilingSpecialist: false,
    corporateTaxConsultant: false,
    investmentTaxPlanningAdvisor: false
  };



  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.loadProfessionals();
  }

  loadProfessionals() {
    this.http.get<TaxProfessional[]>('http://localhost:7005/api/admin/get-professionals')
      .subscribe({
        next: (data) => {
          this.taxPros = data;
          console.log('Loaded professionals:', this.taxPros);
        },
        error: (err) => {
          console.error('Failed to load professionals:', err);
        }
      });
    //console.log(this.selectedTaxProId);
  }

  addTaxPro() {
    const { id, ...professionalData } = this.newTaxPro;

    this.http.post('http://localhost:7005/api/admin/add-professional', professionalData)
      .subscribe({
        next: () => {
          alert('Tax professional added successfully!');
          this.loadProfessionals();

          this.newTaxPro = {
            id: 0,
            name: '',
            email: '',
            phone: '',
            incomeTaxFilingSpecialist: false,
            corporateTaxConsultant: false,
            investmentTaxPlanningAdvisor: false
          };
        },
        error: (err) => {
          console.error('Failed to add professional:', err);
          alert('Failed to add tax professional');
        }
      });
  }

  getSelectedTaxProName(): string {
    return this.selectedTaxPro ? this.selectedTaxPro.name : 'Unknown';
  }

  generateSlots() {
    if (!this.selectedTaxPro || !this.selectedTaxPro.id || !this.startTime || !this.endTime || !this.slotDate) {
      alert('Please select tax professional, slot date and time range.');
      console.log('Professional:', this.selectedTaxPro);
      console.log('Start:', this.startTime, 'End:', this.endTime, 'Date:', this.slotDate);
      return;
    }

    const start = `${this.slotDate}T${this.startTime}`;
    const end = `${this.slotDate}T${this.endTime}`;
    const professionalId = this.selectedTaxPro.id;

    console.log('Selected ID:', professionalId);
    console.log('Name:', this.selectedTaxPro.name);
    console.log('Start:', start);
    console.log('End:', end);

    const url = `http://localhost:7005/api/admin/generate-slots?professionalId=${professionalId}&startTime=${start}&endTime=${end}`;

    this.http.post(url, {}).subscribe({
      next: () => {
        alert('Slots generated and stored in database!');
        this.getSlots();
      },
      error: err => {
        console.error('Error generating slots:', err);
        alert('Failed to generate slots');
      }
    });
  }

  getSlots() {
    if (!this.selectedTaxPro) return;

    this.http.get<Slot[]>(`http://localhost:7005/api/admin/get-slots?professionalId=${this.selectedTaxPro.id}`)
      .subscribe({
        next: (data) => {
          if (this.selectedTaxPro && this.selectedTaxPro.id != null) {
            this.slots[this.selectedTaxPro.id] = data;
          }

        },
        error: err => {
          console.error('Failed to fetch slots:', err);
        }
      });
  }


  updateSlotStatus(slotId: number, status: string) {
    const url = `http://localhost:7005/api/admin/update-slot-status?slotId=${slotId}&status=${status}`;
    this.http.put(url, {}).subscribe({
      next: () => {
        alert('Slot status updated');
        this.getSlots(); // Refresh
      },
      error: err => {
        console.error('Failed to update slot status:', err);
      }
    });
  }

  deleteSlotFromDB(slotId: number) {
    this.http.delete(`http://localhost:7005/api/admin/delete-slot?slotId=${slotId}`).subscribe({
      next: () => {
        alert('Slot deleted');
        this.getSlots(); // Refresh
      },
      error: err => {
        console.error('Failed to delete slot:', err);
      }
    });
  }

  logout() {
    this.router.navigate(['/home']);
  }

  goToProfessionalList() {
    this.router.navigate(['/professional-list']);
  }
}