import {Component, Input, OnInit} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router, RouterLink} from '@angular/router';
import {SecurityService} from '../../service/security.service';

@Component({
  selector: 'app-signin-page',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './signin-page.component.html',
  styleUrl: './signin-page.component.css'
})
export class SigninPageComponent implements OnInit {
  public form: FormGroup<any> = new FormGroup<any>({});

  constructor(public securityService: SecurityService, private router:Router) {
  }

  ngOnInit(): void {
      this.form = new FormGroup<any>({
        username : new FormControl('', [Validators.required]),
        password :new FormControl('', [Validators.required]),
      })
  }

  public signIn():void{
    const payload = this.form.value;
    if(this.form.valid){
      this.securityService.signIn(payload).subscribe({
        next: (response) => {
          if(response) {
            setTimeout(() => {
              this.router.navigate(['/tournament']);
            }, 1000);
          }
        },
        error: (err) => {
          console.error('Login failed:', err);
        },
      });
    };
  }

}
