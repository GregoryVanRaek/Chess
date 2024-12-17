import {FormControl, Validators} from '@angular/forms';
import {Category} from '@shared/api/model/category';

export interface TournamentPayload {
  name: string;
  place: string;
  playerMin: number;
  playerMax: number;
  eloMin: number;
  eloMax: number;
  womenOnly: boolean;
  registrationEndDate:Date;
  categories :Category[];
}
