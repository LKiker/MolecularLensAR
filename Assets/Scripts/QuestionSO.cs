using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea] public string questionText;
    [TextArea] public string correctAnswerText;
    public string[] answers;
    public int correctAnswerIndex;
}
