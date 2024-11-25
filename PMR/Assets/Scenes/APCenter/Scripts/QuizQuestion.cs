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
        Question = "";
        AnswerA = "";
        AnswerB = "";
        AnswerC = "";
        RightAnswer = Answer.None;
    }
    public QuizQuestion(string question, string answerA, string answerB, string answerC, Answer answer)
    {
        Question = question;
        AnswerA = answerA;
        AnswerB = answerB;
        AnswerC = answerC;
        RightAnswer = answer;
    }
    public bool CheckAnswer(Answer answer)
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
    public override string ToString()
    {
        return $"Question: {Question}\n" +
           $"A: {AnswerA}\n" +
           $"B: {AnswerB}\n" +
           $"C: {AnswerC}\n" +
           $"Correct Answer: {RightAnswer}";
    }
}
public enum Answer
{
    None,
    A,
    B,
    C
}