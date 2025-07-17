import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { Usuario, UsuarioService } from '../../core/services/usuario.service';

@Component({
  selector: 'app-listar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './listar.component.html',
})
export class ListarComponent implements OnInit {
  private svc = inject(UsuarioService);
  users: Usuario[] = [];

  ngOnInit() {
    this.svc.list().subscribe({
      next: (list) => (this.users = list),
      error: (err) => console.error(err),
    });
  }
}
