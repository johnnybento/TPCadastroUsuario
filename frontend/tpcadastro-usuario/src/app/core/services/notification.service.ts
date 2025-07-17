import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  private errorSubject = new Subject<string>();
  public error$: Observable<string> = this.errorSubject.asObservable();

  showError(message: string) {
    this.errorSubject.next(message);
  }
}
