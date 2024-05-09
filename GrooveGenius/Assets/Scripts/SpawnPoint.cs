using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn; // Prefab que será instanciado
    public float activationTime = 0f; // Tiempo de activación personalizado
    public float timeToReachZ0 = 5f; // Tiempo en segundos que tarda el prefab en llegar a Z=0

    // Cambia el tipo de retorno de void a IEnumerator
    public IEnumerator Activate()
    {
        // Método para activar el spawn point
        // Llama al método MovePrefab y espera a que termine
        yield return MovePrefab();
    }

    public IEnumerator MovePrefab()
{
    // Instancia el prefab en la posición del SpawnPoint
    GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

    // Calcula la distancia entre el punto de spawn y el punto Z "0"
    float distanceToZ0 = Mathf.Abs(transform.position.z);

    // Calcula la velocidad de movimiento necesaria para llegar a Z "0" en el tiempo especificado
    float moveSpeed = distanceToZ0 / timeToReachZ0;

    // Realiza el movimiento hacia la posición final (Z=0)
    while (spawnedPrefab != null && spawnedPrefab.transform.position.z > 0)
    {
        if (spawnedPrefab != null)
        {
            float step = moveSpeed * Time.deltaTime;
            spawnedPrefab.transform.position = Vector3.MoveTowards(spawnedPrefab.transform.position, new Vector3(transform.position.x, transform.position.y, 0f), step);
        }
        yield return null;
    }

    // Realiza el movimiento hacia la posición final (Z=-2)
    while (spawnedPrefab != null && spawnedPrefab.transform.position.z > -2)
    {
        if (spawnedPrefab != null)
        {
            float step = moveSpeed * Time.deltaTime;
            spawnedPrefab.transform.position = Vector3.MoveTowards(spawnedPrefab.transform.position, new Vector3(transform.position.x, transform.position.y, -2f), step);
        }
        yield return null;
    }

    // Verifica si el prefab sigue siendo válido antes de destruirlo
    if (spawnedPrefab != null)
    {
        Destroy(spawnedPrefab);
    }
}
}