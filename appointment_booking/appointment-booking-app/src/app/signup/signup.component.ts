import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { UserService } from '../services/user.service';

interface UserRegistrationData {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  password: string;
  role: string;
  isEmailVerified: boolean;
}

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  passwordPattern = '^(?=.*\\d)(?=.*[a-zA-Z]).{8,}$';

  form: UserRegistrationData & { confirmPassword: string } = {
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    password: '',
    confirmPassword: '',
    role: 'user',
    isEmailVerified: false
  };

  constructor(private userService: UserService, private router: Router) {}

  onSubmit(form: NgForm) {
    if (!form.valid) {
      alert('Please fill out all fields correctly.');
      return;
    }

    if (this.form.password !== this.form.confirmPassword) {
      alert('Passwords do not match');
      return;
    }

    const userToRegister: UserRegistrationData = {
      firstName: this.form.firstName,
      lastName: this.form.lastName,
      email: this.form.email,
      phoneNumber: this.form.phoneNumber,
      password: this.form.password,
      role: 'user',
      isEmailVerified: this.form.isEmailVerified
    };

    this.userService.registerUser(userToRegister).subscribe({
      next: (res: any) => {
        console.log('✅ User registered successfully:', res);

        localStorage.setItem('userEmail', this.form.email);

      
        this.router.navigate(['/register-success']);
      },
      error: (err: any) => {
        console.error('❌ Registration error:', err);
        alert('Registration failed. Check backend connection and form fields.');
      }
    });
  }
}
