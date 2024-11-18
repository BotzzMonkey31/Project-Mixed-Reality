using UnityEngine;

public class QuizQuestion
{
    public string Question { get; set; } = "";
    public string AnswerA { get; set; } = "";
    public string AnswerB { get; set; } = "";
    public string AnswerC { get; set; } = "";
    public Answer RightAnswer { get; set; }
    public QuizQuestion()
    {
        QuizQuestion questionEmpty = new QuizQuestion();
        questionEmpty.Question = "";
        questionEmpty.AnswerA = "";
        questionEmpty.AnswerB = "";
        questionEmpty.AnswerC = "";
        questionEmpty.RightAnswer = Answer.None;
    }
    public QuizQuestion(string question, string answerA, string answerB, string answerC, Answer answer)
    {
        QuizQuestion questionConstructed = new QuizQuestion();
        questionConstructed.Question = question;
        questionConstructed.AnswerA = answerA;
        questionConstructed.AnswerB = answerB;
        questionConstructed.AnswerC = answerC;
        questionConstructed.RightAnswer = answer;
    }
    public bool checkAnswer(Answer answer)
    {
        if(answer == RightAnswer && RightAnswer != Answer.None)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
public enum Answer
{
    None,
    A,
    B,
    C
}