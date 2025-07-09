import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-appointment-success',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './appointment-success.component.html',
  styleUrls: ['./appointment-success.component.css']
})
export class AppointmentSuccessComponent implements OnInit {
  professionalName: string = '';
  time: string = '';

  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.professionalName = params['pro'] || '';
      this.time = params['time'] || '';
    });
  }

  returnToDashboard(): void {
    this.router.navigate(['/user-dashboard']);
  }
}
