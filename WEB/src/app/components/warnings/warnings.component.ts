import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { IWarning } from '../../Interfaces/IWarning';
import { CardWarning } from '../card-warning/card-warning.component';

@Component({
  selector: 'app-warnings',
  standalone: true,
  imports: [CommonModule, CardWarning],
  templateUrl: './warnings.component.html',
  styleUrl: './warnings.component.css'
})
export class WarningsComponent {
  /*constructor(private warningService: WarningService) {}
  ngOnInit() {
    this.warningService.getWarnings().subscribe({
      next: (data) => this.warnings = data,
      error: (err) => console.error('Error al cargar los warnings', err)
    });
  }*/
  
  warnings: IWarning[] = [
    {
      id: 1,
      title: 'Robo en el banco central',
      nameHeroe: 'Peter Parker',
      nameCriminnal: 'El Duende Verde',
      date: new Date('2024-06-15T10:30:00'),
      location : {
        id: 1,
        number: 50,
        side: 'NORTH',
        ZIPCode: '28013',
        street: 'Calle Mayor',
      },
      status: 'PENDIENTE'
    },
    {
      id: 2,
      title: 'Asalto en joyería',
      nameHeroe: 'Miles Morales',
      nameCriminnal: 'Venom',
      date: new Date('2024-06-16T14:00:00'),
      location : {
        id: 2,
        number: 22,
        side: 'EAST',
        ZIPCode: '28014',
        street: 'Avenida de la Libertad',
      },
      status: 'EN PROCESO'
    },
    {
      id: 3,
      title: 'Secuestro en el parque',
      nameHeroe: 'Gwen Stacy',
      nameCriminnal: 'Electro',
      date: new Date('2024-06-17T09:15:00'),
      location : {
        id: 3,
        number: 5,
        side: 'SOUTH',
        ZIPCode: '28015',
        street: 'Paseo del Sol',
      },
      status: 'SOLUCIONADO'
    }
  ];
}
