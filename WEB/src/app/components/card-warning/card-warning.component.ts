import { Component, Input } from '@angular/core';
import { IWarning } from '../../Interfaces/IWarning';
@Component({
  selector: 'app-card-warning',
  imports: [],
  standalone: true,
  templateUrl: './card-warning.component.html',
  styleUrl: './card-warning.component.css'
})
export class CardWarning {
  @Input() warning!: IWarning; 
}
