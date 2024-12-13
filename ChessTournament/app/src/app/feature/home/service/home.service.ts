import { Injectable } from '@angular/core';
import {ApiService, Tournament} from '@shared/api';
import {Observable} from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private apiService :ApiService) { }

  getTournament(nb :number) :Observable<Tournament>{
    return this.apiService.get(`tournaments/open/${nb}`);
}

  getOneTournament(id :number) :Observable<Tournament>{
    return this.apiService.get(`tournaments/${id}`);
  }

}
