using UnityEngine;

public class ScoreMaster : MonoBehaviour
{
    private int totalScore = 0; // Puntaje total acumulado

    // Método para actualizar el puntaje total
    public void UpdateScore(int score)
    {
        totalScore += score; // Sumar el puntaje recibido al puntaje total
        Debug.Log("Puntaje total actualizado: " + totalScore);
    }
}