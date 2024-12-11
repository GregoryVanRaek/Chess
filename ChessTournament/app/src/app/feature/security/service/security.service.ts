import {computed, inject, Injectable, Signal} from '@angular/core';
import {ApiService, SignInPayload, TokenService} from '@shared/api';
import {Observable, tap} from 'rxjs';
import {AppNode} from '@common';


@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  public isAuthenticated$ : Signal<boolean> = computed(() => this.tokenService.token$().isEmpty) ;
  private readonly apiService :ApiService = inject(ApiService);

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

}
