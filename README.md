# Redarbor Backend Technical Test

REST API para gestión de empleados construida con .NET 8, siguiendo principios de DDD, CQRS, SOLID y Clean Code.

---

## Tecnologías

- **ASP.NET Core 8** — Framework principal
- **Entity Framework Core** — Escrituras (Commands)
- **Dapper** — Lecturas (Queries)
- **MediatR** — Implementación del patrón CQRS
- **FluentValidation** — Validaciones de dominio
- **MySQL 8** — Base de datos
- **Docker + Docker Compose** — Containerización completa
- **xUnit + Moq** — Tests unitarios

---

## Arquitectura

El proyecto sigue una arquitectura en capas basada en DDD y CQRS:

```
Redarbor/
├── Redarbor.API/                  # Capa de presentación (Controllers, Middleware)
├── Redarbor.Application/          # Casos de uso (Commands, Queries, Handlers, DTOs)
├── Redarbor.Domain/               # Entidades, Value Objects, Interfaces del dominio
├── Redarbor.Infrastructure/       # EF Core (writes), Dapper (reads), Repositories
├── Redarbor.Tests/                # Tests unitarios
├── docker-compose.yml
├── Dockerfile
└── init.sql                       # Script de creación de la tabla
```

### Patrón CQRS

- **Commands** (escritura vía EF Core): `CreateEmployeeCommand`, `UpdateEmployeeCommand`, `DeleteEmployeeCommand`
- **Queries** (lectura vía Dapper): `GetAllEmployeesQuery`, `GetEmployeeByIdQuery`
- Cada command/query tiene su propio Handler registrado en MediatR.

---




## Endpoints de la API

[Redarbor_final.json](https://github.com/user-attachments/files/25578562/Redarbor_final.json)


 <img width="1428" height="896" alt="Screenshot 2026-02-26 at 9 35 47 AM" src="https://github.com/user-attachments/assets/ba04dac2-b1fb-4a5d-bd73-7c43da84821e" />


---

##  Ejecución con Docker (recomendado)

### Requisitos
- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/)

### Levantar todo el entorno

```bash
docker-compose up --build
```

Esto levanta automáticamente:
- **MySQL 8** en el puerto `3306` con la base de datos y tabla `Employee` ya creadas
- **API** en `http://localhost:8080`
- **Swagger UI** en `http://localhost:8080/swagger`

No necesitas instalar .NET ni MySQL en tu máquina. Todo corre dentro de Docker.

### Parar y limpiar

```bash
# Parar los contenedores
docker-compose down

# Parar y eliminar también los volúmenes (resetea la BD)
docker-compose down -v
```

---

## Ejemplos de uso con curl

### 1. Insertar un empleado

```bash
curl -i -XPOST -H "Content-Type: application/json" \
  -d '{
    "CompanyId": 1,
    "CreatedOn": "2000-01-01 00:00:00",
    "DeletedOn": "2000-01-01 00:00:00",
    "Email": "test1@test.test.tmp",
    "Fax": "000.000.000",
    "Name": "test1",
    "Lastlogin": "2000-01-01 00:00:00",
    "Password": "test",
    "PortalId": 1,
    "RoleId": 1,
    "StatusId": 1,
    "Telephone": "000.000.000",
    "UpdatedOn": "2000-01-01 00:00:00",
    "Username": "test1"
  }' \
  http://localhost:8080/api/redarbor/
```

### 2. Obtener todos los empleados

```bash
curl -s http://localhost:8080/api/redarbor/ | jq .
```

### 3. Obtener un empleado por ID

```bash
curl -s http://localhost:8080/api/redarbor/1 | jq .
```

### 4. Actualizar un empleado

```bash
curl -s -i -XPUT -H "Content-Type: application/json" \
  -d '{
    "CompanyId": 1,
    "Email": "test1@test.test.tmp",
    "Fax": "000.000.000",
    "Name": "test1",
    "Password": "test",
    "PortalId": 1,
    "RoleId": 1,
    "StatusId": 1,
    "Telephone": "000.000.000",
    "Username": "test1updated"
  }' \
  http://localhost:8080/api/redarbor/1
```

### 5. Verificar la actualización

```bash
curl -s http://localhost:8080/api/redarbor/ | jq .
```

### 6. Eliminar un empleado

```bash
curl -s -XDELETE http://localhost:8080/api/redarbor/1
```

### 7. Verificar que fue eliminado

```bash
curl -s http://localhost:8080/api/redarbor/ | jq .
```

---

## Tests unitarios

```bash
dotnet test Redarbor.Tests
```

Los tests cubren handlers de Commands y Queries, validaciones de FluentValidation y repositorios con mocks.

---

## Principios aplicados

- **DDD**: Entidades con comportamiento en el dominio, separación de capas
- **CQRS**: Separación explícita de operaciones de lectura y escritura
- **SOLID**: Interfaces por dependencia, responsabilidad única por clase/handler
- **Clean Code**: Nombres expresivos, métodos pequeños, sin lógica duplicada
- **OOP**: Encapsulación, herencia controlada, polimorfismo vía interfaces

---

