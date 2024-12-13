import { Routes } from '@angular/router';
import {AppNode} from '@common';
import {securityGuard} from '../feature/security/guard';

export const routes: Routes = [
  {
    path : "",
    redirectTo:AppNode.TOURNAMENT,
    pathMatch:"full"
  },
  {
    path:AppNode.HOME,
    loadChildren: () => import('../feature/home').then(r => r.homeRoutes)
  },
  {
    path:AppNode.TOURNAMENT,
    //canActivate:[securityGuard],
    loadChildren: () => import('../feature/tournament').then(r => r.tournamentRoutes)
  },
  {
    path:AppNode.SECURITY,
    loadChildren: () => import('../feature/security').then(r => r.securityRoutes)
  },
  {
    path:AppNode.MEMBER,
    //canActivate:[securityGuard],
    loadChildren: () => import('../feature/member').then(r => r.memberRoutes)
  }
];
