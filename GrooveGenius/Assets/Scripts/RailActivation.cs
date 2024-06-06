using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RailActivation : MonoBehaviour
{
    public List<GameObject> selectedPrefabs; // Lista de prefabs seleccionados
    public ScoreMaster scoreMaster; // Referencia al script ScoreMaster
    public bool isAutoMode; // Modo automático activado

    [System.Serializable]
    public class ScoreRange
    {
        public float minDistance; // Distancia mínima desde el prefab hasta Z=0
        public float maxDistance; // Distancia máxima desde el prefab hasta Z=0
        public int score; // Puntaje asociado
    }

    public ScoreRange[] scoreRanges; // Rangos de distancia y puntajes

    void Awake()
    {
        if (selectedPrefabs == null)
        {
            selectedPrefabs = new List<GameObject>();
        }

        if (scoreMaster == null)
        {
            scoreMaster = FindObjectOfType<ScoreMaster>();
        }
    }

    // Método para manejar el evento de Player Input "HitHat_Close_Activation"
    public void OnActivation()
    {
        if (!isAutoMode)
        {
            Debug.Log("Hit");
            // Actualizar los prefabs seleccionados más cercanos
            UpdateSelectedPrefabs();

            // Realizar el input y calcular la puntuación
            PerformInputAndCalculateScore();
        }
    }

    // Método para actualizar los prefabs seleccionados
    private void UpdateSelectedPrefabs()
    {
        selectedPrefabs.Clear(); // Limpiar la lista de prefabs seleccionados
        GameObject[] allPrefabs = GameObject.FindGameObjectsWithTag("Prefab");
        float closestDistance = Mathf.Infinity;

        List<GameObject> closestPrefabs = new List<GameObject>();

        foreach (GameObject prefab in allPrefabs)
        {
            float distance = Mathf.Abs(prefab.transform.position.z);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPrefabs.Clear(); // Limpiar la lista de prefabs seleccionados anteriores
                closestPrefabs.Add(prefab); // Agregar el nuevo prefab más cercano
            }
            else if (distance == closestDistance)
            {
                closestPrefabs.Add(prefab); // Agregar otro prefab con la misma distancia
            }
        }

        selectedPrefabs = closestPrefabs;
    }

    // Método para realizar un input, calcular la distancia de tiempo y otorgar una puntuación
    public void PerformInputAndCalculateScore()
    {
        int totalScore = 0; // Reiniciar el puntaje total

        foreach (GameObject selectedPrefab in selectedPrefabs)
        {
            float closestDistance = Mathf.Abs(selectedPrefab.transform.position.z);
            int score = CalculateScore(closestDistance);
            Debug.Log("Puntuación: " + score);

            if (score > 0)
            {
                Destroy(selectedPrefab);
            }

            totalScore += score; // Acumular el puntaje
        }

        // Enviar el puntaje total al script ScoreMaster
        scoreMaster.UpdateScore(totalScore);
    }

    // Método para calcular la puntuación según la distancia al plano Z=0
    private int CalculateScore(float distance)
    {
        foreach (ScoreRange range in scoreRanges)
        {
            if (distance >= range.minDistance && distance <= range.maxDistance)
            {
                return range.score; // Devolver el puntaje de este rango
            }
        }

        return 0; // Devolver 0 si no se encuentra ningún rango válido
    }

    // Método para activar automáticamente los prefabs en el modo automático
    public void AutoActivatePrefab(GameObject prefab)
    {
        float distance = Mathf.Abs(prefab.transform.position.z);
        int score = CalculateScore(distance);
        Debug.Log("Puntuación automática: " + score);

        if (score > 0)
        {
            Destroy(prefab);
        }

        scoreMaster.UpdateScore(score);
    }
}