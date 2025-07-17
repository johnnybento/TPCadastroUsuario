import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from '../../core/services/usuario.service';

interface RegisterPayload {
  nome: string;
  email: string;
  senha: string;
}

const passwordMatchValidator: ValidatorFn = (
  form: AbstractControl
): ValidationErrors | null => {
  const pwd = form.get('senha')?.value;
  const confirm = form.get('confirmarSenha')?.value;
  return pwd && confirm && pwd !== confirm ? { passwordsMismatch: true } : null;
};

@Component({
  selector: 'app-cadastrar',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './cadastrar.component.html',
})
export class CadastrarComponent {
  private fb = inject(FormBuilder);
  private svc = inject(UsuarioService);
  private router = inject(Router);

  form = this.fb.group(
    {
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      confirmarSenha: ['', Validators.required],
    },
    {
      validators: passwordMatchValidator,
    }
  );

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const payload: RegisterPayload = {
      nome: this.form.value.nome!,
      email: this.form.value.email!,
      senha: this.form.value.senha!,
    };

    this.svc.register(payload).subscribe({
      next: () => this.router.navigate(['/login']),
      error: (err) => console.error(err),
    });
  }
}
