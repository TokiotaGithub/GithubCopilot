# GENERACIÓN DE UNA API CON COPILOT
## ÍNDICE
- [Objetivo](#objetivo)
- [Requisitos](#requisitos)
- [Especificaciones iniciales](#especificaciones-iniciales)
- [Pasos](#pasos)
   - [1. Generar la estructura inicial de nuestra aplicación](#1_generar-la-estructura-inicial-de-nuestra-aplicación)
   - [2. Implementación de endpoints](#2_implementación-de-endpoints)
   - [3. Implementación de CervezaService](#3_implementación-de-cervezaservice)
   - [4. Implementacón de BeerRepository](#4_implementacón-de-beerrepository)
   - [5. Completar los modelos](#5_completar-los-modelos)
   - [6. Refactorización de BeerRepository](#6_refactorización-de-beerrepository)
   - [7. Implementación de interfaces](#7_implementación-de-interfaces)
   - [8. Terminar implementación CervezaService](#8_terminar-implementación-cervezaservice)
   - [9. Pruebas y correcciones](#9_pruebas-y-correcciones)
- [Guía Rápida](#guía-rápida-de-pasos)
- [Ejercicios](#ejercicios)
- [Recursos](#recursos)
## OBJETIVO
El objetivo de este ejercicio es generar una API completa en el menor tiempo posible con ayuda de Copilot. El intento de hacerlo en el menor tiempo posible responde a:
- Mostrar la potencia de Copilot y su utilidad para mejorar la productividad.
- Hacer el Workshop lo más ameno posible, se trata de mostrar las ventajas de Copilot más que dar una guía completa sobre su uso (para ello ya está el apartado de 'Casos de Uso').

## REQUISITOS
- Visual Studio 2022 (o superior).
- .Net Core 8 (porque es el que se ha usado en las especificaciones del ejemplo, pero se puede usar cualquier otra versión).
- Licencia para usar Copilot.
- Extensiones de Copilot para VS: GitHub Copilot y GitHub Copilot Chat (aunque para los ejemplos se usarán todas las herrameintas que proporciona Copilot, principalmente usaremos el chat).

## ESPECIFICACIONES INICIALES
La API que elaboremos será pequeña y usará un archivo JSON en lugar de acceder a una base de datos (ver [beers.json](./RecursosWorkshop/beers.json)). Así el ejemplo será sencillo, breve y fácilmente reproducible, quedando abierto a las ampliaciones que desee hacer cada uno por su cuenta.

El modelo de negocio de nuestra API será la gestión de cervezas para un e-commerce. Sólo dispondremos de los métodos necesarios para mostrar las cervezas en nuestro catálogo. Estos métodos son:
- Obtener un listado paginado de cervezas (se especificará desplazamiento y limite).
- Obtener los detalles de una cerveza usando su id.
- Buscar cervezas por nombre, descripción, eslogan, maridaje y/o precio.

En nuestro ejemplo, para darle cierta estructura a la aplicación, implementaremos un servicio, con el que interactuará la API, y un repositorio, que accederá a los datos. Nuestro modelo de dominio será la clase Cerveza.

![](./RecursosWorkshop/DiagramaAPI.png)

## PASOS
AVISO. Es posible que las respuestas que retorna Copilot no se correspondan con las obtenidas durante la elaboración de esta guía, por lo que puede ser necesario que hagamos tareas adicionales o prescindamos de ciertos prompts si queremos desarrollar una API siguiendo estos pasos.

AVISO 2. Cuando se especifique en el prompt el nombre de un archivo precedido por #, esto significa que habrá que teclear # y seleccionar el fichero al que queremos hacer referencia (si hacemos copia-pega del prompt, Copilot no reconocerá ese texto como una referencia).

### 1_Generar la estructura inicial de nuestra aplicación
Según el nivel de detalle que queramos para nuestra estructura inicial, así debermos realizar el prompt. A mayor nivel de detalle, mayor nivel de análisis/diseño que tendremos que realizar previamente, para proporcionarle la mayor cantidad de información posible a Copilot. Si empezamos con poco detalle, tendremos que ir completando el diseño con sucesivas peticiones a Copilot. Ambas aproximaciones son igualmente válidas, aunque ir de menos a más puede garantizar que Copilot acierte mejor con sus respuestas.

Pensamos el diseño que deseamos para nuestra aplicación, en este caso, buscamos que sea bastante estándar y sin complicaciones:
- Presentación: proyecto API que expondrá los métodos para obtener datos
- Dominio: con un proyecto con servicios para manejar la lógica de negocio y otro con la definición de entidades (nuestra única entidad será Cerveza) e interfaces para el acceso a los datos.
- Datos: proyecto Data con acceso a los datos de beer.json.

Empezamos generando una solución con un proyecto .Net Core Web API (BeerShop), dado que Copilot en Visual Studio no puede generar la estructura por sí sólo, sólo te puede dar las instrucciones para hacerlo.

Y generamos el prompt con el que especificar nuestro diseño en el chat de Copilot.

#### Prompt
```
Crea la estructura de un proyecto API de 3 capas. Una para la definición de los métodos de la API, otra donde se defina el dominio (que separará por un lado los servicios y por otro los modelos e interfaces) y una de repositorio. La capa de dominio se centrará en la entidad 'Cerveza'. La capa de API expondrá 3 métodos Get, 2 para obtener un listado de entidades Cervezas y uno para obtener una entidad Cerveza concreta. La capa de repositorio extraerá la información de un archivo beers.json. Existirá una clase Beer en esta capa, que utilizaremos para deserializar beers.json.
```
#### Respuesta
Aquí tienes un esquema básico de cómo podrías estructurar tu aplicación:
1.	BeerShop.Data
Este proyecto se encargará de la capa de repositorio. Aquí es donde leerás el archivo beers.json y deserializarás los datos en objetos Beer.
```cs
public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Add other properties as per your JSON file
}

public class BeerRepository : IBeerRepository
{
    // Implement methods to read from beers.json and return Beer objects
}
```
2.	BeerShop.Domain.Models
Este proyecto contendrá la definición de la clase Cerveza, que será utilizada en toda la aplicación.
```cs
public class Cerveza
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    // Add other properties as per your Beer class
}
```
3.	BeerShop.Domain.Interfaces
Este proyecto contendrá la definición de la interfaz IBeerRepository, que será implementada en BeerShop.Data.
```cs
public interface IBeerRepository
{
    // Define methods that will be implemented in BeerRepository
}
```
4.	BeerShop.Services
Este proyecto contendrá la definición de la clase CervezaService, que se encargará de la lógica de negocio y comunicará la capa de API con la capa de datos.
```cs
public class CervezaService : ICervezaService
{
    private readonly IBeerRepository _beerRepository;

    public CervezaService(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    // Implement methods to call the corresponding methods in BeerRepository
}
```
5.	BeerShop.API
Este proyecto contendrá la definición de la clase CervezaController, que se encargará de manejar las solicitudes HTTP.
```cs
[ApiController]
[Route("[controller]")]
public class CervezaController : ControllerBase
{
    private readonly ICervezaService _cervezaService;

    public CervezaController(ICervezaService cervezaService)
    {
        _cervezaService = cervezaService;
    }

    // Implement API methods to call the corresponding methods in CervezaService
}
```
Por favor, ten en cuenta que este es solo un esquema y necesitarás implementar los detalles de cada método y clase según tus necesidades.
#### Observaciones
La elaboración de un prompt como este puede llevar cierto tiempo y algo de ensayo y error al principio, pero parte de ese tiempo lo invertiríamos igualmente en pensar nuestro diseño y con la práctica los prompts serán cada vez más acertados y fáciles de hacer.
#### Continuación
Creamos los proyectos que nos dice y las clases e interfaces que nos indica. Como Copilot es un 'ayudante', podemos cambiar algunas de sus propuestas, como separar en distintos archivos cada definición de las clases o unificar los dos proyectos de Domain (por comodidad, nuestro proyecto es muy pequeño).
Además, haremos los mínimos cambios necesarios para que nuestro proyecto compile. Como añadir las referencias a proyectos o pedirle a Copilot Chat que genere el archivo ICervezaService que nos falta.
```
Genera el archivo ICervezaService en falta en #Solution
```

### 2_Implementación de endpoints
Una vez tenemos la estructura básica de nuestra aplicación, vamos a dotarla de funcionalidad, empezando por los métodos de nuestra API.
#### Prompt
```
Implementa en #CervezaController.cs los siguientes endpoints:
- Obtener un listado paginado de objetos Cerveza (se pasará por parámetros offset y limit)
- Obtener un objeto Cerveza, pasando su id por parámetros.
- Buscar cervezas, devolverá un listado de objetos Cerveza y tendrá como parámetros: nombre, descripción, eslogan, maridaje y precio.
```
#### Respuesta
Aquí tienes una versión básica de cómo podrías implementar los endpoints en CervezaController.
```cs
[HttpGet]
public IActionResult GetCervezas(int offset, int limit)
{
    var cervezas = _cervezaService.GetCervezas(offset, limit);
    return Ok(cervezas);
}

[HttpGet("{id}")]
public IActionResult GetCerveza(int id)
{
    var cerveza = _cervezaService.GetCerveza(id);
    if (cerveza == null)
    {
        return NotFound();
    }
    return Ok(cerveza);
}

[HttpGet("search")]
public IActionResult SearchCervezas(string nombre, string descripcion, string eslogan, string maridaje, decimal precio)
{
    var cervezas = _cervezaService.SearchCervezas(nombre, descripcion, eslogan, maridaje, precio);
    return Ok(cervezas);
}
```
Por favor, ten en cuenta que necesitarás implementar los métodos GetCervezas, GetCerveza y SearchCervezas en CervezaService y BeerRepository. Estos métodos deben manejar la lógica para obtener y buscar cervezas según los parámetros proporcionados.
#### Observaciones
Como puede verse, Copilot ya da por sentado que utilizaremos CervezaService para recuperar la información, por lo que tendremos que hacer pocos ajustes en el nuevo código.
#### Continuación
Dado que CervezaService no tiene ningún método implementado, le pediremos a Copilot que los implemente.

### 3_Implementación de CervezaService
#### Prompt
```
Continuando con la petición anterior, implementa en #CervezaService los métodos necesarios. También lleva las cabeceras a  #ICervezaService
```
#### Respuesta
Aquí tienes una versión básica de cómo podrías implementar los métodos en CervezaService y ICervezaService.
Primero, actualiza ICervezaService:
```cs
public interface ICervezaService
{
    IEnumerable<Cerveza> GetCervezas(int offset, int limit);
    Cerveza GetCerveza(int id);
    IEnumerable<Cerveza> SearchCervezas(string nombre, string descripcion, string eslogan, string maridaje, decimal precio);
}
```
Luego, implementa estos métodos en CervezaService:
```cs
public class CervezaService : ICervezaService
{
    private readonly IBeerRepository _beerRepository;

    public CervezaService(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public IEnumerable<Cerveza> GetCervezas(int offset, int limit)
    {
        // Call the corresponding method in BeerRepository
    }

    public Cerveza GetCerveza(int id)
    {
        // Call the corresponding method in BeerRepository
    }

    public IEnumerable<Cerveza> SearchCervezas(string nombre, string descripcion, string eslogan, string maridaje, decimal precio)
    {
        // Call the corresponding method in BeerRepository
    }
}
```
Por favor, ten en cuenta que necesitarás implementar los métodos correspondientes en BeerRepository que se llamarán desde estos métodos. Estos métodos deben manejar la lógica para obtener y buscar cervezas según los parámetros proporcionados.

### 4_Implementacón de BeerRepository
Vamos a abordar la recuperación de datos del fichero JSON. Dado que hay que describir con bastante detalle los métodos, iremos uno a uno.
#### 4.1. GetBeer
#### Prompt
```
Implementa en  #BeerRepository  el método GetBeer, que:
- Recibe por parámetros un id para seleccionar un objeto de tipo #Beer
- Busca y deserializa dicho objeto del archivo beer.json
- Este objeto Beer se transformará en un objeto tipo #Cerveza, que será devuelto por el método
```
#### Respuesta
Aquí está la implementación del método GetBeer en BeerRepository.cs. Este método deserializa el archivo beers.json en una lista de objetos Beer, selecciona el objeto Beer con el ID proporcionado, lo convierte en un objeto Cerveza y lo devuelve.
```cs
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using BeerShop.Domain.Models;

public class BeerRepository : IBeerRepository
{
    public Cerveza GetBeer(int id)
    {
        var json = File.ReadAllText("beers.json");
        var beers = JsonConvert.DeserializeObject<List<Beer>>(json);
        var beer = beers.FirstOrDefault(b => b.Id == id);

        if (beer != null)
        {
            return new Cerveza
            {
                Id = beer.Id,
                Nombre = beer.Name
                // Add other properties as per your Beer class
            };
        }

        return null;
    }
}
```
Por favor, asegúrate de que el archivo beers.json esté en la misma ubicación que el ejecutable de tu aplicación o ajusta la ruta del archivo según sea necesario. Además, este código asume que tienes una interfaz IBeerRepository que este repositorio está implementando. Si no es así, puedes eliminar esa parte del código.
#### Observaciones
La respuesta dada por Copilot, aunque es válida, tendrá que ser refinada más adelante, dado que tenemos que terminar de definir los modelos Beer y Cerveza y hay código que seguramente se repita en los demás métodos. Abordaremos esto en pasos posteriores.
#### 4.2. GetBeers
#### Prompt
```
Implementa en #BerrRepository el método GetBeers, que:
- Recibe por parámetros un offset y un limit para obtener una lista paginada de objetos tipo #Beer  
- Busca y deserializa dicha lista del archivo beer.json
- El listado de objetos Beer se transformará en una lista de objetos tipo #Cerveza, que será devuelto por el método
```
#### Respuesta
Aquí está la implementación del método GetBeers en BeerRepository.cs. Este método deserializa el archivo beers.json en una lista de objetos Beer, selecciona un rango de objetos Beer basado en los parámetros offset y limit, los convierte en objetos Cerveza y devuelve la lista.
```cs
public List<Cerveza> GetBeers(int offset, int limit)
{
    var json = File.ReadAllText("beers.json");
    var beers = JsonConvert.DeserializeObject<List<Beer>>(json);
    var selectedBeers = beers.Skip(offset).Take(limit);

    var cervezas = selectedBeers.Select(beer => new Cerveza
    {
        Id = beer.Id,
        Nombre = beer.Name
        // Add other properties as per your Beer class
    }).ToList();

    return cervezas;
}
```
Por favor, asegúrate de que el archivo beers.json esté en la misma ubicación que el ejecutable de tu aplicación o ajusta la ruta del archivo según sea necesario. Además, este código asume que tienes una interfaz IBeerRepository que este repositorio está implementando. Si no es así, puedes eliminar esa parte del código.
#### 4.3. SearchBeers
#### Prompt
```
Implementa en #BeerRepository el método SearchBeers, que:
- Recibe por parámetros: string nombre, string descripcion, string eslogan, string maridaje, decimal precio
- Busca un listado de objetos tipo #Beer en el archivo beers.json, obteniendo todos aquellos Beer que tengan el mismo nombre, descripción, eslogan, maridaje y precio que hemos pasado por parámetros (si el parámetro tiene valor nulo, no se filtra por él)
- El listado de objetos Beer se transformará en una lista de objetos tipo #Cerveza, que será devuelto por el método
```
#### Respuesta
Aquí está la implementación del método SearchBeers en BeerRepository.cs. Este método deserializa el archivo beers.json en una lista de objetos Beer, filtra los objetos Beer basado en los parámetros proporcionados (si no son nulos), los convierte en objetos Cerveza y devuelve la lista.
```cs
public List<Cerveza> SearchBeers(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio)
{
    var json = File.ReadAllText("beers.json");
    var beers = JsonConvert.DeserializeObject<List<Beer>>(json);

    if (!string.IsNullOrEmpty(nombre))
    {
        beers = beers.Where(b => b.Name == nombre).ToList();
    }

    // Add similar checks for descripcion, eslogan, maridaje, and precio
    // Make sure to update your Beer class to include these properties

    var cervezas = beers.Select(beer => new Cerveza
    {
        Id = beer.Id,
        Nombre = beer.Name
        // Add other properties as per your Beer class
    }).ToList();

    return cervezas;
}
```
Por favor, asegúrate de que el archivo beers.json esté en la misma ubicación que el ejecutable de tu aplicación o ajusta la ruta del archivo según sea necesario. Además, este código asume que tienes una interfaz IBeerRepository que este repositorio está implementando. Si no es así, puedes eliminar esa parte del código.
#### Observaciones
Copilot no termina de implementar el filtrado completo de nuestra búsqueda, así que pediremos que genere el mismo filtro para el resto de parámetros (más adelante podremos mejorar el código, por ahora dejémoslo simplemente funcional).

Prompt
```
En el método seleccionado hay un filtrado usando el parámetro nombre. Replica ese filtrado para que también se haga para descripción, eslogan, maridaje y precio.
```
Respuesta
Aquí está la implementación del método SearchBeers con los filtros adicionales para descripcion, eslogan, maridaje y precio.
```cs
public List<Cerveza> SearchBeers(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio)
{
    var json = File.ReadAllText("beers.json");
    var beers = JsonConvert.DeserializeObject<List<Beer>>(json);

    if (!string.IsNullOrEmpty(nombre))
    {
        beers = beers.Where(b => b.Name == nombre).ToList();
    }

    if (!string.IsNullOrEmpty(descripcion))
    {
        beers = beers.Where(b => b.Description == descripcion).ToList();
    }

    if (!string.IsNullOrEmpty(eslogan))
    {
        beers = beers.Where(b => b.Slogan == eslogan).ToList();
    }

    if (!string.IsNullOrEmpty(maridaje))
    {
        beers = beers.Where(b => b.Pairing == maridaje).ToList();
    }

    if (precio.HasValue)
    {
        beers = beers.Where(b => b.Price == precio.Value).ToList();
    }

    var cervezas = beers.Select(beer => new Cerveza
    {
        Id = beer.Id,
        Nombre = beer.Name
        // Add other properties as per your Beer class
    }).ToList();

    return cervezas;
}
```
Por favor, asegúrate de que los nombres de las propiedades en la clase Beer coinciden con los nombres utilizados en los filtros (Description, Slogan, Pairing, Price). Si no es así, debes ajustar los nombres de las propiedades en los filtros para que coincidan con los de tu clase Beer.
### Continuación
Nuestro repositorio tiene una implementación inicial pero no definitiva, necesita definir los modelos y alguna refactorización para darlo por finalizado. Se verá a continuación.

### 5_Completar los modelos
Para poder recuperar datos y transformarlos como deseemos, necesitamos implemmentar correctamente los modelos con los que estamos trabajando. Recordemos que tenemos un modelo Beer, que se correspondería con el tipo de datos que podemos recuperar del fichero JSON y está en el proyecto de Data, y un modelo Cerveza, que es el modelo de nuestro dominio y está en dicho proyecto. La elección de 2 modelos tan similares (uno traduce a otro) responde simplemente a motivos didácticos.
#### 5.1. Beer
Dado que la clase Beer se usa para deserializar los objetos de nuestro json, haremos que Copilot genere todas sus propiedades a partir del contenido de dicho archivo (debemos incluirlo en el proyecto de Data).
#### Prompt
```
Implementa todas las propiedades necesarias en #Beer.cs para poder deserializar los elementos del archivo #beers.json (marca como nullable aquellas propiedades que puedan tener valor null)
```
#### Respuesta
Aquí está la implementación de la clase Beer con todas las propiedades necesarias para deserializar los elementos del archivo beers.json. He marcado las propiedades que pueden tener valor null como nullable.
```cs
public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tagline { get; set; }
    public string First_brewed { get; set; }
    public string Description { get; set; }
    public string Image_url { get; set; }
    public decimal? Price { get; set; }
    public decimal? Abv { get; set; }
    public decimal? Ibu { get; set; }
    public decimal? Target_fg { get; set; }
    public decimal? Target_og { get; set; }
    public decimal? Ebc { get; set; }
    public decimal? Srm { get; set; }
    public decimal? Ph { get; set; }
    public decimal? Attenuation_level { get; set; }
    public Volume Volume { get; set; }
    public Volume Boil_volume { get; set; }
    public List<string> Food_pairing { get; set; }
    public string Brewers_tips { get; set; }
    public string Contributed_by { get; set; }
}

public class Volume
{
    public decimal? Value { get; set; }
    public string Unit { get; set; }
}
```
Por favor, asegúrate de que los nombres de las propiedades en la clase Beer coinciden con los nombres utilizados en el archivo beers.json. Si no es así, debes ajustar los nombres de las propiedades en la clase Beer para que coincidan con los de tu archivo beers.json.
#### 5.2. Cerveza
El modelo Cerveza es nuestro modelo de dominio, básicamente es el modelo Beer traducido, y así le pediremos a Copilot que lo construya.
#### Prompt
```
Genera en #Cerveza.cs las mismas propiedades que en #Beer.cs  ,  pero traducidas al español y teniendo en cuenta que están relacionadas con la cerveza (traducir las siglas para que tengan sentido). Las propiedades deben tener un comentario al lado con el nombre de la propiedad de Beer con la que se corresponden, basta con que sólo salga el nombre)
```
#### Respuesta
Aquí está la implementación de la clase Cerveza con todas las propiedades necesarias traducidas al español y relacionadas con la cerveza. Cada propiedad tiene un comentario con el nombre de la propiedad correspondiente en la clase Beer.
```cs
public class Cerveza
{
    public int Id { get; set; } // Id
    public string Nombre { get; set; } // Name
    public string Lema { get; set; } // Tagline
    public string PrimeraProduccion { get; set; } // First_brewed
    public string Descripcion { get; set; } // Description
    public string UrlImagen { get; set; } // Image_url
    public decimal? Precio { get; set; } // Price
    public decimal? AlcoholPorVolumen { get; set; } // Abv
    public decimal? UnidadesInternacionalesAmargor { get; set; } // Ibu
    public decimal? GravedadFinalObjetivo { get; set; } // Target_fg
    public decimal? GravedadOriginalObjetivo { get; set; } // Target_og
    public decimal? ColorCervezaEuropeo { get; set; } // Ebc
    public decimal? MetodoReferenciaColor { get; set; } // Srm
    public decimal? Ph { get; set; } // Ph
    public decimal? NivelAtenuacion { get; set; } // Attenuation_level
    public Volumen Volumen { get; set; } // Volume
    public Volumen VolumenHervido { get; set; } // Boil_volume
    public List<string> Maridaje { get; set; } // Food_pairing
    public string ConsejosCerveceros { get; set; } // Brewers_tips
    public string ContribuidoPor { get; set; } // Contributed_by
}

public class Volumen
{
    public decimal? Valor { get; set; } // Value
    public string Unidad { get; set; } // Unit
}
```
Por favor, asegúrate de que los nombres de las propiedades en la clase Cerveza coinciden con los nombres utilizados en la clase Beer. Si no es así, debes ajustar los nombres de las propiedades en la clase Cerveza para que coincidan con los de tu clase Beer.

### 6_Refactorización de BeerRepository
Dado que Copilot no es infalibe y hay cambios a los que hay que adaptarse posteriormente, vamos a refactorizar el código de BeerRepository:
- Adaptaremos la transformación de Beer a Cerveza, incluyendo las nuevas propiedades.
- Pediremos una refactorización 'general' del archivo, esperando que Copilot elimine redundandicas de código.
- Si vemos que necesitamos más mejoras, se las pediremos a Copilot.
#### 6.1. Transformación de Beer a Cerveza
#### Prompt
```
Refactoriza #BeerRepository.cs de forma que la conversión de Beer a Cerveza se haga con todas las propiedades de Beer (ten en cuenta que las propiedades de Cerveza tienen comentada la propiedad de Beer con la que se correponden)
```
#### Respuesta
Aquí está la refactorización de BeerRepository.cs. He extraído la lógica de conversión de Beer a Cerveza a un método privado para evitar la repetición de código. Además, he actualizado los métodos para que utilicen este nuevo método.
```
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using BeerShop.Domain;

public class BeerRepository : IBeerRepository
{
    private Cerveza ConvertToCerveza(Beer beer)
    {
        return new Cerveza
        {
            Id = beer.Id, // Id
            Nombre = beer.Name, // Name
            Lema = beer.Tagline, // Tagline
            PrimeraProduccion = beer.First_brewed, // First_brewed
            Descripcion = beer.Description, // Description
            UrlImagen = beer.Image_url, // Image_url
            Precio = beer.Price, // Price
            AlcoholPorVolumen = beer.Abv, // Abv
            UnidadesInternacionalesAmargor = beer.Ibu, // Ibu
            GravedadFinalObjetivo = beer.Target_fg, // Target_fg
            GravedadOriginalObjetivo = beer.Target_og, // Target_og
            ColorCervezaEuropeo = beer.Ebc, // Ebc
            MetodoReferenciaColor = beer.Srm, // Srm
            Ph = beer.Ph, // Ph
            NivelAtenuacion = beer.Attenuation_level, // Attenuation_level
            Volumen = new Volumen { Valor = beer.Volume.Value, Unidad = beer.Volume.Unit }, // Volume
            VolumenHervido = new Volumen { Valor = beer.Boil_volume.Value, Unidad = beer.Boil_volume.Unit }, // Boil_volume
            Maridaje = beer.Food_pairing, // Food_pairing
            ConsejosCerveceros = beer.Brewers_tips, // Brewers_tips
            ContribuidoPor = beer.Contributed_by // Contributed_by
        };
    }

    public Cerveza GetBeer(int id)
    {
        var json = File.ReadAllText("beers.json");
        var beers = JsonConvert.DeserializeObject<List<Beer>>(json);
        var beer = beers.FirstOrDefault(b => b.Id == id);

        return beer != null ? ConvertToCerveza(beer) : null;
    }

    public List<Cerveza> GetBeers(int offset, int limit)
    {
        var json = File.ReadAllText("beers.json");
        var beers = JsonConvert.DeserializeObject<List<Beer>>(json);
        var selectedBeers = beers.Skip(offset).Take(limit);

        return selectedBeers.Select(ConvertToCerveza).ToList();
    }

    public List<Cerveza> SearchBeers(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio)
    {
        var json = File.ReadAllText("beers.json");
        var beers = JsonConvert.DeserializeObject<List<Beer>>(json);

        if (!string.IsNullOrEmpty(nombre))
        {
            beers = beers.Where(b => b.Name == nombre).ToList();
        }

        if (!string.IsNullOrEmpty(descripcion))
        {
            beers = beers.Where(b => b.Description == descripcion).ToList();
        }

        if (!string.IsNullOrEmpty(eslogan))
        {
            beers = beers.Where(b => b.Slogan == eslogan).ToList();
        }

        if (!string.IsNullOrEmpty(maridaje))
        {
            beers = beers.Where(b => b.Pairing == maridaje).ToList();
        }

        if (precio.HasValue)
        {
            beers = beers.Where(b => b.Price == precio.Value).ToList();
        }

        return beers.Select(ConvertToCerveza).ToList();
    }
}
```
Por favor, asegúrate de que el archivo beers.json esté en la misma ubicación que el ejecutable de tu aplicación o ajusta la ruta del archivo según sea necesario. Además, este código asume que tienes una interfaz IBeerRepository que este repositorio está implementando. Si no es así, puedes eliminar esa parte del código.

#### 6.2. Refactorización 'general'
#### Prompt
```
/optimize  #BeerRepository.cs
```
#### Respuesta
Here's an optimized version of BeerRepository.cs. The main optimization is to read and deserialize the JSON file only once when the BeerRepository is constructed, rather than every time a method is called. This will significantly improve performance if the methods are called multiple times.
```cs
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using BeerShop.Domain;

public class BeerRepository : IBeerRepository
{
    private readonly List<Beer> beers;

    public BeerRepository()
    {
        var json = File.ReadAllText("beers.json");
        beers = JsonConvert.DeserializeObject<List<Beer>>(json);
    }

    private Cerveza ConvertToCerveza(Beer beer)
    {
        return new Cerveza
        {
            Id = beer.Id, // Id
            Nombre = beer.Name, // Name
            Lema = beer.Tagline, // Tagline
            PrimeraProduccion = beer.First_brewed, // First_brewed
            Descripcion = beer.Description, // Description
            UrlImagen = beer.Image_url, // Image_url
            Precio = beer.Price, // Price
            AlcoholPorVolumen = beer.Abv, // Abv
            UnidadesInternacionalesAmargor = beer.Ibu, // Ibu
            GravedadFinalObjetivo = beer.Target_fg, // Target_fg
            GravedadOriginalObjetivo = beer.Target_og, // Target_og
            ColorCervezaEuropeo = beer.Ebc, // Ebc
            MetodoReferenciaColor = beer.Srm, // Srm
            Ph = beer.Ph, // Ph
            NivelAtenuacion = beer.Attenuation_level, // Attenuation_level
            Volumen = new Volumen { Valor = beer.Volume.Value, Unidad = beer.Volume.Unit }, // Volume
            VolumenHervido = new Volumen { Valor = beer.Boil_volume.Value, Unidad = beer.Boil_volume.Unit }, // Boil_volume
            Maridaje = beer.Food_pairing, // Food_pairing
            ConsejosCerveceros = beer.Brewers_tips, // Brewers_tips
            ContribuidoPor = beer.Contributed_by // Contributed_by
        };
    }

    public Cerveza GetBeer(int id)
    {
        var beer = beers.FirstOrDefault(b => b.Id == id);
        return beer != null ? ConvertToCerveza(beer) : null;
    }

    public List<Cerveza> GetBeers(int offset, int limit)
    {
        var selectedBeers = beers.Skip(offset).Take(limit);
        return selectedBeers.Select(ConvertToCerveza).ToList();
    }

    public List<Cerveza> SearchBeers(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio)
    {
        var filteredBeers = beers.AsEnumerable();

        if (!string.IsNullOrEmpty(nombre))
        {
            filteredBeers = filteredBeers.Where(b => b.Name == nombre);
        }

        if (!string.IsNullOrEmpty(descripcion))
        {
            filteredBeers = filteredBeers.Where(b => b.Description == descripcion);
        }

        if (!string.IsNullOrEmpty(eslogan))
        {
            filteredBeers = filteredBeers.Where(b => b.Slogan == eslogan);
        }

        if (!string.IsNullOrEmpty(maridaje))
        {
            filteredBeers = filteredBeers.Where(b => b.Pairing == maridaje);
        }

        if (precio.HasValue)
        {
            filteredBeers = filteredBeers.Where(b => b.Price == precio.Value);
        }

        return filteredBeers.Select(ConvertToCerveza).ToList();
    }
}
```
Please make sure that the beers.json file is in the same location as your application's executable or adjust the file path as necessary. Also, this code assumes that you have an IBeerRepository interface that this repository is implementing. If not, you can remove that part of the code.
#### Observaciones
Al utilizar un prompt predefinido, la respuesta es en inglés. Como se puede ver, Copilot ha sacado la lectura del archivo de los distintos métodos, centralizándolo en el constructor y reduciendo el código redundante y el número de lecturas al fichero.
### 6.3. Refactorizaciones concretas
Vamos a pedirle a Copilot cambios concretos que creemos que podrían mejorar el código pero que no se han aplicado durante la refactorización:
- Simplificar el filtrado en SearchBeers.
- Permitir que el conversor de Beer a Cerveza devuelva null.
### Prompt
```
En #BeerRepository, ¿podrías simplificar el filtrado unificando los filtros en un único Where en el método SearchBeers?, ¿podrías permitir que ConevertToCerveza retorne null si llega un null por parámetros?
```
### Respuesta
Por supuesto, podemos simplificar aún más el filtrado en el método SearchBeers unificando todos los filtros en una única cláusula Where. Además, podemos modificar el método ConvertToCerveza para que devuelva null si se pasa un null.
Aquí está el código refactorizado:
```cs
public class BeerRepository : IBeerRepository
{
    // ... other code ...

    private Cerveza ConvertToCerveza(Beer beer)
    {
        if (beer == null)
        {
            return null;
        }

        return new Cerveza
        {
            // ... property assignments ...
        };
    }

    public List<Cerveza> SearchBeers(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio)
    {
        return beers
            .Where(b => 
                (string.IsNullOrEmpty(nombre) || b.Name == nombre) &&
                (string.IsNullOrEmpty(descripcion) || b.Description == descripcion) &&
                (string.IsNullOrEmpty(eslogan) || b.Slogan == eslogan) &&
                (string.IsNullOrEmpty(maridaje) || b.Pairing == maridaje) &&
                (!precio.HasValue || b.Price == precio.Value))
            .Select(ConvertToCerveza)
            .ToList();
    }
}
```
Con estos cambios, el método SearchBeers se ha simplificado aún más y ahora el método ConvertToCerveza puede manejar correctamente los valores null.
#### Observaciones
Como se puede ver en la respuesta, tendremos que hacer algunas adaptaciones a mano, como eliminar la comprobación de valor nulo al ir a convertir un objeto Beer en Cerveza y algunos ajustes con los nombres de las propiedades de Beer en el filtrado. A pesar de esto, el 90% aprox. del código que tenemos ha sido generado automáticamtne por Copilot.
### 7_Implementación de interfaces
Definimos las interfaces pendientes de nuestros servicios y repositorios (hasta el momento sólo nos falta por definir la interfaz IBeerRepository).
#### Prompt
```
Define la interfaz de #IBeerRepository a partir de los métodos implementados en #BeerRepository
```
#### Respuesta
Aquí está la definición de la interfaz IBeerRepository basada en los métodos implementados en BeerRepository:
```cs
public interface IBeerRepository
{
    Cerveza GetBeer(int id);
    List<Cerveza> GetBeers(int offset, int limit);
    List<Cerveza> SearchBeers(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio);
}
```
Esta interfaz define los métodos GetBeer, GetBeers y SearchBeers que están implementados en BeerRepository.
### 8_Terminar implementación CervezaService
En la implementación que hicimos de CervezaService en el punto 3, no se llegó a incluir código en los métodos al no tener implementado el repositorio. Le pediremos a Copilot que la termine ahora.
#### Prompt
```
Incluye en los métodos implementados en #CervezaService.cs las llamadas que necesiten a los métodos de #BeerRepository para recuperar datos
```
#### Respuesta
Aquí está la implementación de los métodos en CervezaService que llaman a los métodos correspondientes en BeerRepository para recuperar los datos:
```cs
public class CervezaService : ICervezaService
{
    private readonly IBeerRepository _beerRepository;

    public CervezaService(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public IEnumerable<Cerveza> GetCervezas(int offset, int limit)
    {
        return _beerRepository.GetBeers(offset, limit);
    }

    public Cerveza GetCerveza(int id)
    {
        return _beerRepository.GetBeer(id);
    }

    public IEnumerable<Cerveza> SearchCervezas(string nombre, string descripcion, string eslogan, string maridaje, decimal precio)
    {
        return _beerRepository.SearchBeers(nombre, descripcion, eslogan, maridaje, precio);
    }
}
```

```
En este ejemplo, se está permitiendo el acceso desde http://example.com. Puedes cambiar esto a cualquier origen que necesites. También se están permitiendo todos los métodos y encabezados HTTP.
Recuerda que debes llamar a UseCors() después de UseRouting() y antes de UseEndpoints().
```

Estos métodos simplemente delegan la llamada a los métodos correspondientes en BeerRepository.
### 9_Pruebas y correcciones
Con todos los pasos que llevamos hasta ahora, nuestra API ya estaría implementada y compilaría. Pero como en cualquier desarrollo que hagamos, queda hacer pruebas y revisar nuestro código para ver si nos hemos pasado algo por alto. Repasemos:
#### 9.1. Falta declarar las dependencias
Al probar nuestra aplicación, obtenermos el siguiente error:
```cs
System.InvalidOperationException: Unable to resolve service for type 'ICervezaService' while attempting to activate 'CervezaController'.
   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.ThrowHelperUnableToResolveService(Type type, Type requiredBy)
   at lambda_method3(Closure, IServiceProvider, Object[])
   at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass6_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Logged|17_1(ResourceInvoker invoker)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Logged|17_1(ResourceInvoker invoker)
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
```
Nos falta declarar todas las dependencias de nuestra aplicación (es tan pequeña la aplicación que podemos implementarlo directamente nosotros, pero seguimos con Copilot hasta el final).
#### Prompt
```
Necesito registrar los servicios y repositorios definidos en #BeerShop.Services.csproj y #BeerShop.Data.csproj en #Program.cs
```
#### Respuesta
Para registrar los servicios y repositorios en Program.cs, necesitas agregar una línea en la sección de servicios para cada dependencia que quieras inyectar. Aquí está cómo puedes hacerlo:
```cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register BeerRepository and CervezaService as singletons
builder.Services.AddSingleton<IBeerRepository, BeerRepository>();
builder.Services.AddSingleton<ICervezaService, CervezaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```
En este código, AddSingleton<IBeerRepository, BeerRepository>() y AddSingleton<ICervezaService, CervezaService>() registran BeerRepository y CervezaService como singletons en el contenedor de inyección de dependencias. Esto significa que se creará una única instancia de cada uno que se reutilizará cada vez que se solicite IBeerRepository o ICervezaService.
Por favor, asegúrate de que tienes las interfaces IBeerRepository y ICervezaService que BeerRepository y CervezaService están implementando. Si no es así, puedes eliminar la parte de la interfaz y registrar directamente las clases BeerRepository y CervezaService.
Además, necesitarás agregar las referencias de proyecto correspondientes a BeerShop.Data y BeerShop.Services en tu proyecto BeerShop.API. Puedes hacerlo agregando las siguientes líneas a tu archivo BeerShop.API.csproj:
```cs
<ItemGroup>
  <ProjectReference Include="..\BeerShop.Data\BeerShop.Data.csproj" />
  <ProjectReference Include="..\BeerShop.Services\BeerShop.Services.csproj" />
</ItemGroup>
```
Esto permitirá que tu proyecto BeerShop.API tenga acceso a las clases definidas en BeerShop.Data y BeerShop.Services.
#### 9.2. No encuentra el archivo beers.json
El siguiente error que tenemos es que no se localiza el archivo beers.json (hasta ahora lo único que hemos hecho es incluirlo en el proyecto).
```cs
System.IO.FileNotFoundException: 'Could not find file 'C:\Repos\Tokiota\PruebasCopilotUseCases\EjemploWorkshop_API\BeerShop\BeerShop.API\beers.json'.'
```
#### Prompt
```
En #BeerRepository se accede al archivo beers.json, pero en tiempo de ejecución no se localiza, ¿cómo puedo solucionarlo?
```
#### Respuesta
El problema puede ser debido a la ruta del archivo beers.json. Cuando se especifica solo el nombre del archivo, el programa buscará el archivo en el directorio de trabajo actual, que puede no ser el mismo que el directorio donde se encuentra el archivo.
Una solución común es colocar el archivo beers.json en el directorio de la aplicación y luego usar AppDomain.CurrentDomain.BaseDirectory para obtener la ruta del directorio de la aplicación:
```cs
public BeerRepository()
{
    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "beers.json");
    var json = File.ReadAllText(path);
    beers = JsonConvert.DeserializeObject<List<Beer>>(json);
}
```
Otra opción es colocar el archivo beers.json en la carpeta de recursos del proyecto y luego configurar las propiedades del archivo para que se copie en el directorio de salida. Para hacer esto, sigue estos pasos:
1.	Haz clic con el botón derecho en el archivo beers.json en el Explorador de soluciones.
2.	Haz clic en Propiedades.
3.	En la ventana de propiedades, establece la propiedad "Copiar en el directorio de salida" en "Copiar siempre" o "Copiar si es más reciente".
Esto asegurará que beers.json siempre esté en el mismo directorio que el ejecutable de la aplicación.
#### Observaciones
Para la segunda opción que nos da Copilot también necesitamos modificar el código tal y como indica en la primera opción. La solución parece correcta pero no está bien expresada del todo.
#### 9.3. Todos los parámetros de SearchCerveza son requeridos
Los parámetros de búsqueda deberían ser opcionales. Este error es demasiado trivial para gestionarlo con Copilot. Basta con marcarlos como nullable en la API.

#### 9.4. Falta de configuración de los CORS
#### Prompt
```
Incluye la configuración de CORS en esta solución
```
#### Respuesta
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


## CONSIDERACIONES FINALES
Como comentábamos al principio, podríamos haberle sacado más 'jugo' a Copilot incluyendo documentación, proyectos de tests, etc. sin embargo nuestro objetivo era demostrar cómo de útil puede ser Copilot a la hora de mejorar nuestra productividad, desarrollando para ello un ejemplo rápido y sencillo. Para más información sobre todas las opciones que ofrece Copilot, revisar los casos de uso de la documentación en la que se incluye este ejemplo.

## GUÍA RÁPIDA DE PASOS
AVISO. Es posible que las respuestas que retorna Copilot no se correspondan con las obtenidas durante la elaboración de esta guía, por lo que puede ser necesario que hagamos tareas adicionales o prescindamos de ciertos prompts. Los prompts que se muestran a continuación son una mera guía.

AVISO 2. Cuando se especifique en el prompt el nombre de un archivo precedido por #, esto significa que habrá que teclear # y seleccionar el fichero al que queremos hacer referencia (si hacemos copia-pega del prompt, Copilot no reconocerá ese texto como una referencia).

### 1. Generar la estructura inicial
```
Crea la estructura de un proyecto API de 3 capas. Una para la definición de los métodos de la API, otra donde se defina el dominio (que separará por un lado los servicios y por otro los modelos e interfaces) y una de repositorio. La capa de dominio se centrará en la entidad 'Cerveza'. La capa de API expondrá 3 métodos Get, 2 para obtener un listado de entidades Cervezas y uno para obtener una entidad Cerveza concreta. La capa de repositorio extraerá la información de un archivo beers.json. Existirá una clase Beer en esta capa, que utilizaremos para deserializar beers.json.
```
### 2. Implementación de endpoints
```
Implementa en #CervezaController.cs los siguientes endpoints:
- Obtener un listado paginado de objetos Cerveza (se pasará por parámetros offset y limit)
- Obtener un objeto Cerveza, pasando su id por parámetros.
- Buscar cervezas, devolverá un listado de objetos Cerveza y tendrá como parámetros: nombre, descripción, eslogan, maridaje y precio.
```
### 3. Implementación de CervezaService
```
Continuando con la petición anterior, implementa en #CervezaService los métodos necesarios. También lleva las cabeceras a  #ICervezaService
```
### 4. Implementacón de BeerRepository
```
Implementa en  #BeerRepository  el método GetBeer, que:
- Recibe por parámetros un id para seleccionar un objeto de tipo #Beer
- Busca y deserializa dicho objeto del archivo beer.json
- Este objeto Beer se transformará en un objeto tipo #Cerveza, que será devuelto por el método
```
```
Implementa en #BeerRepository el método GetBeers, que:
- Recibe por parámetros un offset y un limit para obtener una lista paginada de objetos tipo #Beer  
- Busca y deserializa dicha lista del archivo beer.json
- El listado de objetos Beer se transformará en una lista de objetos tipo #Cerveza, que será devuelto por el método
```
```
Implementa en #BeerRepository el método SearchBeers, que:
- Recibe por parámetros: string nombre, string descripcion, string eslogan, string maridaje, decimal precio
- Busca un listado de objetos tipo #Beer en el archivo beers.json, obteniendo todos aquellos Beer que tengan el mismo nombre, descripción, eslogan, maridaje y precio que hemos pasado por parámetros (si el parámetro tiene valor nulo, no se filtra por él)
- El listado de objetos Beer se transformará en una lista de objetos tipo #Cerveza, que será devuelto por el método
```
```
En el método seleccionado hay un filtrado usando el parámetro nombre. Replica ese filtrado para que también se haga para descripción, eslogan, maridaje y precio.
```
### 5. Completar los modelos
```
Implementa todas las propiedades necesarias en #Beer.cs para poder deserializar los elementos del archivo #beers.json (marca como nullable aquellas propiedades que puedan tener valor null)
```
```
Genera en #Cerveza.cs las mismas propiedades que en #Beer.cs  ,  pero traducidas al español y teniendo en cuenta que están relacionadas con la cerveza (traducir las siglas para que tengan sentido). Las propiedades deben tener un comentario al lado con el nombre de la propiedad de Beer con la que se corresponden, basta con que sólo salga el nombre)
```
### 6. Refactorización de BeerRepository
```
Refactoriza #BeerRepository.cs de forma que la conversión de Beer a Cerveza se haga con todas las propiedades de Beer (ten en cuenta que las propiedades de Cerveza tienen comentada la propiedad de Beer con la que se correponden)
```
```
/optimize  #BeerRepository.cs
```
```
En #BeerRepository, ¿podrías simplificar el filtrado unificando los filtros en un único Where en el método SearchBeers?, ¿podrías permitir que ConevertToCerveza retorne null si llega un null por parámetros?
```
### 7. Implementación de interfaces
```
Define la interfaz de #IBeerRepository a partir de los métodos implementados en #BeerRepository
```
### 8. Terminar implementación CervezaService
```
Incluye en los métodos implementados en #CervezaService.cs las llamadas que necesiten a los métodos de #BeerRepository para recuperar datos
```
### 9. Pruebas y correcciones
```
Necesito registrar los servicios y repositorios definidos en #BeerShop.Services.csproj y #BeerShop.Data.csproj en #Program.cs
```
```
En #BeerRepository se accede al archivo beers.json, pero en tiempo de ejecución no se localiza, ¿cómo puedo solucionarlo?
```
Todos los parámetros de SearchCerveza son requeridos: se pueden hacer opcionales a mano, el proyecto no es tan grande como para necesitar usar Copilot.
## EJERCICIOS
Para afianzar conocimientos, puedes intentar ampliar la API con la ayuda de Copilot:
- Añade métodos nuevos para añadir, actualizar y eliminar cervezas.
- Documenta la API.
- Crea pruebas unitarias.
- Añade Automapper para transformar Beer en Cerveza.
- Diseña un modelo de EF Core con su repository para trabajar contra una BD.
- Añade datos de usuario y autenticación.

## RECURSOS
Este ejemplo se ha tomado de uno de los 'challenges' que utiliza Microsoft en sus 'hackatons'. Para más información, visitar: https://github.com/microsoft/CopilotHackathon
También están disponibles en la carpeta 'Formacion/Desafios' de esta documentación.
