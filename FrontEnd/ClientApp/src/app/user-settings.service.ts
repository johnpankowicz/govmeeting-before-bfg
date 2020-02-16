import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { UserSettings } from './models/user-settings';

export { UserSettings };

@Injectable({ providedIn: 'root' })
export class UserSettingsService {
  private subject = new Subject<any>();
  private settingsSubject = new Subject<UserSettings>();

  sendLocation(message: string) {
      this.subject.next({ text: message });
  }

  clearMessages() {
      this.subject.next();
  }

  getLocation(): Observable<any> {
      return this.subject.asObservable();
  }


  sendSettings(settings: UserSettings) {
    this.settingsSubject.next(settings);
  }

  getSettings(): Observable<UserSettings> {
    return this.settingsSubject.asObservable();
  }


}
