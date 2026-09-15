import { Component } from '@angular/core';
import { RosterComponent } from './roster/roster.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RosterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'roster-manager-app';
}
