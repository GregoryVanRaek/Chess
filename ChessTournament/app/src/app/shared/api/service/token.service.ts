import {effect, EffectRef, Injectable, signal, WritableSignal} from '@angular/core';
import {environment} from '@environment';
import {isNil} from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  public token$ :WritableSignal<string> = signal(this.getToken()) ; // signal lancé lors de l'injection du service
  private readonly tokenHandler :EffectRef = effect(() => this.handleTokenChange(this.token$())); // réagir en fonction de la donnée dans le token

  private getToken() :string {
    const token :string | null = localStorage.getItem(environment.TOKEN_KEY);

    return !isNil(token) ? token : "";
  }

  public setToken(token :string) :void{
    if (!isNil(token)) {
      this.token$.set(token);
    } else {
      this.token$.set("");
      localStorage.removeItem(environment.TOKEN_KEY);
    }
  }

  private handleTokenChange(token:string):void{
    if(token)
      localStorage.setItem(environment.TOKEN_KEY, token);
    else
      localStorage.removeItem(environment.TOKEN_KEY);
  }

}
