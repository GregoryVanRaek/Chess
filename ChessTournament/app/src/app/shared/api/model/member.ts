import {GenderEnum} from '@shared/api/enum';

export interface Member{
  id: number;
  username:string;
  mail:string;
  birthday: Date;
  gender :GenderEnum;
  elo :number;
}
