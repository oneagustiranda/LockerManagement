import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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
    locker.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Locker>(this.baseUrl, locker);
  }

  deleteLocker(id: string): Observable<Locker> {
    return this.http.delete<Locker>(this.baseUrl + '/' + id);
  }

  updateLocker(locker: Locker): Observable<Locker> {
    return this.http.put<Locker>(this.baseUrl + '/' + locker.id, locker);
  }
}
