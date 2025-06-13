window.initMap = (dotNetRef, lat, lon) => {
    const map = L.map('leaflet-map').setView([lat, lon], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    let marker = L.marker([lat, lon], { draggable: true }).addTo(map);

    function updateCoords(e) {
        const { lat, lng } = e.latlng || e.target.getLatLng();
        dotNetRef.invokeMethodAsync('UpdateCoordinatesFromJs', lat, lng);
    }

    map.on('click', function (e) {
        marker.setLatLng(e.latlng);
        updateCoords(e);
    });

    marker.on('dragend', function (e) {
        updateCoords(e);
    });
};
