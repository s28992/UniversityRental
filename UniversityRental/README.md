## Opis projektu

Prosta aplikacja konsolowa C#, która obsługuje uczelnianą wypożyczalnię sprzętu.

Program pozwala:
- dodawać użytkowników
- dodawać różne typy sprzętów
- wypożyczać sprzęt
- zwracać sprzęt
- oznaczać sprzęt jako niedostępny
- sprawdzać aktywne i przeterminowane wypożyczenia
- wyświetlać raport końcowy

## Podział projektu

Kod podzieliłem na kilka folderów:
- `Models` - klasy opisujące sprzęt, użytkowników i wypożyczenia
- `Repositories` - przechowywanie danych w pamięci
- `Rules` - zasady dotyczące limitów i kar
- `Services` - logika działania programu
- `Program.cs` - przykładowy scenariusz pokazujący działanie programu

## Decyzje

Użyłem klas abstrakcyjnych `Equipment` oraz `User`, ponieważ różne typy sprzętu i użytkowników mają część wspólnych danych jak i swoje własne pola.

Dodałem interfejsy dla repozytoriów i reguł, bo te elementy mogłyby się łatwo kiedyś zmienić.

## Odpowiedzialność klas

Starałem się, żeby każda klasa miała jedno główne zadanie:
- `RentalService` zajmuje się wypożyczeniami i zwrotami
- `EquipmentService` obsługuje sprzęt
- `UserService` obsługuje użytkowników
- `ReportService` tworzy raport
- `UserLimitRule` odpowiada za limity wypożyczeń
- `PenaltyCalculator` odpowiada za naliczanie kar