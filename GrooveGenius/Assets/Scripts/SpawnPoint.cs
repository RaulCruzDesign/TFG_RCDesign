using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn; // Prefab que será instanciado
    public float timeToReachZ0 = 5f; // Tiempo en segundos que tarda el prefab en llegar a Z=0

    public IEnumerator Activate()
    {
        yield return MovePrefab();
    }

    public IEnumerator MovePrefab()
    {
        GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        float distanceToZ0 = Mathf.Abs(transform.position.z);
        float moveSpeed = distanceToZ0 / timeToReachZ0;

        while (spawnedPrefab != null && spawnedPrefab.transform.position.z > 0)
        {
            if (spawnedPrefab != null)
            {
                float step = moveSpeed * Time.deltaTime;
                spawnedPrefab.transform.position = Vector3.MoveTowards(spawnedPrefab.transform.position, new Vector3(transform.position.x, transform.position.y, 0f), step);
            }
            yield return null;
        }

        while (spawnedPrefab != null && spawnedPrefab.transform.position.z > -2)
        {
            if (spawnedPrefab != null)
            {
                float step = moveSpeed * Time.deltaTime;
                spawnedPrefab.transform.position = Vector3.MoveTowards(spawnedPrefab.transform.position, new Vector3(transform.position.x, transform.position.y, -2f), step);
            }
            yield return null;
        }

        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
        }
    }
}
