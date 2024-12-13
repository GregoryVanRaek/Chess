import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {TokenService} from '@shared/api';

@Component({
  selector: 'app-tournament-router',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink
  ],
  templateUrl: './tournament-router.component.html',
  styleUrl: './tournament-router.component.css'
})
export class TournamentRouterComponent {
  constructor(private tokenService :TokenService) {
  }

  logout():void{
    return this.tokenService.setToken("");
  }

}
