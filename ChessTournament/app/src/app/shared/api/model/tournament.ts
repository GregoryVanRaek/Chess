import {TournamentState} from '@shared/api/enum';

export interface Tournament{
  id :number,
  name :string,
  place :string,
  registeredPlayers :number,
  playerMin :number,
  playerMax :number,
  eloMin :number,
  eloMax :number,
  state :TournamentState,
  actualRound :number,
  womenOnly :boolean,
  registrationEndDate :Date,
  creationDate :Date,
  updateDate :Date,
  categories :any,
  members :any,
  games :any
}
