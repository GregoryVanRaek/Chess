import { Component } from '@angular/core';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-home-fallback-page',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './home-fallback-page.component.html',
  styleUrl: './home-fallback-page.component.css'
})
export class HomeFallbackPageComponent {

}
