using UnityEngine;

public class ScoreMaster : MonoBehaviour
{
    private int totalScore = 0; // Puntaje total acumulado

    public void UpdateScore(int score)
    {
        totalScore += score;
        Debug.Log("Puntaje total actualizado: " + totalScore);
    }
}
