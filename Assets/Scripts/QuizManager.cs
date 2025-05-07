using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<QuestionSO> questions;
    [SerializeField] private TMPro.TextMeshProUGUI questionText;
    [SerializeField] private TMPro.TextMeshProUGUI answerText;
    [SerializeField] private List<Toggle> answerButtons;
    [SerializeField] private GameObject quizPanelContainer;
    [SerializeField] private GameObject showQuestionButton;
    [SerializeField] private GameObject hideQuestionButton;
    [SerializeField] private GameObject correctAnswerUI;
    [SerializeField] private GameObject incorrectAnswerUI;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject quizComplete;
    [SerializeField] private AudioSource quizAudio;

    private int currentQuestionIndex = 0;

    void Start()
    {
        quizPanelContainer.SetActive(false);
    }

    public void StartQuiz()
    {
        // Only show quizPanel in the UI
        currentQuestionIndex = 0;
        quizPanelContainer.SetActive(true);
        quizComplete.SetActive(false);
        quizPanel.SetActive(true);
        DisplayCurrentQuestion();
    }
    
    public void toggleQuestionView()
    {
        if (quizPanelContainer.activeSelf)
        {
            quizPanelContainer.SetActive(false);
            showQuestionButton.SetActive(true);
        }
        else
        {
            quizPanelContainer.SetActive(true);
            showQuestionButton.SetActive(false);
        }
    }

    // Read SO question and answer data
    public void DisplayCurrentQuestion()
    {
        ResetToggles();

        QuestionSO currentQuestion = questions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;
        // Play audio for question
        if (currentQuestion.questionAudio != null)
        {
            quizAudio.clip = currentQuestion.questionAudio;
            quizAudio.Play();
        }

        for (int i = 0; i < answerButtons.Count; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];

                // Clear previous to avoid stacking answer choices
                answerButtons[i].onValueChanged.RemoveAllListeners();

                int capturedIndex = i;
                answerButtons[i].onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        HandleAnswerSelected(capturedIndex);
                    }
                });
            }
            else
            {
                // Hide unused answer buttons
                answerButtons[i].gameObject.SetActive(false); 
            }

        }

    }

    private void ResetToggles()
    {
        foreach (Toggle toggle in answerButtons)
        {
            toggle.isOn = false;
        }
    }

    public void HandleAnswerSelected(int index)
    {
        QuestionSO currentQuestion = questions[currentQuestionIndex];
        bool isCorrect = index == currentQuestion.correctAnswerIndex;

        Debug.Log(isCorrect ? "Correct!" : "Incorrect.");
        // Correct answer choice
        if (isCorrect)
        {
            quizPanel.SetActive(false);
            hideQuestionButton.SetActive(false);
            correctAnswerUI.SetActive(true);

            answerText.text = currentQuestion.correctAnswerText;
            // Play audio for answer
            if (currentQuestion.correctAnswerAudio != null)
            {
                quizAudio.clip = currentQuestion.correctAnswerAudio;
                quizAudio.Play();
            }
        }
        // Incorrect answer choice
        else
        {
            quizPanel.SetActive(false);
            hideQuestionButton.SetActive(false);
            incorrectAnswerUI.SetActive(true);
        }
    }

    public void continueQuiz()
    {
        correctAnswerUI.SetActive(false);
        quizPanel.SetActive(true);
        hideQuestionButton.SetActive(true);

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayCurrentQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    public void tryAgain()
    {
        incorrectAnswerUI.SetActive(false);
        quizPanel.SetActive(true);
        hideQuestionButton.SetActive(true);
        DisplayCurrentQuestion();
    }

    public void EndQuiz()
    {
        quizPanel.SetActive(false);
        quizComplete.SetActive(true);
        Debug.Log("Quiz complete!");
    }

    public void CloseQuiz()
    {
        quizPanelContainer.SetActive(false);
        Debug.Log("Quiz complete!");
    }
    
}
