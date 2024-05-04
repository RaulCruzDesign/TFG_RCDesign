using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    public float bpm = 120f; // BPM de la música
    public List<SpawnPoint> spawnPoints; // Lista de spawn points

    // Estructura para mapear SpawnPoints a secciones en un beat
    [System.Serializable]
    public class BeatSection
    {
        public List<int> spawnPointIndices = new List<int>(); // Índices de los SpawnPoints en esta sección
        public int sectionsPerBeat = 4; // Número de secciones por beat
    }

    public List<BeatSection> beatSections = new List<BeatSection>(); // Lista de secciones por beat

    void Start()
    {
        // Activar los spawn points en los tiempos especificados
        StartCoroutine(ActivateSpawnPoints());
    }

    IEnumerator ActivateSpawnPoints()
    {
        // Calcular el tiempo de un beat en segundos
        float beatTime = 60f / bpm;

        // Bucle infinito para reproducir las secciones de beats
        while (true)
        {
            // Recorrer la lista de secciones por beat
            foreach (BeatSection beatSection in beatSections)
            {
                // Lista para almacenar las corrutinas de activación de las notas simultáneas
                List<Coroutine> noteCoroutines = new List<Coroutine>();

                // Recorrer los SpawnPoints en esta sección
                foreach (int spawnPointIndex in beatSection.spawnPointIndices)
                {
                    // Verificar si el índice del SpawnPoint está dentro del rango
                    if (spawnPointIndex < 0 || spawnPointIndex >= spawnPoints.Count)
                    {
                        Debug.LogWarning("El índice del SpawnPoint está fuera de rango.");
                        continue;
                    }

                    // Obtener el SpawnPoint correspondiente
                    SpawnPoint spawnPoint = spawnPoints[spawnPointIndex];

                    // Llamar al método Activate en el SpawnPoint
                    Coroutine coroutine = StartCoroutine(spawnPoint.Activate());

                    // Agregar la corrutina a la lista
                    noteCoroutines.Add(coroutine);
                }

                // Esperar la duración de cada sección de beat
                float sectionTime = beatTime / beatSection.sectionsPerBeat;
                yield return new WaitForSeconds(sectionTime);
            }
        }
    }
}
