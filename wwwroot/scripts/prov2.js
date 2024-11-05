let quizData = [];
let currentQuestionIndex = 0;
let userAnswers = [];
let correctAnswersCount = 0;
let selectedOption = null;
let isOptionSelected = false;
let showResult = false;
let resultSaved = false; 

const fetchQuizData = async () => {
    try {
        const response = await fetch("https://localhost:44320/api/Prov2Quizz");
        if (!response.ok) throw new Error("Network response was not ok");

        quizData = await response.json();
        console.log("Quiz Data Loaded:", quizData); 
        displayQuestion();
    } catch (error) {
        displayError(error.message);
    }
};

const displayQuestion = () => {
    const container = document.querySelector(".quiz-container");
    container.innerHTML = "";

    if (showResult) {
        displayResult();
        return;
    }

    const questionData = quizData[currentQuestionIndex];

    const questionHTML = `
        <h1>Grammatik Quiz</h1>
        <div class="question-container">
            <h3 class="question">${questionData.question}</h3>
            <ul class="options">
                ${questionData.options.map((option, index) => `
                    <li class="option" data-option="${option}" style="cursor: ${isOptionSelected ? 'not-allowed' : 'pointer'};">
                        ${option}
                    </li>
                `).join("")}
            </ul>
            <div class="navigation">
             <button id="nextQuestionBtn" ${!isOptionSelected ? "disabled" : ""}>
                    ${currentQuestionIndex === quizData.length - 1 ? "Visa Resultat" : "Nästa"}
                </button>
                <button id="firstQuestionBtn" ${currentQuestionIndex === 0 ? "disabled" : ""}>Tillbaka till Första Frågan</button>
            </div>
        </div>
    `;
    container.innerHTML = questionHTML;

    document.querySelectorAll(".option").forEach(option => {
        option.addEventListener("click", handleOptionSelect);
    });
    document.getElementById("firstQuestionBtn").addEventListener("click", goToFirstQuestion);
    document.getElementById("nextQuestionBtn").addEventListener("click", nextQuestion);
};

const handleOptionSelect = (event) => {
    if (isOptionSelected) return;

    selectedOption = event.target.getAttribute("data-option");
    isOptionSelected = true;

    const questionData = quizData[currentQuestionIndex];
    const isCorrect = selectedOption === questionData.correctAnswer;
    userAnswers.push({
        question: questionData.question,
        selected: selectedOption,
        correct: questionData.correctAnswer,
        isCorrect: isCorrect
    });

    console.log(`Fråga: ${questionData.question}`);
    console.log(`Valt Svar: ${selectedOption}`);
    console.log(`Rätt Svar: ${questionData.correctAnswer}`);
    console.log(isCorrect ? "Rätt Svar!" : "Fel Svar!");

    if (isCorrect) correctAnswersCount++;

    document.querySelectorAll(".option").forEach(option => {
        const optionValue = option.getAttribute("data-option");
        if (optionValue === questionData.correctAnswer) {
            option.classList.add("correct");
        } else if (optionValue === selectedOption) {
            option.classList.add("incorrect");
        }
    });

    document.getElementById("nextQuestionBtn").disabled = false;
};

const nextQuestion = () => {
    if (currentQuestionIndex < quizData.length - 1) {
        currentQuestionIndex++;
        selectedOption = null;
        isOptionSelected = false;
        displayQuestion();
    } else {
        showResult = true;
        if (!resultSaved) { 
            saveResultToDatabase();
            resultSaved = true; 
        }
        displayResult(); 
    }
};

const saveResultToDatabase = async () => {
    const resultData = {
        totalQuestions: quizData.length,
        correctAnswers: correctAnswersCount
    };

    try {
        const response = await fetch("https://localhost:44320/api/Prov2QuizzResult", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(resultData)
        });
        if (!response.ok) throw new Error("Failed to save quiz result");

        console.log("Result saved successfully");
    } catch (error) {
        displayError(error.message);
    }
};

const displayResult = () => {
    const container = document.querySelector(".quiz-container");
    container.innerHTML = `
        <h1>Quiz Resultat</h1>
        <p>Du har ${correctAnswersCount} av ${quizData.length} rätt!</p>
        <div class="result">
            <ul>
                ${userAnswers.map((answer) => `
                    <li>
                        <span>${answer.question}</span><br>
                        <span class="${answer.isCorrect ? 'user-correct' : 'user-incorrect'}">
                            Ditt svar: ${answer.selected}
                        </span>
                        <br>
                        <span class="right-answer">
                            Rätt svar: ${answer.correct}
                        </span>
                    </li>
                `).join("")}
            </ul>
        </div>
        <br><br>
        <button class="ResetQuizButton" onclick="window.location.reload()">Starta om Quizet</button>
    `;

    console.log(`Du hade ${correctAnswersCount} av ${quizData.length} rätt!`);
};

const displayError = (message) => {
    const container = document.querySelector(".quiz-container");
    container.innerHTML = `<div class="error">${message}</div>`;
};

const goToFirstQuestion = () => {
    currentQuestionIndex = 0;
    selectedOption = null;
    isOptionSelected = false;
    displayQuestion();
};

const initQuizApp = () => {
    const quizContainer = document.createElement("div");
    quizContainer.className = "quiz-container";
    document.body.appendChild(quizContainer);

    fetchQuizData();
};

document.addEventListener("DOMContentLoaded", initQuizApp);
