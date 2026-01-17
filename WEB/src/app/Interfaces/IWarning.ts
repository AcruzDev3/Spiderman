import { ILocationWarning } from "./ILocation";

export interface IWarning {
    id: number;
    title: string;
    nameHeroe: string;
    nameCriminnal: string;
    date: Date;
    location:  ILocationWarning;
    // PENDIENTE, para cuando se confirme un aviso
    // AVISADO, cuando se ha confirmado el aviso
    // EN PROCESO, cuando los heroes estan actuando
    // SOLUCIONADO, cuando los heroes han resuelto el caso
    // FINALIZADO, cuando el caso se ha cerrado
    status : 'PENDIENTE'  /* ESPERANDO A CONFIRMARSE EL AVISO */ | 
        'AVISADO' | /* CUANDO SE HA CONFIRMADO EL AVISO */ 
        'EN PROCESO' |  /* CUANDO SE ESTÁ ACTUANDO */ 
        'SOLUCIONADO' /* CUANDO SE HA RESUELTO EL CASO*/ | 
        'FINALIZADO' /* CUANDO SE HA FINALIZADO EL CASO*/;
}