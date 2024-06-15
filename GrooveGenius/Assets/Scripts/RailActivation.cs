using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RailActivation : MonoBehaviour
{
    public string prefabTag1; // Tag del primer tipo de prefab
    public string prefabTag2; // Tag del segundo tipo de prefab
    public AudioClip sound1; // Sonido correspondiente al prefab 1
    public AudioClip sound2; // Sonido correspondiente al prefab 2
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

    public void OnActivation1()
    {
        if (!isAutoMode)
        {
            Debug.Log("Hit 1");
            GameObject selectedPrefab1 = FindClosestPrefab(prefabTag1);

            if (selectedPrefab1 != null)
            {
                PerformInputAndCalculateScore(selectedPrefab1, sound1);
            }
        }
    }

    public void OnActivation2()
    {
        if (!isAutoMode)
        {
            Debug.Log("Hit 2");
            GameObject selectedPrefab2 = FindClosestPrefab(prefabTag2);

            if (selectedPrefab2 != null)
            {
                PerformInputAndCalculateScore(selectedPrefab2, sound2);
            }
        }
    }

    private GameObject FindClosestPrefab(string prefabTag)
    {
        GameObject[] allPrefabs = GameObject.FindGameObjectsWithTag(prefabTag);
        float closestDistance = Mathf.Infinity;
        GameObject closestPrefab = null;

        foreach (GameObject prefab in allPrefabs)
        {
            float distance = Mathf.Abs(prefab.transform.position.z);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPrefab = prefab;
            }
        }

        return closestPrefab;
    }

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

    public void AutoActivatePrefab(GameObject prefab)
    {
        if (prefab.CompareTag(prefabTag1))
        {
            PerformInputAndCalculateScore(prefab, sound1);
        }
        else if (prefab.CompareTag(prefabTag2))
        {
            PerformInputAndCalculateScore(prefab, sound2);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is null!");
            return;
        }

        audioSource.PlayOneShot(clip);
        Debug.Log("Playing sound: " + clip.name);
    }
}
