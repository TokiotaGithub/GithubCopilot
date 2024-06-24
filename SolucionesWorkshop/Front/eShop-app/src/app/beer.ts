export interface Beer {
    id: number;
    nombre: string;
    lema: string;
    alcoholPorVolumen: number; // Alcohol by volume
    urlImagen?:string
    primeraProduccion?:string
    descripcion?:string
  }