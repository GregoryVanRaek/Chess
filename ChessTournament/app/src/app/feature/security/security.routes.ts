import { Routes } from '@angular/router';
import {AppNode} from '@common';
import {SecurityRouterComponent} from './router';



export const securityRoutes: Routes = [
  {
    path : "",
    component:SecurityRouterComponent,
    children:[
      {
        path:"",
        redirectTo:AppNode.LOGIN,
        pathMatch:'full'
      },
      {
        path:AppNode.LOGIN,
        loadComponent:() => import('./page').then(r => r.SigninPageComponent)
      }
    ]
  },

];
