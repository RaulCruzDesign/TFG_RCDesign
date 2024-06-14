using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RailActivation : MonoBehaviour
{
    public GameObject selectedPrefab1; // Primer prefab seleccionado
    public GameObject selectedPrefab2; // Segundo prefab seleccionado
    public AudioClip sound1; // Sonido correspondiente al primer prefab
    public AudioClip sound2; // Sonido correspondiente al segundo prefab
    public ScoreMaster scoreMaster; // Referencia al script ScoreMaster
    public bool isAutoMode; // Modo automático activado
    public AudioSource audioSource; // Fuente de audio para reproducir los sonidos

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
        if (scoreMaster == null)
        {
            scoreMaster = FindObjectOfType<ScoreMaster>();
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Método para manejar el evento de Player Input "HitHat_Close_Activation" para el primer prefab
    public void OnActivation1()
    {
        if (!isAutoMode)
        {
            Debug.Log("Hit 1");
            UpdateSelectedPrefabs();

            if (selectedPrefab1 != null)
            {
                PerformInputAndCalculateScore(selectedPrefab1, sound1);
            }
        }
    }

    // Método para manejar el evento de Player Input "HitHat_Close_Activation" para el segundo prefab
    public void OnActivation2()
    {
        if (!isAutoMode)
        {
            Debug.Log("Hit 2");
            UpdateSelectedPrefabs();

            if (selectedPrefab2 != null)
            {
                PerformInputAndCalculateScore(selectedPrefab2, sound2);
            }
        }
    }

    // Método para actualizar los prefabs seleccionados
    private void UpdateSelectedPrefabs()
    {
        GameObject[] allPrefabs = GameObject.FindGameObjectsWithTag("Prefab");
        float closestDistance1 = Mathf.Infinity;
        float closestDistance2 = Mathf.Infinity;

        selectedPrefab1 = null;
        selectedPrefab2 = null;

        foreach (GameObject prefab in allPrefabs)
        {
            float distance = Mathf.Abs(prefab.transform.position.z);

            if (distance < closestDistance1)
            {
                closestDistance2 = closestDistance1;
                selectedPrefab2 = selectedPrefab1;

                closestDistance1 = distance;
                selectedPrefab1 = prefab;
            }
            else if (distance < closestDistance2)
            {
                closestDistance2 = distance;
                selectedPrefab2 = prefab;
            }
        }
    }

    // Método para realizar un input, calcular la distancia de tiempo y otorgar una puntuación
    public void PerformInputAndCalculateScore(GameObject selectedPrefab, AudioClip sound)
    {
        float closestDistance = Mathf.Abs(selectedPrefab.transform.position.z);
        int score = CalculateScore(closestDistance);
        Debug.Log("Puntuación: " + score);

        if (score > 0)
        {
            PlaySound(sound);
            Destroy(selectedPrefab);
        }

        // Enviar el puntaje al script ScoreMaster
        scoreMaster.UpdateScore(score);
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
            if (prefab == selectedPrefab1)
            {
                PlaySound(sound1);
            }
            else if (prefab == selectedPrefab2)
            {
                PlaySound(sound2);
            }

            Destroy(prefab);
        }

        scoreMaster.UpdateScore(score);
    }

    // Método para reproducir un sonido
    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
