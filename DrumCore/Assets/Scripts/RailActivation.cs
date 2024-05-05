using UnityEngine;
using UnityEngine.InputSystem;

public class RailActivation : MonoBehaviour
{
    public GameObject selectedPrefab; // Prefab seleccionado como propiedad
    public RailActivation[] railActivations; // Lista de scripts RailActivation asociados a cada carril

    [System.Serializable]
    public class ScoreRange
    {
        public float minDistance; // Distancia mínima desde el prefab hasta Z=0
        public float maxDistance; // Distancia máxima desde el prefab hasta Z=0
        public int score; // Puntaje asociado
    }

    public ScoreRange[] scoreRanges; // Rangos de distancia y puntajes

    // Método para manejar el evento de Player Input "HitHat_Close_Activation"
     int totalScore = 0; // Variable para acumular el puntaje
    public void OnActivation()
    {
       
        Debug.Log("Hit");
        // Actualizar el prefab seleccionado más cercano
        UpdateSelectedPrefab();

        // Realizar el input y calcular la puntuación
        PerformInputAndCalculateScore();
    }

    // Método para actualizar el prefab seleccionado
    private void UpdateSelectedPrefab()
    {
        GameObject[] allPrefabs = GameObject.FindGameObjectsWithTag("Prefab");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject prefab in allPrefabs)
        {
            float distance = Mathf.Abs(prefab.transform.position.z);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                selectedPrefab = prefab;
            }
        }
    }

    // Método para realizar un input, calcular la distancia de tiempo y otorgar una puntuación
    public void PerformInputAndCalculateScore()
    {
        float closestDistance = Mathf.Abs(selectedPrefab.transform.position.z);
        int score = CalculateScore(closestDistance);
        Debug.Log("Puntuación: " + score);

        if (score > 0)
        {
            Destroy(selectedPrefab);
        }
    }

    // Método para calcular la puntuación según la distancia al plano Z=0
   private int CalculateScore(float distance)
{
   

    foreach (ScoreRange range in scoreRanges)
    {
        if (distance >= range.minDistance && distance <= range.maxDistance)
        {
            totalScore += range.score; // Sumar el puntaje de este rango al total
        }
    }

    return totalScore; // Devolver el puntaje total acumulado
}
}

