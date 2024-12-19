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
    public AudioSource AudioSourceCorrect;
    public AudioSource AudioSourceWrong;
    public AudioSource AudioSourceQuestions;
    public AudioClip[] audioclipsQuestions;
    private int currentAudioClipIndex = 0;
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
        if(AudioSourceCorrect == null)
        {
            Debug.LogError("audio source correct is not referenced");
        }
        if (AudioSourceWrong == null)
        {
            Debug.LogError("audio source wrong is not referenced");
        }
        if(audioclipsQuestions == null)
        {
            Debug.LogError("audio source questions is not referenced");
        }
        ClearScreen();
        InitQuestions();
        //LoadNextQuestion();        
    }
    public void LoadNextQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            if(currentAudioClipIndex + 1 <= audioclipsQuestions.Length)
            {
                AudioSourceQuestions.clip = audioclipsQuestions[currentQuestionIndex];
            }
            AudioSourceQuestions.Play();
            currentQuestion = questions[currentQuestionIndex];
            currentQuestionIndex++;
            currentAudioClipIndex++;
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
            if(AudioSourceQuestions.isPlaying && AudioSourceQuestions != null)
            {
                AudioSourceQuestions.Stop();
            }
            if (currentQuestion.CheckAnswer(answer))
            {
                Debug.Log("Correct!");
                if(AudioSourceCorrect != null)
                {
                    AudioSourceCorrect.Play();
                }
                //ClearScreen();
                SetAdvanceAfetQuestionExplanationText();
                if (controller != null)
                {
                    controller.UnlockControls();
                }
            }
            else
            {
                Debug.Log("Wrong!");
                if(AudioSourceWrong != null)
                {
                    AudioSourceWrong.Play();
                }
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
    public void ClearScreen()
    {
        questionCanCurrentlyBeAnswered = false;
        if(text != null)
        {
            text.text = "";
        }
    }
    private void SetAdvanceAfetQuestionExplanationText()
    {
        questionCanCurrentlyBeAnswered = false;
        if(text != null)
        {
            string resultText = "";
            resultText += "Juist!";
            resultText += "\n\n";
            resultText += "Druk op de liftknop om verder te gaan.";
            text.text = resultText;
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
        /*
        questions.Add(new QuizQuestion(
            "Wat kun je doen om deze laag van je huid te beschermen tegen UV-stralen?",
            "Zonnecrème gebruiken.",
            "Een dikke jas dragen.",
            "Niets, want deze laag is sterk genoeg.",
            Answer.A
            ));
        */
    }
}
