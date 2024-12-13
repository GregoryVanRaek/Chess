import { Routes} from '@angular/router';
import {HomeRouterComponent} from './router/home-router/home-router.component';
import {AppNode} from '../../common';

export const homeRoutes :Routes = [
  {
    path: '',
    component: HomeRouterComponent,
    children: [
      {
        path: '',
        loadComponent: () => import('./page').then(c => c.HomePageComponent)
      },
      {
        path: AppNode.OPENTOURNAMENT,
        loadComponent:() => import('./page').then(c => c.OpenTournamentComponent),
      },
      {
        path:`${AppNode.OPENTOURNAMENTDETAIL}/:id`,
        loadComponent: () => import('./page').then(c => c.TournamentDetailComponent)
      },
      {
        path: '**',
        loadComponent: () => import('./page').then(c => c.HomeFallbackPageComponent)
      }
    ]
  }
]
