document.addEventListener('DOMContentLoaded', function () {
    let questions = [];
    let currentQuestionIndex = 0;
    let selectedOption = '';
    let correctAnswersCount = 0;
    const results = [];
    const quizContainer = document.getElementById('quiz-container');

    async function fetchQuestions() {
        try {
            const response = await fetch('https://localhost:44320/api/Prov3Quizz');
            if (!response.ok) throw new Error('Nätverksresponsen var inte okej');
            questions = await response.json();
            console.log('Frågor hämtade:', questions); 
            displayQuestion();
        } catch (error) {
            quizContainer.innerHTML = "<p>Det gick inte att hämta frågorna</p>";
            console.error("Det gick inte att hämta frågorna", error);
        }
    }

    function displayQuestion() {
        if (questions.length === 0) return;

        const currentQuestion = questions[currentQuestionIndex];
        const optionsHtml = currentQuestion.options.map(option =>
            `<button class="option-button" onclick="selectOption('${option}')">${option}</button>`
        ).join('');

        quizContainer.innerHTML = `
         <div class="quiz-container">
            <h2>${currentQuestion.question}</h2>
            <img src="/media/${currentQuestion.imagePath}" alt="Frågebild" style="width: 300px; height: auto;">
            <div class="options-container">${optionsHtml}</div>
            <p id="selected-option">Du valde: ${selectedOption || 'Ingen'}</p>
            <div class="navigation-buttons">
                <button onclick="goToPreviousQuestion()" ${currentQuestionIndex === 0 ? 'disabled' : ''}>Föregående fråga</button>
                <button onclick="goToFirstQuestion()">Tillbaka till start</button>
                <button id="next-button" onclick="goToNextQuestion()" ${selectedOption === '' ? 'disabled' : ''}>Nästa fråga</button>
            </div>
         </div>
        `;
        console.log('Visar fråga:', currentQuestion.question);
    }

    window.selectOption = function (option) {
        selectedOption = option;
        document.getElementById('selected-option').textContent = `Du valde: ${option}`;
        console.log('Valt svar:', option); 
        document.querySelectorAll('.option-button').forEach(button => {
            button.disabled = true; 
            if (button.textContent === option) {
                const isCorrect = option === questions[currentQuestionIndex].correctAnswer;
                button.classList.add(isCorrect ? 'correct' : 'incorrect');
                console.log(`Valt svar ${option} är ${isCorrect ? 'rätt' : 'fel'}`); 
            }
        });
        document.getElementById('next-button').disabled = false; 
    }

    window.goToNextQuestion = function () {
        const currentQuestion = questions[currentQuestionIndex];
        const isCorrect = selectedOption === currentQuestion.correctAnswer;
        if (isCorrect) correctAnswersCount++;

        console.log(`Svar på fråga ${currentQuestion.question}: Du valde ${selectedOption}, Rätt svar var ${currentQuestion.correctAnswer}.`);

        results.push({
            question: currentQuestion.question,
            selectedOption,
            isCorrect,
            correctAnswer: currentQuestion.correctAnswer
        });

        if (currentQuestionIndex + 1 < questions.length) {
            currentQuestionIndex++;
            selectedOption = '';
            displayQuestion();
        } else {
            displayResults();
            saveResultsToDatabase();
        }
    }

    window.goToPreviousQuestion = function () {
        if (currentQuestionIndex > 0) {
            currentQuestionIndex--;
            selectedOption = '';
            displayQuestion();
        }
    }

    window.goToFirstQuestion = function () {
        currentQuestionIndex = 0;
        selectedOption = '';
        displayQuestion();
    }

    function displayResults() {
        const resultsHtml = results.map(result => `
            <li class="result-item">
                <span class="question-text">${result.question}</span>
                <span class="${result.isCorrect ? 'correct' : 'incorrect'}">
                    ${result.selectedOption} (${result.isCorrect ? 'Rätt' : 'Fel'})
                </span>
                ${!result.isCorrect ? `<span class="correct-answer">- Rätt svar: ${result.correctAnswer}</span>` : ''}
            </li>
        `).join('');

        quizContainer.innerHTML = `
            <div class="quiz-container">
                <h2>Resultat</h2>
                <p>Du fick ${correctAnswersCount} av ${questions.length} rätt!</p>
                <ul>${resultsHtml}</ul>
                <button onclick="window.location.reload()">Spela igen</button>
            </div>
        `;
        console.log(`Totalt antal rätta svar: ${correctAnswersCount} av ${questions.length}`); 
    }

    async function saveResultsToDatabase() {
        const resultData = {
            totalQuestions: questions.length,
            correctAnswers: correctAnswersCount
        };

        try {
            const response = await fetch('https://localhost:44320/api/Prov3QuizzResult', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(resultData)
            });

            if (!response.ok) {
                throw new Error('Misslyckades med att spara quizresultat');
            }
            console.log('Resultat sparat framgångsrikt');
        } catch (error) {
            console.error(error.message);
        }
    }

    fetchQuestions();
});

