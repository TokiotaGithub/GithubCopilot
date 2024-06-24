# Generación de una aplicación angular usando copilot

## Índice
- [Objetivo](#objetivo)
- [Requisitos](#requisitos)
- [Pasos](#pasos)
   - [Generar la aplicación angular](#generar-la-aplicación-angular)
   - [Generar los componentes, el servicio y el modelo de datos](#generar-los-componentes-el-servicio-y-el-modelo-de-datos)
   - [Generar el CRUD en el servicio Beers e implenentar las llamadas en el componente BeerList](#generar-el-crud-en-el-servicio-beers-e-implenentar-las-llamadas-en-el-componente-beerlist)
   - [Generar el contenido html y eventos en los componentes Beer y BeerList](#generar-el-contenido-html-y-eventos-en-los-componentes-beer-y-beerlist)
   - [Generar la configuración del routing](#generar-la-configuración-del-routing)
   - [Enlazar el componete Beer (detalle) al seleccionar una cerveza](#enlazar-el-componete-beer-detalle-al-seleccionar-una-cerveza)
   - [Pruebas y correcciones](#pruebas-y-correcciones)
- [Guía Rápida](#guía-rápida-de-pasos)
- [Ejercicios](#ejercicios)
- [Recursos](#recursos)

## Objetivo
El objetivo de este ejercicio es generar una aplicación en angular completa con ayuda de Copilot. 

## Requisitos
- Visual Studio Code
- Node 18 o superior
- Angular CLI
- Tener instalado las extensiones de GitHub Copilot y GitHub Copilot Chat


## Pasos
### Generar la aplicación angular
Sitúese en el directorio donde se desea generar la app, abra visual studio code y luego el chat de copilot y ejecute el siguiente prompt. 

#### Prompt (Crear aplicación en modo standalone)
```
- Genera una aplicación angular con el nombre 'eShop-app'
- Usa CSS por defecto para los estilos
- Habilita el routing
```

#### Respuesta
```
Para generar una nueva aplicación Angular con el nombre 'eShop-app', utilizando CSS para los estilos y habilitando el routing, puedes seguir los siguientes pasos:

Primero, necesitas tener instalado Node.js y npm en tu sistema. Luego, puedes instalar Angular CLI globalmente en tu sistema utilizando npm.
#### Respuesta
```
```shell
npm install -g @angular/cli
```
```
Luego, puedes generar una nueva aplicación Angular con el nombre 'eShop-app' utilizando el comando ng new. Cuando se te pregunte "Would you like to add Angular routing?", selecciona "Yes". Cuando se te pregunte "Which stylesheet format would you like to use?", selecciona "CSS".
Ahora, vamos a generar la aplicación Angular con el nombre eShop-app, con CSS por defecto y routing habilitado:
```
```shell
ng new eShop-app --routing --style=css
```
```
Este comando creará una nueva aplicación Angular con el nombre 'eShop-app'. El flag --routing habilitará el sistema de routing de Angular, el flag --style=css establecerá CSS como el preprocesador de estilos por defecto.

Finalmente, navega a la carpeta de la aplicación y ejecuta la aplicación.
Navega al directorio de la nueva aplicación:
```
```shell
cd eShop-app
ng serve -o
```
```
Ahora, tu aplicación Angular está corriendo en http://localhost:4200/.
```

#### Observaciones

- En este ejemplo, se creó una aplicación en modo `standalone`, por ello, no aparecerá el fichero `app.module.ts` ya que en angular ahora es el comportamiento predeterminado, aunque, en ocaciones te puede ofrecer el contenido básico de app.routing.module.ts. Esto corresponde a una versión antigua de Angular.
- Si se desea crear una aplicación usando dichos módulos como se hacía hasta ahora, entonces puedes usar el siguente prompt con el flag `--no-standalone`:

```
- Genera una aplicación angular con el nombre 'eShop-app' con el flag --no-standalone
- Usa CSS por defecto para los estilos
- Habilita el routing
```


### Generar los componentes, el servicio y el modelo de datos

Abra visual studio code para esa aplicación eShop-app

#### Prompt
```
- Agrega el componente BeerList
- Agrega el componente Beer
- Agrega el servicio Beers
- Agrega la interface Beer con las siguientes propeidades: id, nombre, lema, primeraProduccion, descripcion, urlImagen, precio, alcoholPorVolumen
```

#### Respuesta
```
Para agregar los componentes, la interfaz y el servicio solicitados en tu aplicación Angular, puedes usar los siguientes comandos en la terminal:

Primero, agrega el componente BeerList:
```
```shell
ng generate component BeerList
```
```
Luego, para agregar el componente Beer, puedes usar el mismo comando:
Genera el modelo Beer:
```
```shell
ng generate component Beer
```
```
Para agregar la interfaz Beer, puedes crear un nuevo archivo en la carpeta src/app llamado beer.interface.ts. En este archivo, puedes definir la interfaz Beer y sus propiedades. Aquí hay un ejemplo de cómo podría ser:
Generamos el servicio Beers:
```
```ts
export interface Beer {
  id: number;
  nombre: string;
  lema: string;
  primeraProduccion: string;
  descripcion: string;
  urlImagen: string;
  precio: number;
  alcoholPorVolumen: number;
}
```
```
Finalmente, para agregar el servicio Beers, puedes usar el comando ng generate service o ng g s:
```
```shell
ng generate service Beers
```
```
Este comando creará un nuevo servicio llamado Beers que puedes usar para manejar la lógica relacionada con las cervezas en tu aplicación.
```

### Generar el CRUD en el servicio Beers e implenentar las llamadas en el componente BeerList

#### Prompt
```
- Genera un CRUD para el modelo Beer en el servicio Beers con llamadas http a un api
- Implementa todas las llamadas al servicio Beers en los métodos de cada evento en el componente BeerList
```

#### Respuesta
```
Primero, vamos a generar un CRUD para el modelo cervezas en el servicio Beers. Necesitarás importar HttpClient de @angular/common/http para hacer las llamadas HTTP. Asegúrate de que tu API esté configurada correctamente para recibir estas solicitudes.
```
```ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Beer } from './beer';

@Injectable({
  providedIn: 'root'
})
export class BeersService {
  private apiUrl = 'http://localhost:3000/api/beers'; // replace with your API URL

  constructor(private http: HttpClient) { }

  getBeers(): Observable<Beer[]> {
    return this.http.get<Beer[]>(this.apiUrl);
  }

  getBeer(id: number): Observable<Beer> {
    return this.http.get<Beer>(`${this.apiUrl}/${id}`);
  }

  createBeer(beer: Beer): Observable<Beer> {
    return this.http.post<Beer>(this.apiUrl, beer);
  }

  updateBeer(beer: Beer): Observable<Beer> {
    return this.http.put<Beer>(`${this.apiUrl}/${beer.id}`, beer);
  }

  deleteBeer(id: number): Observable<Beer> {
    return this.http.delete<Beer>(`${this.apiUrl}/${id}`);
  }
}
```
```
Luego, en el componente BeerList, puedes implementar las llamadas al servicio Beers en los métodos de cada evento. Asegúrate de suscribirte a los Observables devueltos por los métodos del servicio para que las solicitudes HTTP se realicen.
```
```ts
import { Component, OnInit } from '@angular/core';
import { BeersService } from '../beers.service';
import { Beer } from '../beer';

@Component({
  selector: 'app-beer-list',
  templateUrl: './beer-list.component.html',
  styleUrls: ['./beer-list.component.css']
})
export class BeerListComponent implements OnInit {
  beers: Beer[];

  constructor(private beersService: BeersService) { }

  ngOnInit(): void {
    this.getBeers();
  }

  getBeers(): void {
    this.beersService.getBeers().subscribe(beers => this.beers = beers);
  }

  addBeer(beer: Beer): void {
    this.beersService.createBeer(beer).subscribe(beer => this.beers.push(beer));
  }

  updateBeer(beer: Beer): void {
    this.beersService.updateBeer(beer).subscribe(() => this.getBeers());
  }

  deleteBeer(beer: Beer): void {
    this.beersService.deleteBeer(beer.id).subscribe(() => this.getBeers());
  }
}
```

#### Observaciones
1 En el servicio  `beers.service.ts` hacer los siguiente:
- Reemplace 'http://your-api-url.com/api/beers' con la URL de tu API real, en este caso de ejemplo, se reemplazó por: `http://localhost:5132/Cerveza`.
- Es posible que tengas que ajustar algunos endpoints también. Por ejemplo, en el método `getBeers` debes añadir al final de la composición de la ruta lo siguiente `?limit=100` para poder obtener los datos correctamente. Ejemplo: `${this.apiUrl}?limit=100`

2 En muchas ocasiones obtendrémos un error en `beer.component.ts` por no inicializar nunca la variable beer, podemos probar a ver que soluciones ofrece copilot.

Seleccionando la linea y usando el comando /fix en el chat, nos ofrece esta respuesta
```
El error se debe a que la propiedad beers no se inicializa en el constructor y TypeScript está configurado para asegurarse de que todas las propiedades se inicialicen. Podemos solucionar este problema inicializando la propiedad beers como un array vacío en su declaración. Aquí está el código corregido:
```
```ts
beers: Beer[] = [];
```


### Generar el contenido html y eventos en los componentes Beer y BeerList

#### Prompt
```
- Genera una tabla con sus estilos css para Beers en angular en el componente BeerList con los botones de acciones para modificar Beer y eliminar Beer teniendo en cuenta los nombres de los metodos disponibles en beer-list.component.ts 
- Genera el contenido html para mostrar los detalles de una Beer en el componente Beer con sus respectivos estilos css
- Genera llamada al servicio Beers para obtener detalles de una cerveza en el componente Beer
```

#### Respuesta
```
Primero, vamos a generar un grid en HTML para listar cervezas en el componente BeerList. Este grid incluirá botones para modificar y eliminar cervezas.
```
```html
<table>
  <thead>
    <tr>
      <th>ID</th>
      <th>Nombre</th>
      <th>Lema</th>
      <th>Primera Producción</th>
      <th>Descripción</th>
      <th>Precio</th>
      <th>Alcohol por Volumen</th>
      <th>Acciones</th>
    </tr>
  </thead>
  <tbody>
    <tr *ngFor="let beer of beers">
      <td>{{beer.id}}</td>
      <td>{{beer.nombre}}</td>
      <td>{{beer.lema}}</td>
      <td>{{beer.primeraProduccion}}</td>
      <td>{{beer.descripcion}}</td>
      <td>{{beer.precio}}</td>
      <td>{{beer.alcoholPorVolumen}}</td>
      <td>
        <button (click)="updateBeer(beer)">Modificar</button>
        <button (click)="deleteBeer(beer)">Eliminar</button>
      </td>
    </tr>
  </tbody>
</table>
```
```
En este ejemplo, beers es un array de objetos, donde cada objeto representa una cerveza y tiene propiedades como name, type y description. Asegúrate de reemplazar estas propiedades con las propiedades reales de tus objetos de cerveza.

Luego, en tu archivo de estilos .css:
```
```css
table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  padding: 15px;
  text-align: left;
  border-bottom: 1px solid #ddd;
}

th {
  background-color: #4CAF50;
  color: white;
}

tr:hover {background-color: #f5f5f5;}
```
```
Luego, vamos a generar el html del detalle de una cerveza en el componente Beer.

Aquí hay un ejemplo de cómo podrías hacerlo:
```
```html
<div class="beer-detail">
  <h2>{{beer.nombre}}</h2>
  <p>{{beer.lema}}</p>
  <p>Primera Producción: {{beer.primeraProduccion}}</p>
  <p>Descripción: {{beer.descripcion}}</p>
  <img src="{{beer.urlImagen}}" alt="{{beer.nombre}}">
  <p>Precio: {{beer.precio}}</p>
  <p>Alcohol por Volumen: {{beer.alcoholPorVolumen}}</p>
</div>
```
```
Luego, en tu archivo de estilos .css:
```
```css
.beer-detail {
  width: 100%;
  padding: 20px;
  box-sizing: border-box;
  background-color: #f5f5f5;
  border-radius: 10px;
  margin-bottom: 20px;
}

.beer-detail h2 {
  margin: 0 0 10px 0;
  color: #333;
}

.beer-detail .beer-type {
  font-size: 18px;
  color: #666;
  margin: 0 0 20px 0;
}

.beer-detail .beer-image {
  width: 100%;
  height: auto;
  margin-bottom: 20px;
}

.beer-detail .beer-description {
  font-size: 16px;
  color: #333;
}
```
```
Finalmente, vamos a generar una llamada al servicio Beers para obtener los detalles de una cerveza en el componente Beer.
```
```ts
// beer.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BeersService } from '../beers.service';
import { Beer } from '../beer.interface';

@Component({
  selector: 'app-beer',
  templateUrl: './beer.component.html',
  styleUrls: ['./beer.component.css']
})
export class BeerComponent implements OnInit {
  beer: Beer;

  constructor(
    private route: ActivatedRoute,
    private beersService: BeersService
  ) {}

  ngOnInit(): void {
    this.getBeer();
  }

  getBeer(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.beersService.getBeer(id)
      .subscribe(beer => this.beer = beer);
  }
}
```

#### Observaciones
Este código asume que estás utilizando el enrutamiento de Angular y que el ID de la cerveza se pasa como un parámetro de ruta. Para solucionar el error en el componnete `beer.component.ts` modifica lo siguiente:
- Cambia la siguiente línea `const id = this.route.snapshot.paramMap.get('id');` por lo siguiente: `const id:number = Number(this.route.snapshot.paramMap.get('id'));`.
- `beer: Beer` por `beer!: Beer;`

Para que los nombres de los métodos sean correctos hemos tenido que indicarle que utilice los nombres del componente, si no su tendencia era a usar editBeer en lugar de updateBeer.

### Generar la configuración del routing

#### Prompt
```
Configura todas las rutas para los componentes 'BeerList', 'Beer' y una ruta por defecto
```

#### Respuesta
```
Para configurar las rutas para los componentes BeerList, Beer y una ruta por defecto, puedes agregar las siguientes configuraciones de ruta en tu archivo app-routing.module.ts:
```
```ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BeerListComponent } from './beer-list/beer-list.component';
import { BeerComponent } from './beer/beer.component';

const routes: Routes = [
  { path: 'beers', component: BeerListComponent },
  { path: 'beer/:id', component: BeerComponent },
  { path: '', redirectTo: '/beers', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
```

#### Observaciones 

En este código, hemos definido tres rutas:

La ruta 'beers' muestra el componente BeerListComponent.
La ruta 'beer/:id' muestra el componente BeerComponent y pasa el parámetro de ruta id a este componente. La ruta por defecto ('') redirige a /beers.
Asegúrate de que los nombres de los componentes y las rutas coinciden con los de tu aplicación.

1. Si creaste la aplicaciñón en modo `standalone` entonces solo debes copiar la constante y pegalo en `app.routes.ts`:
```ts
const routes: Routes = [
  { path: 'beers', component: BeerListComponent },
  { path: 'beers/:id', component: BeerComponent },
  { path: '', redirectTo: '/beers', pathMatch: 'full' },
];
```

2. Si creaste la aplicación usando módulos, entonces puedes copiar el resultado de la respuesta de copilot y pegarlo en el módulo de routing que corresponde.

3. Se debe añadir  `provideHttpClient()` al `app.config.ts` en la sección de providers. Debería quedar de la siguiente manera el fichero `app.config.ts`:
```ts
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideHttpClient()]
};
```

4. Se debe importar el `ngFor` y `RouterLink` en el componente `beer-list.component.ts`. Si seguistes los pasos con la aplicación en modo `standalone` solo debes copiar lo siguiente y pegarlo en el decorador de `beer-list.component.ts`:
```ts
standalone: true,
providers: [BeersService],
imports: [NgFor,RouterLink, CommonModule]
```

El decorador del `beer-list.component.ts`debería verse de la siguiente mnanera:
```ts
@Component({
  selector: 'app-beer-list',
  templateUrl: './beer-list.component.html',
  styleUrls: ['./beer-list.component.css'],
  standalone: true,
  providers: [BeersService],
  imports: [NgFor, RouterLink, CommonModule]
})
export class BeerListComponent implements OnInit {
```

5. En el getBeer de beer.component.ts hay que indicar que el id es un numero

6. Pueden aparecer otros errores que aun debamos solucionar.
En este punto podemos arrancar la app y mostrar la lista de cervezas.

### Enlazar el componete Beer (detalle) al seleccionar una cerveza

#### Prompt
```
Permite que al clickar el nombre de una cerveza viaje a sus detalles
```

#### Respuesta
```
Para navegar a los detalles de la cerveza cuando se hace clic en el nombre de la cerveza, puedes utilizar la directiva de enlace de atributos [routerLink] de Angular. Esta directiva te permite enlazar un elemento HTML a una ruta específica.
```
```html
<td><a [routerLink]="['/beer', beer.id]">{{beer.nombre}}</a></td>
```

En este código, cuando se hace click en el nombre de la cerveza, se navega a la ruta '/beer/:id', donde ':id' es el id de la cerveza.

### Agregar el router oulet en el app component

Finalmente, ve al fichero app.component.html, borra todo su contenido y agrega lo siguiente 
```html
<router-outlet></router-outlet>
```

### Pruebas y correcciones

Con todos los pasos que llevamos hasta ahora, nuestra aplicación angular ya estaría implementada. Pero, como en cualquier desarrollo, queda hacer pruebas y revisar si ocurre algún error. 

Para ello, vamos a ejecutar la aplicación usando el siguiente comando:
```
ng serve -o
```
Algunos errores que se pueden presentar en este punto pueden ser:


#### Falta de configuración de los CORS
Para poder llamar al back desde el front se necesita activar las politicas  de CORS, preguntamos a copilot como hacerlo para el backend

##### Prompt
```
Incluye la configuración de CORS en esta solución
```
##### Respuesta
```
Para agregar CORS en una aplicación .NET 6.0 (la versión más reciente hasta la fecha, ya que .NET 8.0 aún no existe), puedes hacerlo en el archivo Startup.cs. Aquí hay un ejemplo de cómo hacerlo:
```
```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy",
            builder =>
            {
                builder.WithOrigins("http://example.com")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        // Otros servicios...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseCors("MyPolicy");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
```
```
En este ejemplo, se está permitiendo el acceso desde
http://example.com.
Puedes cambiar esto a cualquier origen que necesites. También se están permitiendo todos los métodos y encabezados HTTP.
Recuerda que debes llamar a UseCors() después de UseRouting() y antes de UseEndpoints().
```

## Guía rápida de pasos
AVISO. Es posible que las respuestas que retorna Copilot no se correspondan con las obtenidas durante la elaboración de esta guía, por lo que puede ser necesario que hagamos tareas adicionales o prescindamos de ciertos prompts. Los prompts que se muestran a continuación son una mera guía.

### Generar la aplicación angular
```
- Genera una aplicación angular con el nombre 'eShop-app'
- Usa CSS por defecto para los estilos
- Habilita el routing
```

### Generar los componentes, el servicio y el modelo de datos
```
- Agrega el componente BeerList
- Agrega el componente Beer
- Agrega el servicio Beers
- Agrega la interface Beer con las siguientes propeidades: id, nombre, lema, primeraProduccion, descripcion, urlImagen, precio, alcoholPorVolumen
```

### Generar el CRUD en el servicio Beers e implenentar las llamadas en el componente BeerList
```
- Genera un CRUD para el modelo Beer en el servicio Beers con llamadas http a un api
- Implementa todas las llamadas al servicio Beers en los métodos de cada evento en el componente BeerList
```

### Generar el contenido html y eventos en los componentes Beer y BeerList
```
- Genera una tabla con sus estilos css para Beers en angular en el componente BeerList con los botones de acciones para modificar Beer y eliminar Beer teniendo en cuenta los nombres de los metodos disponibles en beer-list.component.ts 
- Genera el contenido html para mostrar los detalles de una Beer en el componente Beer con sus respectivos estilos css
- Genera llamada al servicio Beers para obtener detalles de una cerveza en el componente Beer
```

### Generar la configuración del routing
```
Configura todas las rutas para los componentes 'BeerList', 'Beer' y una ruta por defecto
```

### Enlazar el componete Beer (detalle) al seleccionar una cerveza
```
Permite que al clickar el nombre de una cerveza viaje a sus detalles
```

## Ejercicios
Para afianzar conocimientos, puedes intentar ampliar la aplicación angular con la ayuda de Copilot:
- Agrega paginación en el componente beer-list (listado de cervezas).
- Agrega un nuevo componente que contenga un formulario para agregar una nueva cerveza.
- Añade un nuevo botón para agregar una nueva cerveza en el componente beer-list (debe abrir el formulario de agregar cerveza en una modal al pulsarlo).
- Prueba los métodos para añadir, actualizar y eliminar cervezas llamando al api.
- Agrega test unitarios a los componentes de la aplicación.

## Recursos
Este ejemplo se ha tomado de uno de los 'challenges' que utiliza Microsoft en sus 'hackatons'. Para más información, visitar: https://github.com/microsoft/CopilotHackathon
También están disponibles en la carpeta 'Formacion/Desafios' de esta documentación.
