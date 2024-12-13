import {Component, signal, WritableSignal} from '@angular/core';
import {RouterLink} from '@angular/router';
import {MemberService} from '../../service';
import {Member} from '@shared/api';

@Component({
  selector: 'app-member-home-page',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './member-home-page.component.html',
  styleUrl: './member-home-page.component.css'
})
export class MemberHomePageComponent {
  member$: WritableSignal<Member | null> = signal(null);

  constructor(private memberService :MemberService) {
    this.fetchMemberData();
  }

  private fetchMemberData(): void {
    this.memberService.getUserIdFromToken()?.subscribe({
      next: (response: Member) => {
        if (response) {
          this.member$.set(response);
        }
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

}



