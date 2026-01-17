export interface ILocationWarning {
    id: number;
    number: number;
    side: 'NORTH' | 'SOUTH' | 'EAST' | 'WEST';
    ZIPCode: string;
    street: string;
    imagenUrl?: string;
}