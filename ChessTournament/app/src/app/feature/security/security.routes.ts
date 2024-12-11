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
        redirectTo:AppNode.SIGNIN,
        pathMatch:'full'
      },
      {
        path:AppNode.SIGNIN,
        loadComponent:() => import('./page').then(r => r.SigninPageComponent)
      }
    ]
  },

];
