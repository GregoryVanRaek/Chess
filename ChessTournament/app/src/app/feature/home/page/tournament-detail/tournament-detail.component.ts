import {Component, signal, WritableSignal} from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {HomeService} from '../../service';
import {Tournament} from '@shared/api';

@Component({
  selector: 'app-tournament-detail',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './tournament-detail.component.html',
  styleUrl: './tournament-detail.component.css'
})
export class TournamentDetailComponent {
  tournament$ :WritableSignal<Tournament | null> = signal(null);
  tournamentId :number;

  constructor(private homeservice :HomeService, private route: ActivatedRoute) {
    this.fetchTournament();
    this.tournamentId = Number(this.route.snapshot.paramMap.get('id'));
  }

  fetchTournament() : void{
    this.homeservice.getOneTournament(this.tournamentId).subscribe({
      next: (response) => {
        if(response){
          this.tournament$.set(response);
        }
      },
      error : (err :Error) => {
        console.log(err)
      }
    })
  }

}
