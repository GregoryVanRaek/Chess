import {GenderEnum} from '@shared/api/enum';

export interface Member{
  Id: number;
  Username:string;
  Mail:string;
  Birthday: Date;
  Gender :GenderEnum;
  Elo :number;
  Role :string;
}
