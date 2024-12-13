import {computed, effect, EffectRef, inject, Injectable, Signal} from '@angular/core';
import {ApiService, SignInPayload, TokenService} from '@shared/api';
import {Observable, tap} from 'rxjs';
import {AppNode} from '@common';
import {Router} from '@angular/router';
import {environment} from '@environment';
import {isNil} from 'lodash';


@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  public isAuthenticated$ : Signal<boolean> = computed(() => !this.tokenService.token$()) ;
  private isAuthenticatedHandler :EffectRef = effect( () => this.handleAuthenticatedChange(this.isAuthenticated$()));
  private readonly apiService :ApiService = inject(ApiService);
  private router :Router = inject(Router);

  constructor(public tokenService :TokenService) {
  }

  signIn(payload: SignInPayload): Observable<string> {
    return this.apiService.post<string>(`${AppNode.SECURITY}/${AppNode.LOGIN}`, payload, { responseType: 'text' }).pipe(
      tap((response: string) => {
        if (response) {
          this.tokenService.setToken(response);
        } else {
          console.error('Erreur : réponse vide');
        }
      })
    );
  }

  private handleAuthenticatedChange(isAuthenticated :boolean):void{
    if(isAuthenticated){
      console.log("is authenticated", isAuthenticated, this.tokenService.token$());

      this.apiService.get(environment.apiURL).pipe(
        tap((response :any) => {
          if(response){
            if(!window.location.pathname.startsWith(`/${AppNode.REDIRECT_TO_AUTHENTICATED}`)){
              this.router.navigate([AppNode.REDIRECT_TO_AUTHENTICATED]).then();
            }
            return;
          }
          this.router.navigate([AppNode.HOME]).then();
        })).subscribe();
      return;
    }
    this.router.navigate([AppNode.HOME]).then();
  }


}
