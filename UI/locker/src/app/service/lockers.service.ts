import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Locker } from '../models/locker.model';

@Injectable({
  providedIn: 'root'
})
export class LockersService {

  baseUrl = 'https://localhost:7281/api/lockers';

  constructor(private http: HttpClient) { }

  
  // Get all lockers
  getAllLockers(): Observable<Locker[]> {
    return this.http.get<Locker[]>(this.baseUrl);
  }

  // add locker
  addLocker(locker: Locker): Observable<Locker> {
    return this.http.post<Locker>(this.baseUrl, locker);
  }

  deleteLocker(lockerNo: string): Observable<Locker> {
    return this.http.delete<Locker>(this.baseUrl + '/' + lockerNo);
  }

  //TODO: change method updateLocker with LockerNo 
  updateLocker(locker: Locker): Observable<Locker> {
    return this.http.put<Locker>(this.baseUrl + '/' + locker.lockerNo, locker);
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
