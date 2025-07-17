import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UsuarioService } from '../core/services/usuario.service';

interface LoginPayload {
  email: string;
  senha: string;
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private svc = inject(UsuarioService);
  private router = inject(Router);

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    senha: ['', Validators.required],
  });

  error = '';

  submit() {
    if (this.form.invalid) return;

    const payload = this.form.value as LoginPayload;

    this.svc.login(payload).subscribe({
      next: (res) => {
        localStorage.setItem('jwt', res.token);
        localStorage.setItem('email', res.email);
        this.router.navigate(['/usuarios/listar']);
      },
      error: (err) => {
        console.error(err);
        this.error = 'Usuário ou senha inválidos';
      },
    });
  }
}
