import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { RegisterSuccessComponent } from './register-success/register-success.component';
import { EmailVerifyComponent } from './email-verify/email-verify.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { AppointmentSuccessComponent } from './appointment-success/appointment-success.component';
import { ProfessionalListComponent } from './professional-list/professional-list.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';



export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'register-success', component: RegisterSuccessComponent },
  { path: 'email-verify', component: EmailVerifyComponent },
  { path: 'admin-dashboard', component: AdminDashboardComponent },
  { path: 'user-dashboard', component: UserDashboardComponent },
  { path: 'appointment-success', component: AppointmentSuccessComponent  },
  { path: 'professional-list', component: ProfessionalListComponent  },
  { path: 'my-bookings', component: MyBookingsComponent  }
];
