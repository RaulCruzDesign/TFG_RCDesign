using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class RailActivation : MonoBehaviour
{
    public string prefabTag1;
    public string prefabTag2;
    public AudioClip sound1;
    public AudioClip sound2;
    public ScoreMaster scoreMaster;
    public bool isAutoMode;
    public AudioSource audioSource;
    public GameObject[] scoreSprites;
    public GameObject failSprite;

    private int comboMultiplier = 1;

    [System.Serializable]
    public class ScoreRange
    {
        public float minDistance;
        public float maxDistance;
        public int score;
        public int spriteIndex;
    }

    public ScoreRange[] scoreRanges;

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
        int scoreRangeIndex = GetScoreRangeIndex(closestDistance);
        Debug.Log("Puntuación: " + score);

        if (score > 0)
        {
            PlaySound(sound);
            Destroy(selectedPrefab);
            score *= comboMultiplier;
            comboMultiplier++;
            scoreMaster.UpdateCombo(true, scoreRangeIndex);
        }
        else
        {
            comboMultiplier = 1;
            ShowFailSprite();
            scoreMaster.UpdateCombo(false, scoreRangeIndex);
        }

        scoreMaster.UpdateScore(score);
        ShowScoreSprite(closestDistance);
    }

    private int CalculateScore(float distance)
    {
        foreach (ScoreRange range in scoreRanges)
        {
            if (distance >= range.minDistance && distance <= range.maxDistance)
            {
                return range.score;
            }
        }

        return 0;
    }

    private int GetScoreRangeIndex(float distance)
    {
        for (int i = 0; i < scoreRanges.Length; i++)
        {
            if (distance >= scoreRanges[i].minDistance && distance <= scoreRanges[i].maxDistance)
            {
                return i;
            }
        }

        return -1; // Indica un fallo
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

    private void ShowScoreSprite(float distance)
    {
        foreach (ScoreRange range in scoreRanges)
        {
            if (distance >= range.minDistance && distance <= range.maxDistance)
            {
                if (range.spriteIndex >= 0 && range.spriteIndex < scoreSprites.Length)
                {
                    StartCoroutine(DisplaySprite(scoreSprites[range.spriteIndex]));
                }
                break;
            }
        }
    }

    private void ShowFailSprite()
    {
        if (failSprite != null)
        {
            StartCoroutine(DisplaySprite(failSprite));
        }
    }

    private IEnumerator DisplaySprite(GameObject sprite)
    {
        sprite.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sprite.SetActive(false);
    }

    public void ResetCombo()
    {
        comboMultiplier = 1;
        ShowFailSprite();
        scoreMaster.UpdateCombo(false, -1);
    }
}
