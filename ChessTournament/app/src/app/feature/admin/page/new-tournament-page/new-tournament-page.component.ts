import { Component } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import {Router, RouterLink} from '@angular/router';
import {AdminService} from '../../service/admin.service';
import {Category} from '@shared/api/model/category';

@Component({
  selector: 'app-new-tournament-page',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './new-tournament-page.component.html',
  styleUrl: './new-tournament-page.component.css'
})
export class NewTournamentPageComponent {
  public formGroup: FormGroup<any> = new FormGroup<any>({});
  public categories :Category[] = [{name:'Junior'}, {name:'Senior'},{name:'Veteran'}];

  constructor(private adminService :AdminService, private router :Router) {
    this.formGroup = new FormGroup<any>({
      name: new FormControl('', [Validators.required, Validators.minLength(2)],),
      place: new FormControl('', [Validators.required, Validators.minLength(2)],),
      playerMin: new FormControl('', [Validators.required, Validators.min(2), Validators.max(32)],),
      playerMax: new FormControl('', [Validators.required, Validators.min(2), Validators.max(32)],),
      eloMin: new FormControl('', [Validators.required, Validators.min(0), Validators.max(3000)],),
      eloMax: new FormControl('', [Validators.required, Validators.min(0), Validators.max(3000)],),
      womenOnly: new FormControl(false, [Validators.required]),
      registrationEndDate: new FormControl('',
        [Validators.required, this.registrationDateValidator(this.formGroup?.get('playerMin')?.value || 0)]),
      categories :new FormControl('', [Validators.required]),
    });
  }

  registrationDateValidator(playerMin: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const registrationEndDate = new Date(control.value);
      const today = new Date();
      const minDate = new Date(today);
      minDate.setDate(today.getDate() + playerMin);

      return registrationEndDate > minDate
        ? null
        : {invalidRegistrationDate: true};
    };
  }

  create() :void{
    const payload = this.formGroup.value;
    console.log(payload)
    if(this.formGroup.valid){
      this.adminService.newTournament(payload).subscribe({
        next: (response) => {
          if(response){
            this.router.navigate(['/admin/information'])
          }
          else{
            console.log("error during creation");
          }
        },
        error:(err) => {
          console.log(err);
        }
      })
    }

  }

}
