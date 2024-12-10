import { Routes } from '@angular/router';
import {AppNode} from '../common';

export const routes: Routes = [
  {
    path : "",
    redirectTo:AppNode.HOME,
    pathMatch:"full"
  },
  {
    path:AppNode.HOME,
    loadChildren: () => import('../feature/home').then(r => r.homeRoutes)
  }
];
