import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { NotificationService } from '../core/services/notification.service';

@Component({
  selector: 'app-error-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './error-banner.component.html',
  styleUrls: ['./error-banner.component.css'],
})
export class ErrorBannerComponent implements OnInit {
  private notify = inject(NotificationService);
  message = '';

  ngOnInit() {
    this.notify.error$.subscribe((msg) => (this.message = msg));
  }

  clear() {
    this.message = '';
  }
}
