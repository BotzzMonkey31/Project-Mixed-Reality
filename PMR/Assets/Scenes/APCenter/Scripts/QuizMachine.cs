using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using UnityEngine;

public class QuizMachine : MonoBehaviour
{
    private List<QuizQuestion> questions = new List<QuizQuestion>();
    void Start()
    {
        InitQuestions();
        foreach (QuizQuestion question in questions)
        {
            Debug.Log(question.ToString());
        }
    }

    void Update()
    {
        
    }
    private void InitQuestions()
    {
        questions.Add(new QuizQuestion(
            "Wat denk je dat er gebeurt als UV-stralen je huidcellen beschadigen?",
            "Ze verdwijnen vanzelf.",
            "Ze kunnen herstellen, maar niet altijd",
            "Ze veranderen in andere cellen, zoals spiercellen",
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
            "Een dikke jas draagen.",
            "Niets, want deze laag is sterk genoeg.",
            Answer.A
            ));
    }
}
