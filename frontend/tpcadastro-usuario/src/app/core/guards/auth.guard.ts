import { inject, Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  private router = inject(Router);
  canActivate(): boolean {
    const token = localStorage.getItem('jwt');
    if (token) return true;
    this.router.navigate(['/login']);
    return false;
  }
}
