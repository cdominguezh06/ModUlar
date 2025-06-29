// Función para cargar el SDK de PayPal de forma dinámica
function loadScript(src) {
    return new Promise((resolve, reject) => {
        if (document.querySelector(`script[src="${src}"]`)) {
            resolve();
            return;
        }

        const script = document.createElement('script');
        script.src = src;
        script.charset = "UTF-8";
        script.onload = resolve;
        script.onerror = reject;
        document.body.appendChild(script);
    });
}

// Función para inicializar el botón de donación
window.initializePayPalDonateButton = async function (buttonId, hostedButtonId) {
    try {
        // Cargar el SDK de PayPal
        await loadScript("https://www.paypalobjects.com/donate/sdk/donate-sdk.js");

        // Asegurarse de que el SDK está cargado
        if (typeof PayPal !== 'undefined' && PayPal.Donation) {
            // Renderizar el botón de donación
            PayPal.Donation.Button({
                env: 'production',
                hosted_button_id: hostedButtonId,
                image: {
                    src: 'https://www.paypalobjects.com/en_US/ES/i/btn/btn_donateCC_LG.gif',
                    alt: 'Donate with PayPal button',
                    title: 'PayPal - The safer, easier way to pay online!',
                }
            }).render(`#${buttonId}`);

            console.log("PayPal donation button initialized successfully.");
        } else {
            console.error("PayPal SDK loaded but PayPal.Donation is not available");
        }
    } catch (error) {
        console.error("Error initializing PayPal button:", error);
    }
};