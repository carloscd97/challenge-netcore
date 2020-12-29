# Reto Backend C# ASP .NET Core
_Desafío para prueba de Web API ASP .NET Core
### Construir imagen Docker 

_Se deberá compilar Docker y crear nuestra imagen_

```
docker build -t bcp-challenge .
```

### Correr nuestro Docker

_Iniciar nuestro contenedor Docker_

```
docker run -it --rm -p 8080:80 bcp-challenge
```

### Usuario inicio de sesión (POST)

_http://localhost:8080/api/login/authenticate_

```
{
	"Username": "cdiaz",
    "Password": "123"
}
```

### Listado de cambios de monedas (GET)

_http://localhost:8080/api/v1/currency_

```
```

### Hacer cambio de moneda (GET)

_http://localhost:8080/api/v1/currency/change_

```
{
    "Amount": 20,
    "IdOriginCurrency": 1,
    "IdDestinationCurrency": 4
}
```

### Actualizar tipo de cambio (POST)

_http://localhost:8080/api/v1/currency_

```
{
    "IdCurrency": 1,
    "Amount": 3.60
}
```

