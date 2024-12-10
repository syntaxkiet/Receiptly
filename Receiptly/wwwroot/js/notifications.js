//Intended to be called by BrowserNotificationService

window.notify = (message) => {
    if (!("Notification" in window)) {
        console.log("This browser does not support notifications.");
        return;
    }

    if (Notification.permission === "granted") {
        new Notification(message);
    } else if (Notification.permission !== "denied") {
        Notification.requestPermission().then((permission) => {
            if (permission === "granted") {
                new Notification(message);
            }
        });
    }
};