import { Injectable } from '@angular/core';

export interface TournamentStatus {
  message: string;
  isReadyForPrize: boolean;
  isReadyForForcedEnd: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TournamentService {
  public getTournamentStatus(tournament: any): TournamentStatus {
    const startDate = new Date(tournament.startDate);
    const endDate = new Date(tournament.endDate);
    const currentDate = new Date();

    if (tournament.isWinnerSelected) {
      return { message: "Winner selected", isReadyForPrize: false, isReadyForForcedEnd: false };
    }

    if (startDate > currentDate) {
      return { message: 'Registration open', isReadyForPrize: false, isReadyForForcedEnd: false };
    }

    if (startDate < currentDate && currentDate < endDate) {
      return { message: 'Registration closed', isReadyForPrize: false, isReadyForForcedEnd: true };
    }

    return { message: 'Ended', isReadyForPrize: true, isReadyForForcedEnd: false };
  }
}
