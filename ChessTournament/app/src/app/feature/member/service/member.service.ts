import { Injectable } from '@angular/core';
import {ApiService, Member, TokenService} from '@shared/api';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private tokenService :TokenService, private apiService :ApiService) {

  }

  getUserIdFromToken(): Observable<Member> | null {
    const token = this.tokenService.token$(); // Récupérer le token actuel
    let id:number;

    if (token) {
      const decodedToken = this.tokenService.decodeJwt(token);
      id = decodedToken.Id; // Retourner l'ID de l'utilisateur
      return this.apiService.get(`members/${id}`);
    }

    return null ;
  }
}
