import {Routes} from '@angular/router';
import {AppNode} from '@common';

export const memberRoutes :Routes = [
    {
      path: '',
      redirectTo: AppNode.MEMBERDETAIL,
      pathMatch: 'full'
    },
    {
      path: AppNode.MEMBERDETAIL,
      loadComponent: () => import('./page').then(r => r.MemberHomePageComponent)
    },
]
