import {Injectable, signal, WritableSignal} from '@angular/core';
import {ApiService, Tournament, TournamentPayload} from '@shared/api';
import {AppNode} from '@common';
import {Observable, tap} from 'rxjs';
import {AddPlayerPayload} from '@shared/api/data/payload/addplayer.payload';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private apiService :ApiService) {
  }

  newTournament(payload :TournamentPayload){
    return this.apiService.post("tournaments/newtournament", payload).pipe(
      tap((response) => {

      }
    ));
  }

  getAlltournament() :Observable<Tournament[]>{
    return this.apiService.get("tournaments/alltournaments");
  }

  deleteTournament(id :number) :Observable<Tournament>{
    return this.apiService.delete(`tournaments/${id}`)
  }

  startTournament(id :number) :Observable<Tournament>{
    return this.apiService.post(`tournaments/start/${id}`, "");
  }

  addPlayer(payload :AddPlayerPayload){
    return this.apiService.post(`tournaments/addplayer`, payload);
  }

}
