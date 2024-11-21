document.addEventListener("DOMContentLoaded", () => {
    updateAuthButton();
});

async function handleLogin(event) {
    event.preventDefault();
    const email = document.getElementById("loginEmail").value;
    const password = document.getElementById("loginPassword").value;

    try {
        const response = await fetch("https://localhost:44320/api/umbraco/auth/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password })
        });

        if (!response.ok) {
            alert(await response.text());
            return;
        }

        const responseText = await response.text();
        if (responseText === "success") {
            sessionStorage.setItem("authToken", "your_token");  
            closeModal();
            alert("Login successful!");
            updateAuthButton();
        } else {
            alert("Unexpected response: " + responseText);
        }
    } catch (error) {
        console.error("Error logging in:", error);
        alert("An error occurred. Please try again.");
    }
}
async function handleRegister(event) {
    event.preventDefault();
    const email = document.getElementById("registerEmail").value;
    const password = document.getElementById("registerPassword").value;

    try {
        const response = await fetch("https://localhost:44320/api/umbraco/auth/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password })
        });

        if (!response.ok) {
            alert(await response.text());
            return;
        }

        const responseData = await response.json();
        if (responseData.token) { 
            sessionStorage.setItem("authToken", responseData.token); 
            closeModal();
            alert("Registration successful!"); 
            updateAuthButton();
        } else {
            alert("Unexpected response");
        }
    } catch (error) {
        console.error("Error during registration:", error);
        alert("An error occurred. Please try again.");
    }
}


async function handleLogout() {
    try {
        const response = await fetch("https://localhost:44320/api/umbraco/auth/logout", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${sessionStorage.getItem("authToken")}`
            }
        });

        if (response.ok) {
            sessionStorage.removeItem("authToken"); // Remove token on logout
            alert("Logged out successfully!");
            updateAuthButton();
        } else {
            alert("Failed to log out. Please try again.");
        }
    } catch (error) {
        console.error("Error logging out:", error);
        alert("An error occurred. Please try again.");
    }
}

function updateAuthButton() {
    const authButton = document.getElementById("authButton");
    const isLoggedIn = !!sessionStorage.getItem("authToken");

    if (isLoggedIn) {
        authButton.textContent = "Logout";
        authButton.onclick = handleLogout;
    } else {
        authButton.textContent = "Login/Register";
        authButton.onclick = openLoginModal;
    }
}