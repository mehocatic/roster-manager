import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Player } from '../models/player';

@Injectable({ providedIn: 'root' })
export class PlayerService {
  private readonly baseUrl = 'http://localhost:5218/api/players';

  constructor(private http: HttpClient) {}

  getTeamRoster(teamId: number): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.baseUrl}?teamId=${teamId}`);
  }

  getPlayerById(id: number): Observable<Player | null> {
    return this.http.get<Player | null>(`${this.baseUrl}/${id}`);
  }
}
