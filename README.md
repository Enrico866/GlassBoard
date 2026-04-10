GlassBoard – Network Monitoring Dashboard
GlassBoard è una dashboard di monitoraggio di rete moderna e scalabile, sviluppata con Blazor WebAssembly e .NET 8. Il progetto è stato progettato seguendo un'architettura a livelli per garantire manutenibilità, separazione delle responsabilità e sicurezza dei dati.

🚀 Architettura e Pattern
Il cuore del progetto è stato recentemente rifattorizzato per passare da una gestione monolitica della logica UI a un'architettura Provider-Service:

Service Layer (Data Access): Gestisce la comunicazione HTTP con le API esterne, la gestione dei token JWT e la logica di fallback. Utilizza HttpClient e il pattern IOptions per una configurazione tipizzata.

Provider Layer (State Management): Agisce come un'interfaccia tra i servizi e la UI, gestendo la cache dei dati in memoria (In-Memory Caching) per ridurre il numero di chiamate API e migliorare la fluidità dell'interfaccia utente.

Dependency Injection: Ogni componente è registrato nel container DI di .NET, facilitando il testing e il disaccoppiamento dei componenti.

🛠 Tech Stack
Framework: .NET 8 (Blazor WASM)

UI Library: MudBlazor per una UX fluida e componenti Material Design.

Data Access: Dapper (lato server) e HttpClient (lato client).

Configurazione: Pattern Options per la gestione centralizzata degli endpoint.

🔒 Sicurezza e Gestione Configurazioni
Il progetto adotta standard di sicurezza enterprise per la gestione delle informazioni sensibili:

Zero Credentials in Git: Nessuna password o segreto è salvato nel repository. Il file appsettings.json contiene solo segnaposto e URL pubblici.

User Secrets: In fase di sviluppo, le credenziali vengono caricate tramite il tool User Secrets di .NET, garantendo che i dati sensibili rimangano solo sulla macchina dello sviluppatore.

Template di Configurazione: Viene fornito un file appsettings.json.example per permettere una configurazione rapida del progetto senza esporre i dati di produzione.

📦 Installazione e Setup
Clona il repository:

Bash

git clone https://github.com/tuo-username/GlassBoard.git
Configura l'ambiente:
Rinomina il file Body/appsettings.json.example in Body/appsettings.json e inserisci gli URL corretti delle API.

Configura i segreti (Opzionale ma raccomandato):
Utilizza la CLI di .NET o Visual Studio per aggiungere le tue credenziali locali:

Bash

dotnet user-secrets set "Tenant:Admin:User" "tua-email@esempio.com"
dotnet user-secrets set "Tenant:Admin:Password" "tua-password"
Esegui l'applicazione:
Premi F5 in Visual Studio o lancia:

Bash

dotnet run
📈 Funzionalità Implementate
Gestione Multitenant: Selezione dinamica di Tenant e Organizzazioni con caricamento condizionale dei database.

Monitoraggio Real-time: Visualizzazione di sonde (Probe), Check e Collettori con aggiornamento dello stato in cache.

Job Management: Generazione e notifica di job di scansione rete tramite interfacce ottimizzate.

Architettura Estensibile: Facilità di aggiunta di nuovi tipi di monitoraggio grazie alle astrazioni implementate.