import {effect, EffectRef, Injectable, signal, WritableSignal} from '@angular/core';
import {Token} from '@shared/api';
import {environment} from '../../../../environment';
import {isNil} from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  public token$ :WritableSignal<Token> = signal(this.getToken()) ; // signal lancé lors de l'injection du service
  private readonly tokenHandler :EffectRef = effect(() => this.handleTokenChange(this.token$())); // réagir en fonction de la donnée dans le token

  private getToken() :Token {
    const token :string | null = localStorage.getItem(environment.TOKEN_KEY);
    return !isNil(token) ? JSON.parse(token) as Token : this.getEmpty();
  }

  public getEmpty() :Token {
    return {
      token : ''
    }
  }

  public setToken(token :Token) :void{
    if (!isNil(token.token)) {
      this.token$.set(token);
    } else {
      this.token$.set(this.getEmpty());
      localStorage.removeItem(environment.TOKEN_KEY);
    }
  }

  private handleTokenChange(token:Token):void{
    if(isNil(token.token))
      localStorage.setItem(environment.TOKEN_KEY, JSON.stringify(token.token));
    else
      localStorage.removeItem(environment.TOKEN_KEY);
  }

}
