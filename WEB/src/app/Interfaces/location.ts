interface LocationWarning {
    id: number;
    number: number;
    side: 'NORTE' | 'SUR' | 'ESTE' | 'OESTE';
    ZIPCode: string;
    street: string;
    imagenUrl?: string;
}