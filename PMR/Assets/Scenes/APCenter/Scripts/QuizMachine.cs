using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizMachine : MonoBehaviour
{
    private int currentQuestionIndex = 0;
    private QuizQuestion currentQuestion;
    private List<QuizQuestion> questions = new List<QuizQuestion>();
    private Text text;
    private bool questionCanCurrentlyBeAnswered = false;
    public GameObject elevatorControllerObject;
    private ElevatorControllerXR controller;
    void Start()
    {
        if(elevatorControllerObject == null)
        {
            Debug.LogError("elevator controller object is not referenced");
        }
        else
        {
            controller = elevatorControllerObject.GetComponent<ElevatorControllerXR>();
            if(controller == null)
            {
                Debug.LogError("elevator controller script is not referenced");
            }
        }
        text = GetComponentInChildren<Text>();
        if(text == null)
        {
            Debug.LogError("can't find UI text element on screen");
        }
        ClearScreen();
        InitQuestions();
        //LoadNextQuestion();        
    }
    public void LoadNextQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            currentQuestion = questions[currentQuestionIndex];
            currentQuestionIndex++;
            RenderQuestion(currentQuestion);
        }
        else
        {
            ClearScreen();
        }
    }
    public void ButtonPushed(Answer answer)
    {
        if(questionCanCurrentlyBeAnswered)
        {
            if (currentQuestion.CheckAnswer(answer))
            {
                Debug.Log("Correct!");
            }
            else
            {
                Debug.Log("Wrong!");
            }
            ClearScreen();
            if(controller != null)
            {
                controller.UnlockControls();
            }
        }
    }
    private void RenderQuestion(QuizQuestion question)
    {
        questionCanCurrentlyBeAnswered = true;
        if (text != null)
        {
            string textToRender = "";
            textToRender += question.Question;
            textToRender += "\n";
            textToRender += "\n";
            textToRender += "A: ";
            textToRender += question.AnswerA;
            textToRender += "\n";
            textToRender += "\n";
            textToRender += "B: ";
            textToRender += question.AnswerB;
            textToRender += "\n";
            textToRender += "\n";
            textToRender += "C: ";
            textToRender += question.AnswerC;
            text.text = textToRender;
        }
    }
    private void ClearScreen()
    {
        questionCanCurrentlyBeAnswered = false;
        if(text != null)
        {
            text.text = "";
        }
    }
    private void InitQuestions()
    {
        questions.Add(new QuizQuestion(
            "Wat denk je dat er gebeurt als UV-stralen je huidcellen beschadigen?",
            "Ze verdwijnen vanzelf.",
            "Ze kunnen herstellen, maar niet altijd.",
            "Ze veranderen in andere cellen, zoals spiercellen.",
            Answer.B
            ));
        questions.Add(new QuizQuestion(
            "Wat vind je in deze laag van je huid?",
            "Alleen huidcellen.",
            "Haarvaten, zenuwen en bloedvaten.",
            "Botten en spieren.",
            Answer.B
            ));
        questions.Add(new QuizQuestion(
            "Waarom is deze laag extra gevaarlijk als er kankercellen komen?",
            "Omdat kankercellen zich hier kunnen verspreiden via het bloed.",
            "Omdat deze laag geen bescherming heeft.",
            "Omdat deze laag geen belangrijke functie heeft.",
            Answer.A
            ));
        questions.Add(new QuizQuestion(
            "Wat kun je doen om deze laag van je huid te beschermen tegen UV-stralen?",
            "Zonnecrème gebruiken.",
            "Een dikke jas dragen.",
            "Niets, want deze laag is sterk genoeg.",
            Answer.A
            ));
    }
}
