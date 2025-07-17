import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Usuario {
  id?: string;
  nome: string;
  email: string;
}
export interface AuthResponse {
  token: string;
  usuarioId: string;
  email: string;
}
export interface LoginPayload {
  email: string;
  senha: string;
}
@Injectable({ providedIn: 'root' })
export class UsuarioService {
  private baseUsuarios = `${environment.apiUrl}/Usuarios`;
  private baseAuth = `${environment.apiUrl}/Auth`;

  constructor(private http: HttpClient) {}
  login(p: LoginPayload): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseAuth}/login`, p);
  }
  register(u: Usuario & { senha: string }): Observable<any> {
    return this.http.post(this.baseUsuarios, u);
  }

  list(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(this.baseUsuarios);
  }
}
