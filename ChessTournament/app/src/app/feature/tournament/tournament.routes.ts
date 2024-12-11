import { Routes} from '@angular/router';
import {AppNode} from '@common';
import {RegisterPageComponent} from './page/register-page/register-page.component';

export const tournamentRoutes :Routes = [
  {
    path: '',
    redirectTo:AppNode.REGISTER,
    pathMatch:'full'
  },
  {
    path:AppNode.REGISTER,
    loadComponent: () => import('./page').then(r => r.RegisterPageComponent)
  }
]
