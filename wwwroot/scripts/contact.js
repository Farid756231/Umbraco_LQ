    $(document).ready(function () {
        
        $('#contactForm').submit(function (e) {
            e.preventDefault();

            const messageData = {
                name: $('#contactName').val(),
                email: $('#contactEmail').val(),
                message: $('#contactMessage').val()
            };

            $.ajax({
                url: 'https://localhost:44320/api/Contact',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(messageData),
                success: function () {
                    $('#thankYouMessage').show();
                    $('#contactForm')[0].reset();
                    location.reload();  
                },
                error: function () {
                    alert('Kunde inte skicka meddelandet');
                }
            });
        });
    });
</script>