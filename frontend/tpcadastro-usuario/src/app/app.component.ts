import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ErrorBannerComponent } from './shared/error-banner.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, ErrorBannerComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {
  private router = inject(Router);

  get isLoggedIn(): boolean {
    return !!localStorage.getItem('jwt');
  }

  get email(): string | null {
    return localStorage.getItem('email');
  }

  logout(): void {
    localStorage.removeItem('jwt');
    localStorage.removeItem('email');
    this.router.navigate(['/login']);
  }
}
