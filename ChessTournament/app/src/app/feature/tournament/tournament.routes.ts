import { Routes} from '@angular/router';
import {AppNode} from '@common';
import {TournamentRouterComponent} from './router';

export const tournamentRoutes :Routes = [
  {
    path: "",
    component: TournamentRouterComponent,
    children: [
      {
        path: '',
        redirectTo: AppNode.DASHBOARD,
        pathMatch: 'full'
      },
      {
        path: AppNode.DASHBOARD,
        loadComponent: () => import('./page').then(r => r.DashboardComponent)
      },
      {
        path: AppNode.REGISTER,
        loadComponent: () => import('./page').then(r => r.RegisterPageComponent)
      }]
  }
]
