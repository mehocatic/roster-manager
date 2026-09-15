import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Player } from '../models/player';
import { PlayerService } from '../services/player.service';
import { PlayerEditComponent } from '../player-edit/player-edit.component';

@Component({
  selector: 'app-roster',
  standalone: true,
  imports: [CommonModule, PlayerEditComponent],
  templateUrl: './roster.component.html',
  styleUrl: './roster.component.css'
})
export class RosterComponent implements OnInit {
  readonly teamId = 7;
  readonly teamName = 'Cascade Timberwolves';

  roster: Player[] = [];
  selectedPlayerId: number | null = null;

  constructor(private playerService: PlayerService) {}

  ngOnInit(): void {
    this.playerService.getTeamRoster(this.teamId).subscribe(players => {
      this.roster = players;
    });
  }

  openEditCard(playerId: number): void {
    this.selectedPlayerId = playerId;
  }

  closeEditCard(): void {
    this.selectedPlayerId = null;
  }
}
