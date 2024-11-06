
    $(document).ready(function () {
        // Fetch reviews from API
        $.get('https://localhost:44320/api/Reviews', function (reviews) {
            const reviewList = $('#reviewList');
            reviewList.empty();

            if (reviews.length === 0) {
                reviewList.append('<li>No reviews available</li>');
            } else {
                reviews.forEach(function (review) {
                    reviewList.append(`
                        <li>
                            <strong>${review.name}</strong>
                            <p>${review.description}</p>
                            <p>${review.email}</p>
                        </li>
                    `);
                });
            }
        });

        // Submit review form
        $('#reviewForm').submit(function (e) {
            e.preventDefault();

            const reviewData = {
                name: $('#reviewName').val(),
                email: $('#reviewEmail').val(),
                description: $('#reviewDescription').val()
            };

            $.ajax({
                url: 'https://localhost:44320/api/Reviews',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(reviewData),
                success: function () {
                    $('#thankYouMessage').show();
                    $('#reviewForm')[0].reset();
                    location.reload();  // Reload to fetch updated reviews
                },
                error: function () {
                    alert('Failed to submit review');
                }
            });
        });
    });
