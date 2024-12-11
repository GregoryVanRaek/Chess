import {computed, effect, EffectRef, inject, Injectable, Signal} from '@angular/core';
import {ApiService, SignInPayload, TokenService} from '@shared/api';
import {Observable, tap} from 'rxjs';
import {AppNode} from '@common';


@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  public isAuthenticated$ : Signal<boolean> = computed(() => this.tokenService.token$().isEmpty) ;
  private isAuthenticatedHandler :EffectRef = effect( () => this.handleAuthenticatedChange(this.isAuthenticated$()));
  private readonly apiService :ApiService = inject(ApiService);
  private router :Router = inject(Router);

  constructor(public tokenService :TokenService) {
  }

  signIn(payload :SignInPayload) : Observable<any>{
    return this.apiService.post(`${AppNode.SECURITY}/${AppNode.LOGIN}`, payload).pipe(
      tap((response) => {
        if(response){
          this.tokenService.setToken({
            token:response,
            isEmpty:false
          })
        }
        else{
          console.log('error');
        }
      })
    );
  }

  private handleAuthenticatedChange(isAuthenticated :boolean):void{
    if(isAuthenticated){
      console.log("is authenticated", isAuthenticated, this.tokenService.token$());
      this.apiService.get(ApiURI.ME).pipe(
        tap((response :ApiResponse) => {
          if(response.result){
            this.account$.set(CredentialUtils.fromDto(response.data));

            if(!window.location.pathname.startsWith(`/${AppNode.REDIRECT_TO_AUTHENTICATED}`)){
              this.router.navigate([AppNode.REDIRECT_TO_AUTHENTICATED]).then();
            }

            return;
          }
          this.router.navigate([AppRoutes.PUBLIC]).then();
        })).subscribe();
      return;
    }
    this.router.navigate([AppRoutes.PUBLIC]).then();
  }


}
