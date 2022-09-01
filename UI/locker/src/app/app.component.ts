import { Component, OnInit } from '@angular/core';
import { Locker } from './models/locker.model';
import { LockersService } from './service/lockers.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'locker';
  lockers: Locker[] = [];
  locker: Locker = {
    id: '',
    employeeNumber: '',
    lockerNo: '',
    size: 0,
    location: '',
    isEmpty: true
  };

  constructor(private lockersService: LockersService){

  }

  ngOnInit(): void {
    this.getAllLockers();
  }

  getAllLockers() {
    this.lockersService.getAllLockers()
    .subscribe(
      response => {
        this.lockers = response;
      }
    );
  }

  onSubmit() {
    if(this.locker.id === '') {
      this.lockersService.addLocker(this.locker)
      .subscribe(
        response => {
          this.getAllLockers();
          this.locker = {
            id: '',
            employeeNumber: '',
            lockerNo: '',
            size: 0,
            location: '',
            isEmpty: true
          }
        }
      )
    }
    else {
      this.updateLocker(this.locker);
    }

    
  }

  deleteLocker(id: string) {
    this.lockersService.deleteLocker(id)
    .subscribe(
      response => {
        this.getAllLockers();
      }
    );
  }

  populateForm(locker: Locker) {
    this.locker = locker;
  }

  updateLocker(locker: Locker) {
    this.lockersService.updateLocker(locker)
    .subscribe(
      response => {
        this.getAllLockers();
      }
    )
  }
}
