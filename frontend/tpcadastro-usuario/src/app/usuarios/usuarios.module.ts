import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CadastrarComponent } from './cadastrar/cadastrar.component';
import { ListarComponent } from './listar/listar.component';
import { UsuariosRoutingModule } from './usuarios-routing.module';

@NgModule({
  declarations: [CadastrarComponent, ListarComponent],
  imports: [CommonModule, ReactiveFormsModule, UsuariosRoutingModule],
})
export class UsuariosModule {}
