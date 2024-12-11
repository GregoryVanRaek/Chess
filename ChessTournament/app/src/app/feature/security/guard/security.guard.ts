import {CanActivateFn, Router} from '@angular/router';
import {SecurityService} from '../service/security.service';
import {inject} from '@angular/core';
import {AppNode} from '@common';

export const securityGuard: CanActivateFn = (route, state) => {

  const securityService :SecurityService = inject(SecurityService);
  const router :Router = inject(Router);

  const isAuthenticated = securityService.isAuthenticated$();

  if(isAuthenticated)
    return router.createUrlTree([AppNode.REDIRECT_TO_AUTHENTICATED])

  return !isAuthenticated;
};
