# Redarbor Backend Technical Test

REST API para gestión de empleados construida con .NET 8, siguiendo principios de DDD, CQRS, SOLID y Clean Code.

---

## Tecnologías

- **ASP.NET Core 8** — Framework principal
- **Entity Framework Core** — Escrituras (Commands)
- **Dapper** — Lecturas (Queries)
- **MediatR** — Implementación del patrón CQRS
- **FluentValidation** — Validaciones de dominio
- **SQL Server / SQLite** — Base de datos
- **Docker** — Containerización opcional
- **xUnit + Moq** — Tests unitarios

---

## Arquitectura

El proyecto sigue una arquitectura en capas basada en DDD y CQRS:

```
src/
├── Redarbor.API/                  # Capa de presentación (Controllers, Middleware)
├── Redarbor.Application/          # Casos de uso (Commands, Queries, Handlers, DTOs)
├── Redarbor.Domain/               # Entidades, Value Objects, Interfaces del dominio
├── Redarbor.Infrastructure/       # EF Core (writes), Dapper (reads), Repositories
tests/
└── Redarbor.Tests/                # Tests unitarios
```

### Patrón CQRS

- **Commands** (escritura vía EF Core): `CreateEmployeeCommand`, `UpdateEmployeeCommand`, `DeleteEmployeeCommand`
- **Queries** (lectura vía Dapper): `GetAllEmployeesQuery`, `GetEmployeeByIdQuery`
- Cada command/query tiene su propio Handler registrado en MediatR.

---

## Modelo de Datos — Employee

| Campo       | Tipo     | Obligatorio |
|-------------|----------|-------------|
| Id          | int      | Auto        |
| CompanyId   | int      | ✅          |
| Email       | string   | ✅          |
| Password    | string   | ✅          |
| PortalId    | int      | ✅          |
| RoleId      | int      | ✅          |
| StatusId    | int      | ✅          |
| Username    | string   | ✅          |
| Name        | string   | ❌          |
| Fax         | string   | ❌          |
| Telephone   | string   | ❌          |
| CreatedOn   | datetime | ❌          |
| UpdatedOn   | datetime | ❌          |
| DeletedOn   | datetime | ❌          |
| Lastlogin   | datetime | ❌          |

---

## Endpoints de la API

| Método | Endpoint              | Descripción             |
|--------|-----------------------|-------------------------|
| GET    | /api/redarbor         | Obtener todos            |
| GET    | /api/redarbor/{id}    | Obtener por ID           |
| POST   | /api/redarbor         | Crear nuevo empleado     |
| PUT    | /api/redarbor/{id}    | Actualizar empleado      |
| DELETE | /api/redarbor/{id}    | Eliminar empleado        |

---

## Ejecución local

### Requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server o SQLite (configurable en `appsettings.json`)

### Pasos

```bash
# 1. Clonar el repositorio
git clone https://github.com/tuusuario/redarbor-api.git
cd redarbor-api

# 2. Aplicar migraciones de base de datos
dotnet ef database update --project src/Redarbor.Infrastructure

# 3. Ejecutar la API
dotnet run --project src/Redarbor.API
```

La API estará disponible en: `http://localhost:5000`

---

## Ejecución con Docker

```bash
# Construir y levantar contenedores (API + SQL Server)
docker-compose up --build
```

El archivo `docker-compose.yml` incluye:
- Servicio `api` (ASP.NET Core en puerto 5000)
- Servicio `db` (SQL Server)

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
  http://localhost:5000/api/redarbor/
```

### 2. Obtener todos los empleados

```bash
curl -s http://localhost:5000/api/redarbor/ | jq .
```

### 3. Obtener un empleado por ID

```bash
curl -s http://localhost:5000/api/redarbor/1 | jq .
```

### 4. Actualizar un empleado

```bash
curl -s -i -XPUT -H "Content-Type: application/json" \
  -d '{"Username": "test1updated", ...}' \
  http://localhost:5000/api/redarbor/1
```

### 5. Verificar la actualización

```bash
curl -s http://localhost:5000/api/redarbor/ | jq .
```

### 6. Eliminar un empleado

```bash
curl -s -XDELETE http://localhost:5000/api/redarbor/1
```

### 7. Verificar que fue eliminado

```bash
curl -s http://localhost:5000/api/redarbor/ | jq .
```

---

## Tests unitarios

```bash
dotnet test tests/Redarbor.Tests
```



## Autor

Prueba técnica — Redarbor Backend
