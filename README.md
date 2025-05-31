# Zasobowo – System Zarządzania Zasobami IT

Zasobowo to aplikacja rozproszona do zarządzania sprzętem w małej firmie IT. Obsługuje użytkowników, urządzenia, synchronizację danych w czasie rzeczywistym oraz podstawową autoryzację JWT. Projekt został opracowany jako aplikacja akademicka w technologii C#/.NET z podziałem na trzy niezależne moduły.

## Moduły projektu

Projekt składa się z trzech niezależnych aplikacji w jednej solucji:

| Projekt            | Opis                                                                 |
|--------------------|----------------------------------------------------------------------|
| Zasobowo.API       | Backend REST API (.NET 7) z obsługą SQLite, JWT i Swaggera           |
| Zasobowo.Client    | Aplikacja desktopowa WPF – interfejs do zarządzania urządzeniami     |
| Zasobowo.Sync      | Moduł synchronizacji – komunikacja SignalR w czasie rzeczywistym     |

## Główne funkcje

- Rejestracja i logowanie użytkowników (JWT)
- Zarządzanie urządzeniami (CRUD)
- Przypisywanie urządzeń użytkownikom
- Walidacja formularzy i danych
- Synchronizacja danych między klientem a backendem z użyciem SignalR
- Zasady:
  - Nie można przypisać urządzenia, jeśli jego status to "Dostępny" lub "Zepsuty"
  - Nie można dodać urządzenia o tej samej nazwie (walidacja duplikatów)

## Uruchamianie projektu lokalnie

### Wymagania

- Visual Studio 2022
- .NET 7.0 SDK
- SQLite (lokalna baza danych)
- System operacyjny Windows 10/11

### Instrukcja

1. Klonuj repozytorium:
   git clone https://github.com/rockandlog/StudiumPrzypadku.git
   cd StudiumPrzypadku

2. Otwórz plik `Zasobowo.sln` w Visual Studio.

3. Ustaw projekt startowy:
   - Backend: `Zasobowo.API`
   - Interfejs użytkownika: `Zasobowo.Client`

4. Uruchom projekt `Zasobowo.API` (uruchomi Swagger i backend API).
   Interfejs Swaggera będzie dostępny pod adresem `https://localhost:7031/swagger`.

5. Uruchom aplikację `Zasobowo.Client` (desktop WPF).

6. Opcjonalnie uruchom `Zasobowo.Sync`, aby przetestować synchronizację w czasie rzeczywistym.

## Autoryzacja

System logowania wykorzystuje token JWT. Po zalogowaniu token jest przechowywany lokalnie i dołączany do nagłówka `Authorization: Bearer <token>` przy zapytaniach HTTP.

## Testowanie

Testy funkcjonalne wykonano manualnie:
- Walidacja formularzy w aplikacji WPF (brak możliwości zapisania pustych pól).
- Komunikacja API przetestowana z użyciem Swaggera.
- Przypadki błędów (duplikaty nazw, niepoprawny status) obsługiwane i raportowane.

## Struktura katalogów

Zasobowo/
├── Zasobowo.API/          // Backend API (ASP.NET Core)
├── Zasobowo.Client/       // Interfejs graficzny (WPF)
├── Zasobowo.Sync/         // Synchronizacja (SignalR)
└── Zasobowo.sln           // Plik solucji Visual Studio

## Technologie

- .NET 7
- ASP.NET Core Web API
- WPF (Model-View-ViewModel)
- Entity Framework Core (SQLite)
- SignalR
- JWT (autoryzacja)
- Swagger / OpenAPI
