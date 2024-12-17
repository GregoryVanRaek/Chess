import { CanActivateFn } from '@angular/router';
import {TokenService} from '@shared/api';
import {inject} from '@angular/core';

export const adminGuard: CanActivateFn = (route, state) => {
  const tokenService :TokenService = inject(TokenService);

  let token :string = tokenService.token$();

  const role = tokenService.decodeJwt(token).Role;

  return role === 'Admin';
};
