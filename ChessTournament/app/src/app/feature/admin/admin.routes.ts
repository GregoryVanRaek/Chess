import {Routes} from '@angular/router';
import {AdminRouterComponent} from './router';
import {AppNode} from '@common';

export const adminRoutes :Routes = [
  {
    path: '',
    component: AdminRouterComponent,
    children: [
      {
        path: '',
        loadComponent: () => import('./page').then(c => c.TournamentAdminPageComponent)
      },
      {
        path:AppNode.NEWTOURNAMENT,
        loadComponent: () => import('./page').then(c => c.NewTournamentPageComponent)
      },
      {
        path:AppNode.MANAGETOURNAMENT,
        loadComponent: () => import('./page').then(c => c.StartTournamentPageComponent)
      },
      {
        path:AppNode.NEXTROUND,
        loadComponent: () => import('./page').then(c => c.NextRoundPageComponent)
      },
      {
        path:AppNode.UPDATEMATCH,
        loadComponent: () => import('./page').then(c => c.UpdateMatchPageComponent)
      },
      {
        path:AppNode.INFORMATION,
        loadComponent:() => import('./page').then(c => c.InformationPageComponent)
      }
    ]
  }
]
