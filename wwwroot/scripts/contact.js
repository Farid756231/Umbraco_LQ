$(document).ready(function () {

    function fetchContacts() {
        console.log("Starting GET request to fetch contacts...");
        $.ajax({
            url: 'https://localhost:44320/api/Contact',
            type: 'GET',
            contentType: 'application/json',

            success: function (contacts) {
                console.log("GET request successful. Data received:", contacts);
                const contactList = $('#contactList');
                contactList.empty(); 

                if (contacts.length === 0) {
                    contactList.append('<li>No contacts available</li>');
                } else {
                    contacts.forEach(function (contact) {
                        contactList.append(`
                            <li>
                                <strong>Name:</strong> ${contact.name}<br>
                                <strong>Email:</strong> ${contact.email}<br>
                                <strong>Message:</strong> ${contact.description}
                            </li>
                            <hr>
                        `);
                    });
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error fetching contacts:", textStatus, errorThrown);
                console.error("Response details:", jqXHR.responseText);
            }
        });
    }

    fetchContacts();
    $('#contactForm').submit(function (e) {
        e.preventDefault();
        console.log("Form submit event triggered");

        const messageData = {
            name: $('#contactName').val(),
            email: $('#contactEmail').val(),
            description: $('#contactMessage').val()  
        };

        console.log("Data to be sent:", messageData);

        $.ajax({
            url: 'https://localhost:44320/api/Contact',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(messageData),
            success: function () {
                $('#thankYouMessage').show();
                $('#contactForm')[0].reset();
                fetchContacts();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error in AJAX request:", textStatus, errorThrown);
                console.error("Response details:", jqXHR.responseText);
                alert('Kunde inte skicka meddelandet');
            }
        });
    });
});