import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';

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

}
