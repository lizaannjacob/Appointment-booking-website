import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'http://localhost:7005/api/user';

  constructor(private http: HttpClient) {}

  registerUser(userData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, userData)
      .pipe(catchError(this.handleError));
  }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, credentials)
      .pipe(catchError(this.handleError));
  }

  
  private handleError(error: HttpErrorResponse) {
    console.error('Backend returned error:', error);
    return throwError(() => new Error('Something went wrong during HTTP call'));
  }
}
