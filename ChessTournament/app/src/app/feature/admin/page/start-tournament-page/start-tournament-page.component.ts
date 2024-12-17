import {Component, signal, WritableSignal} from '@angular/core';
import {AdminService} from '../../service/admin.service';
import {Tournament} from '@shared/api';
import {DatePipe} from '@angular/common';
import {Observable} from 'rxjs';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-start-tournament-page',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink
  ],
  templateUrl: './start-tournament-page.component.html',
  styleUrl: './start-tournament-page.component.css'
})
export class StartTournamentPageComponent {
  tournament$ :WritableSignal<Tournament[] | null> = signal([]);

  constructor(private adminService: AdminService) {
    this.fetchAllTournament();
  }

  fetchAllTournament() :void{
    this.adminService.getAlltournament().subscribe({
      next : (response) => {
        if(response)
          this.tournament$.set(response);
      }
    });
  }

  startTournament(id :number) :void {
    this.adminService.startTournament(id).subscribe({
      next:(response) => {
        this.fetchAllTournament();
      }
    });
  }

  deleteTournament(id :number) :void{
    this.adminService.deleteTournament(id).subscribe({
      next:(response) => {
        this.fetchAllTournament();
      }
    });
  }

  addPlayer() :void{

  }

}
