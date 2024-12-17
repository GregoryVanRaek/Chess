import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-admin-router',
  standalone: true,
  imports: [
    RouterLink,
    RouterOutlet
  ],
  templateUrl: './admin-router.component.html',
  styleUrl: './admin-router.component.css'
})
export class AdminRouterComponent {

}
