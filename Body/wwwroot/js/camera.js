window.cameraManager = {
    // 1. Avvia lo streaming della telecamera e lo collega all'elemento video
    getCameraStream: async function (videoElement) {
        try {
            // Richiede l'accesso alla telecamera
            const stream = await navigator.mediaDevices.getUserMedia({ video: true });
            
            // Collega lo stream all'elemento HTML <video>
            videoElement.srcObject = stream;
            
            // L'evento 'onloadeddata' assicura che il video sia pronto per la riproduzione
            videoElement.onloadeddata = function() {
                console.log("Video stream started. Video size:", videoElement.videoWidth, "x", videoElement.videoHeight);
                videoElement.play();
            };
        } catch (error) {
            console.error('Errore nell\'accesso alla telecamera:', error);
            // Invia un avviso all'utente in caso di blocco dei permessi
            alert('Errore nell\'accesso alla telecamera: ' + error.message + '. Controlla i permessi del browser.');
            // Ritorna null per indicare al C# che l'operazione è fallita
            return null;
        }
    },

    // 2. Cattura l'immagine dal frame corrente del video
    takePhoto: function (videoElement) {
        // Controllo robusto: verifica se il video è attivo e ha dimensioni valide
        if (!videoElement || videoElement.videoWidth === 0 || videoElement.videoHeight === 0) {
            console.error('Il video non è pronto o le sue dimensioni non sono valide.');
            return null; // Ritorna null, che Blazor gestirà come stringa nulla
        }

        // Crea un canvas nascosto per disegnare il frame del video
        const canvas = document.createElement('canvas');
        canvas.width = videoElement.videoWidth;
        canvas.height = videoElement.videoHeight;
        const context = canvas.getContext('2d');
        
        // Disegna il frame corrente
        context.drawImage(videoElement, 0, 0, canvas.width, canvas.height);

        // Converte l'immagine del canvas in una stringa Base64
        const imageData = canvas.toDataURL('image/jpeg', 0.9); // Qualità 90%
        return imageData;
    },

    // 3. Ferma lo streaming della telecamera
    stopCamera: function (videoElement) {
        if (videoElement && videoElement.srcObject) {
            // Ottiene tutte le tracce (video, audio) e le ferma
            videoElement.srcObject.getTracks().forEach(track => track.stop());
            videoElement.srcObject = null;
        }
    }
};