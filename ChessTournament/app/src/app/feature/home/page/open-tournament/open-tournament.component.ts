import { Component, signal, WritableSignal } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {Tournament} from '@shared/api';
import {HomeService} from '../../service';
import {AppNode} from '@common';

@Component({
  selector: 'app-open-tournament',
  standalone: true,
  imports: [
    RouterLink,
    RouterOutlet
  ],
  templateUrl: './open-tournament.component.html',
  styleUrl: './open-tournament.component.css'
})
export class OpenTournamentComponent {
  tournament$ :WritableSignal<Tournament[]> = signal([]);

  constructor(private homeService :HomeService) {
    this.fetchTournament(5);
  }

  fetchTournament(nb :number) :void{
    this.homeService.getTournament(nb).subscribe({
      next: (response :any) => {
        console.log(response)
        if(response){
          this.tournament$.set(response);
        }
      },
      error : (err :Error) => {
        console.log(err)
      }
    })
  }

  protected readonly AppNode = AppNode;
}
