import { Component } from '@angular/core';
import {RouterLink} from '@angular/router';

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

}
