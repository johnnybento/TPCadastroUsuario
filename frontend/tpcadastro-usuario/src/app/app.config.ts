import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { CadastrarComponent } from './usuarios/cadastrar/cadastrar.component';
import { ListarComponent } from './usuarios/listar/listar.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'usuarios/cadastrar',
    component: CadastrarComponent,
  },
  {
    path: 'usuarios/listar',
    component: ListarComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', redirectTo: 'login' },
];
