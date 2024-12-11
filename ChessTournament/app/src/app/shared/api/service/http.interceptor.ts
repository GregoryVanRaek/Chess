import {HttpHandlerFn, HttpInterceptorFn, HttpRequest} from '@angular/common/http';
import {environment} from '@environment';
import {AppNode} from '@common';
import {Router} from '@angular/router';
import {inject} from '@angular/core';
import {TokenService} from '@shared/api';
import { EMPTY, Observable} from 'rxjs';


const baseURL: string = environment.apiURL;
const publicRoute: string[] = [`${baseURL}`, `${baseURL}${AppNode.SECURITY}/${AppNode.LOGIN}`, `${baseURL}${AppNode.SECURITY}/${AppNode.SIGNUP}`,
  `${baseURL}${AppNode.HOME}`, `${baseURL}${AppNode.HOME}/${AppNode.OPENTOURNAMENT}`,`${baseURL}${AppNode.HOME}/${AppNode.OPENTOURNAMENTDETAIL}`
];

export const httpInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
  // si route publique reprise ci dessus, on laisse passer
  if (!req.url.startsWith(baseURL) || publicRoute.includes(req.url)) {
    return next(req);
  }

  // si pas public
  const tokenService = inject(TokenService);
  const router: Router = inject(Router);

  if (!tokenService.token$().isEmpty) {
    return next(setTokenInHeader(req, tokenService.token$().token));
  }

  return redirectToPublic(router);
}

// ajouter le token dans le header de la requête
const setTokenInHeader :any = (req: HttpRequest<any>, token: string): HttpRequest<any> => {
  return req.clone({
    headers: req.headers.set('Authorization', `Bearer ${token}`)
  });
}

// renvoyer vers une route public (dans les cas ou on est pas autorisé)
const redirectToPublic: (router:Router) => Observable<any> = (router:Router) => {
  if(!window.location.pathname.startsWith(`/${AppNode.HOME}`)){
    router.navigate([AppNode.REDIRECT_TO_PUBLIC]).then();
  }
  return EMPTY;
}



