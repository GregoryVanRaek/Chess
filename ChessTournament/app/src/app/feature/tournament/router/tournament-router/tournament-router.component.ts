import {Component, signal, WritableSignal} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {Member, TokenService} from '@shared/api';

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
  user$ :WritableSignal<Member | null> = signal(null);

  constructor(private tokenService :TokenService) {
    this.loadMember();
  }

  logout():void{
    return this.tokenService.setToken("");
  }

  loadMember():any{
    const token = this.tokenService.token$();

    if (token) {
      this.user$.set(this.tokenService.decodeJwt(token));
    } else {
      console.error('Token is null or undefined');
    }
  }

}
