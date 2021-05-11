Dwa panele: użytkownik i admin


Użytkownik:
-wpłaca i wypłaca,
-sprawdza historię operacji na koncie,
-generuje podsumowanie: ile wpłacił i ile wypłacił od początku
-może wziąć kredyt (pole kredyt w klasie Konto), ale tylko jeden. Metoda SpłaćKredyt przelewa pieniądze z konta na spłatę kredytu.
-może zrobić przelew na inne konto
-wszystkie operacje możliwe dopiero po zalogowaniu na konto (hasło przechowujemy w klasie Account)


Admin
-może zablokować konto użytkownikowi (pole typu bool w Account), wtedy użytkownik nie może się zalogować
-może dodawać i usuwać konta
-może robić podsumowania: ile w sumie jest pieniędzy w banku, ile wynosi łączny kredyt itd
-jest w posiadaniu historii kredytowej każdego klienta, może też pobrać wszystkich aktualnych dłużników


konta w banku przechowujemy w kolekcji ArrayList
