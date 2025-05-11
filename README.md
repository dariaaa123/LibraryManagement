
#  Library Management System – ASP.NET Core Web API

Această aplicație backend scrisă în C# folosind ASP.NET Core permite gestionarea unei biblioteci, cu funcționalități CRUD pentru cărți, împrumut și returnare, plus o extensie pentru notificare prin email (simulată) când o carte devine disponibilă.

---

##  Cerințe minime

- .NET SDK 6.0 sau mai nou (7/8 sunt OK)  
- Un IDE compatibil cu C#:  
  -  Rider (JetBrains)  
  - Visual Studio 2022+  
  - Visual Studio Code (cu extensia C#)

---

##  Pași pentru rulare

1. **Descarcă sau clonează proiectul**
   - Deschide folderul în IDE
   - Sunt necesare fișierele:
     - `Program.cs`, `LibraryManagementSystem.csproj`
     - `Controllers/`, `Models/`, `Services/`, `Data/`
     - `Data/books.json` și `Data/subscriptions.json` (pot fi goale)

2. **Restaurare și rulare**

```bash
 dotnet restore
 dotnet run
```

3. **Deschide browserul la:**

```
http://localhost:xxxx/swagger
```

(înlocuiește `xxxx` cu portul din consolă, ex. `http://localhost:5236/swagger`)

---

##  Testare în Swagger UI

Swagger oferă o interfață grafică pentru a testa rapid toate endpoint-urile.

### Pași:
1. Deschide `http://localhost:xxxx/swagger`
2. Selectează un endpoint (ex: `POST /api/books`)
3. Apasă `Try it out`
4. Completează datele în format JSON
5. Apasă `Execute`

---

##  Funcționalități și cum se folosesc

### 1.  Adaugă o carte  
`POST /api/books`

```json
{
  "title": "The Hobbit",
  "author": "J.R.R. Tolkien",
  "quantity": 3
}
```

 Nu pune `id`, se generează automat.

---

### 2.  Vezi toate cărțile  
`GET /api/books`

---

### 3.  Caută cărți după titlu/autor  
`GET /api/books/search?title=Hobbit&author=Tolkien`

---

### 4.  Actualizează o carte existentă  
`PUT /api/books/{id}`  
Copiază `id`-ul cărții și trimite:

```json
{
  "id": "guid-ul-cartii",
  "title": "Hobbit Updated",
  "author": "J.R.R.",
  "quantity": 2
}
```

---

### 5.  Șterge o carte  
`DELETE /api/books/{id}`

---

### 6.  Împrumută o carte  
`POST /api/books/{id}/lend`  
→ Scade quantity cu 1. Dacă e 0, apare mesaj de eroare.

---

### 7.  Returnează o carte  
`POST /api/books/{id}/return`  
→ Crește quantity cu 1. Dacă existau subscrieri la acea carte, afișează emailurile în consolă.

---

### 8.  Subscriere pentru notificare  
`POST /api/books/{id}/subscribe?email=exemplu@email.com`  
→ Funcționează doar dacă quantity este 0. Se salvează emailul în așteptare.

---

### 9. Notificare când devine disponibilă  
Când cineva returnează cartea, toate emailurile înregistrate pentru acea carte vor fi afișate în consolă, simulând o notificare.

---

##  Persistență (fără bază de date)

Toate datele se salvează local, automat:

- `Data/books.json` – lista cărților
- `Data/subscriptions.json` – emailuri în așteptare

---

##  Exemplu de flux complet:

1. Adaugă o carte cu quantity = 1  
2. Împrumută cartea → quantity = 0  
3. Fă `POST /subscribe` cu email  
4. Fă `POST /return`  
 Vezi emailul în consolă → “Notifying subscribers of book...”

---



