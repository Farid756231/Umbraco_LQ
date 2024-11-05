let quizzes = [];
let currentQuizIndex = 0;
let selectedWord = null;
let correctAnswersCount = 0;
let userAnswers = []; 

async function fetchQuizData() {
    try {
        const response = await fetch('https://localhost:44320/api/Quiz');
        if (!response.ok) {
            throw new Error(`Network response was not ok: ${response.statusText}`);
        }

        quizzes = await response.json();
        console.log('Quiz Data:', quizzes);

        if (!Array.isArray(quizzes) || quizzes.length === 0) {
            throw new Error('No quizzes found or data format is incorrect');
        }

        quizzes.forEach((quiz, index) => {
            if (!quiz.id || !quiz.englishText || !quiz.swedishText || !quiz.missingWords || !quiz.correctAnswer) {
                console.warn(`Quiz at index ${index} is missing required fields:`, quiz);
            }
        });

        displayQuiz();
    } catch (error) {
        console.error('Error fetching data:', error);
        const container = document.getElementById('quiz-container');
        container.innerHTML = '<p>Error loading quiz data. Please try again later.</p>';
    }
}

function displayQuiz() {
    const container = document.getElementById('quiz-container');
    container.innerHTML = '';

    if (currentQuizIndex < quizzes.length) {
        const quiz = quizzes[currentQuizIndex];

        const quizId = quiz.id || 'N/A';
        const englishText = quiz.englishText || 'No English text available';
        const swedishText = quiz.swedishText || 'No Swedish text available';
        const missingWords = Array.isArray(quiz.missingWords) ? quiz.missingWords : [];

        const randomMissingWord = missingWords[Math.floor(Math.random() * missingWords.length)];


        const remainingWords = missingWords.filter(word => !options.has(word));
        if (remainingWords.length > 0) {
        const randomThirdWord = remainingWords[Math.floor(Math.random() * remainingWords.length)];
            options.add(randomThirdWord);

      
        const shuffledWords = shuffleArray([...options]);

        const quizElement = document.createElement('div');
        quizElement.classList.add('quiz');

        quizElement.innerHTML = `
            <h2>Quiz ID: ${quizId}</h2>
            <p><strong>English Text:</strong> ${englishText}</p>
            <p><strong>Swedish Text:</strong> ${swedishText}</p>
            <p><strong>Choose the correct missing word:</strong></p>
            <div id="word-buttons"></div>
            <button id="nextButton" class="nextButton" disabled>Next</button>
        `;

        container.appendChild(quizElement);
        const wordButtonsContainer = document.getElementById('word-buttons');

        shuffledWords.forEach(word => {
            const button = document.createElement('button');
            button.className = 'word-button';
            button.textContent = word;
            button.onclick = () => handleWordClick(word);
            wordButtonsContainer.appendChild(button);
        });

        document.getElementById('nextButton').onclick = handleNextQuiz;

    } else {
        displayResults();
    }
}

function handleWordClick(word) {
    selectedWord = word;
    const currentQuiz = quizzes[currentQuizIndex];

    userAnswers[currentQuizIndex] = { selected: selectedWord, correct: currentQuiz.correctAnswer };

    document.querySelectorAll('.word-button').forEach(button => {
        button.classList.remove('selected');
    });
    const selectedButton = Array.from(document.querySelectorAll('.word-button')).find(button => button.textContent === word);
    if (selectedButton) {
        selectedButton.classList.add('selected');

        if (selectedButton.textContent === currentQuiz.correctAnswer) {
            selectedButton.classList.add('correct'); 
        } else {
            selectedButton.classList.add('incorrect'); 
            const correctButton = Array.from(document.querySelectorAll('.word-button')).find(button => button.textContent === currentQuiz.correctAnswer);
            if (correctButton) {
                correctButton.classList.add('correct'); 
            }
        }
    }
    document.getElementById('nextButton').disabled = false;
}

function handleNextQuiz() {
    const currentQuiz = quizzes[currentQuizIndex];
    const isCorrect = selectedWord === currentQuiz.correctAnswer;

    if (isCorrect) {
        correctAnswersCount++;
    }

    selectedWord = null;
    currentQuizIndex++;

    displayQuiz();
}

async function displayResults() {
    const container = document.getElementById('quiz-container');
    const resultDiv = document.createElement('div');
    resultDiv.className = 'result-container';
    container.innerHTML = ''; 

    const resultHeader = document.createElement('h2');
    resultHeader.textContent = 'Quiz Result';
    resultDiv.appendChild(resultHeader);

    resultDiv.innerHTML += `You got ${correctAnswersCount} out of ${quizzes.length} correct!<br><br>`;

    const questionsContainer = document.createElement('div');
    questionsContainer.classList.add('questions-container'); 

    quizzes.forEach((quiz, index) => {
        const questionResultDiv = document.createElement('div');
        questionResultDiv.classList.add('question-result');

        const userAnswer = userAnswers[index] ? userAnswers[index].selected : 'No answer'; // Använd användarens svar
        const correctAnswer = quiz.correctAnswer;

        const userAnswerIsCorrect = userAnswer === correctAnswer;
        const answerColor = userAnswerIsCorrect ? 'green' : 'red';

        questionResultDiv.innerHTML = `
            <p>Question ${index + 1}: ${quiz.swedishText}</p>
            <p style="color: ${answerColor};">Your Answer: ${userAnswer}</p>
            <p>Correct Answer: ${correctAnswer}</p>
        `;

        questionsContainer.appendChild(questionResultDiv);
    });

    resultDiv.appendChild(questionsContainer);
    container.appendChild(resultDiv);

  
    const resultData = {
        TotalQuestions: quizzes.length,
        CorrectAnswers: correctAnswersCount
    };

    try {
        const response = await fetch('https://localhost:44320/api/Prov1Result', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(resultData)
        });

        if (!response.ok) {
            throw new Error(`Failed to save result: ${response.statusText}`);
        }

        console.log('Result saved successfully');
        resultDiv.innerHTML += '<p>Result has been saved successfully!</p>';
    } catch (error) {
        console.error('Error saving result:', error);
        resultDiv.innerHTML += '<p>Failed to save result. Please try again later.</p>';
    }

    const resetButton = document.createElement('button');
    resetButton.className = 'ResetQuizButton';
    resetButton.textContent = 'Restart Quiz';
    resetButton.onclick = resetQuiz;
    container.appendChild(resetButton); 
}

function resetQuiz() {
    currentQuizIndex = 0;
    correctAnswersCount = 0;
    userAnswers = [];
    fetchQuizData(); 
}

function shuffleArray(array) {
    const shuffled = [...array];
    for (let i = shuffled.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [shuffled[i], shuffled[j]] = [shuffled[j], shuffled[i]];
    }
    return shuffled;
}

fetchQuizData();

