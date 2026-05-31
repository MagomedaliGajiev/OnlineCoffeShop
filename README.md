# OnlineCoffeShop

Веб-приложение интернет-магазина кофе на **ASP.NET Core MVC (.NET 10)**. Каталог товаров с фильтрацией и полнотекстовым поиском через Elasticsearch, корзина, избранное, сравнение товаров, оформление заказа и админ-панель управления товарами, заказами, пользователями и ролями.

## Возможности

- 🛍️ **Каталог товаров** — кофе, аксессуары и сопутствующие товары с категориями, тегами, обжаркой, происхождением и вкусовыми нотами
- 🔎 **Поиск** — полнотекстовый поиск по товарам на базе Elasticsearch
- 🛒 **Корзина** — хранение в базе данных через сессию
- ❤️ **Избранное** и ⚖️ **Сравнение** товаров — хранение в сессии
- 💳 **Оформление заказа** (Checkout) с валидацией данных, включая проверку даты доставки в пределах 3 месяцев
- 👤 **Аккаунт** — вход и регистрация
- 🛠️ **Админ-панель** — управление товарами, заказами, пользователями и ролями

## Технологии

| Слой | Технология |
|------|------------|
| Платформа | .NET 10 |
| Веб | ASP.NET Core MVC |
| База данных | Entity Framework Core (In-Memory) |
| Поиск | Elasticsearch (`Elastic.Clients.Elasticsearch` 9.4.0) |
| Фронтенд | Razor Views, Bootstrap, jQuery Validation |
| Анализ кода | StyleCop.Analyzers |

## Структура проекта

```
OnlineCoffeShop.Web/
├── Controllers/        # Контроллеры (Home, Product, Cart, Fav, Compare, Checkout, Account, Admin, Success)
├── Models/             # Доменные модели и view-модели
├── Data/               # AppDbContext (EF Core)
├── Repositories/       # Репозитории и сервис поиска
│   └── Abstractions/   # Интерфейсы репозиториев
├── Validation/         # Кастомные атрибуты валидации
├── Views/              # Razor-представления
└── wwwroot/            # Статика (css, js, fonts, lib)
```

## Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (для Elasticsearch)

## Запуск

### 1. Поднять Elasticsearch

```bash
docker compose up -d
```

Elasticsearch будет доступен на `http://localhost:9202`.

### 2. Запустить приложение

```bash
cd OnlineCoffeShop.Web
dotnet run
```

Приложение откроется по адресу:

- HTTP: `http://localhost:5244`
- HTTPS: `https://localhost:7150`

> При старте товары автоматически индексируются в Elasticsearch.

## Конфигурация

Настройки поиска задаются в `appsettings.json`:

```json
{
  "Elasticsearch": {
    "Uri": "http://localhost:9202",
    "ProductsIndex": "products"
  }
}
```

## Заметки

- База данных работает в режиме **In-Memory** — данные не сохраняются между перезапусками, товары загружаются из `InMemoryProductRepository`.
- Корзина хранится в БД, избранное и сравнение — в сессии.
