import { Component } from '@angular/core';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {RouterLink, RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-home-router',
  standalone: true,
  imports: [
    FontAwesomeModule,
    RouterOutlet,
    RouterLink
  ],
  templateUrl: './home-router.component.html',
  styleUrl: './home-router.component.css'
})
export class HomeRouterComponent {


}
