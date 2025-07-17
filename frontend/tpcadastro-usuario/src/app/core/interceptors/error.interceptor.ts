import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { NotificationService } from '../services/notification.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  private notify = inject(NotificationService);

  intercept(
    req: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {        
        const apiError = err.error;
        const msg =
          apiError?.title ||
          apiError?.message ||
          (typeof apiError === 'string' ? apiError : null) ||
          'Erro inesperado no servidor.';
        this.notify.showError(msg);
        return throwError(() => err);
      })
    );
  }
}
