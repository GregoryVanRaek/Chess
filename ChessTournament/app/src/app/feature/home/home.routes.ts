import { Routes} from '@angular/router';

export const homeRoutes :Routes = [
  {
    path: '',
    loadComponent: () => import('./page').then(c => c.HomePageComponent)
  },
  {
    path: '**',
    loadComponent: () => import('./page').then(c => c.HomeFallbackPageComponent)
  }
]
