import { bootstrapApplication } from '@angular/platform-browser';
import { routes } from './app/app.routes';
import { provideRouter } from '@angular/router';
import { LoginComponent } from './app/login/login.component';
import { SignupComponent } from './app/signup/signup.component';
import { HomeComponent } from './app/home/home.component';
import { RegisterSuccessComponent } from './app/register-success/register-success.component';
import { EmailVerifyComponent } from './app/email-verify/email-verify.component';
import { AdminDashboardComponent } from './app/admin-dashboard/admin-dashboard.component';
import { UserDashboardComponent } from './app/user-dashboard/user-dashboard.component';
import { AppointmentSuccessComponent } from './app/appointment-success/appointment-success.component';
import { ProfessionalListComponent } from './app/professional-list/professional-list.component';
import { MyBookingsComponent } from './app/my-bookings/my-bookings.component';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(AppComponent, {
  providers: [provideRouter(routes), provideHttpClient()]
});
