import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Player } from '../models/player';
import { PlayerService } from '../services/player.service';

@Component({
  selector: 'app-player-edit',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './player-edit.component.html',
  styleUrl: './player-edit.component.css'
})
export class PlayerEditComponent implements OnChanges {
  @Input({ required: true }) playerId!: number;
  @Output() closed = new EventEmitter<void>();

  player: Player | null = null;

  constructor(private playerService: PlayerService) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['playerId']) {
      this.loadPlayer(this.playerId);
    }
  }

  private loadPlayer(id: number): void {
    this.player = null;
    this.playerService.getPlayerById(id).subscribe(result => {
      this.player = result ?? { id, name: '', teamId: 0, position: '', number: 0 };
    });
  }

  close(): void {
    this.closed.emit();
  }
}
