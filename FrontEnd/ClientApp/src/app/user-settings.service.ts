import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { UserSettings, LocationType } from './models/user-settings';

export { UserSettings, LocationType };

@Injectable({ providedIn: 'root' })
export class UserSettingsService {
  //private subject = new Subject<any>();
  private settingsSubject = new Subject<UserSettings>();
  private bSubject = new BehaviorSubject("a");

  clearMessages() {
      this.settingsSubject.next();
  }

  sendSettings(settings: UserSettings) {
    this.settingsSubject.next(settings);
  }

  getSettings(): Observable<UserSettings> {
    return this.settingsSubject.asObservable();
  }

  sendBSubject(message: string){
    this.bSubject.next(message);
  }

  getBSubject() {
    return this.bSubject;
  }



}
