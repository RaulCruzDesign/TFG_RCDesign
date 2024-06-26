using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    public float bpm = 120f; // BPM de la música
    public float division = 4; // BPM de la música
    public List<SpawnPoint> spawnPoints; // Lista de spawn points
    public TextAsset notesFile; // Archivo de notas asignado desde el editor

    private List<int[]> noteDataList = new List<int[]>(); // Lista de datos de notas
    private float secondsPerBeat; // Segundos por beat
    private float secondsPerSubdivision; // Segundos por subdivisión de beat

    void Start()
    {
        // Calcular los segundos por beat basado en el BPM
        secondsPerBeat = 60f / bpm;
        // Calcular los segundos por subdivisión de beat (16 subdivisiones por beat)
        secondsPerSubdivision = secondsPerBeat / 16f;

        // Inicializar la lista de spawn points
        if (spawnPoints == null)
        {
            Debug.LogWarning("Lista de spawn points no asignada. Asigne manualmente los spawn points en el inspector.");
            return;
        }

        LoadNotes();
        StartCoroutine(PlayNotesInLoop());
    }

    void LoadNotes()
    {
        if (notesFile == null || string.IsNullOrEmpty(notesFile.text))
        {
            Debug.LogWarning("No se ha asignado ningún archivo de notas o el archivo está vacío.");
            return;
        }

        string[] lines = notesFile.text.Split('\n');

        foreach (string line in lines)
        {
            string[] parts = line.Trim().Split(',');

            List<int> spawnPointIndices = new List<int>();
            foreach (string part in parts)
            {
                int spawnPointIndex;
                if (int.TryParse(part, out spawnPointIndex))
                {
                    spawnPointIndices.Add(spawnPointIndex);
                }
                else
                {
                    Debug.LogWarning($"Error al analizar el índice de spawn point en la línea: {line}");
                }
            }

            noteDataList.Add(spawnPointIndices.ToArray());
        }
    }

    IEnumerator PlayNotesInLoop()
    {
        while (true)
        {
            foreach (int[] spawnPointIndices in noteDataList)
            {
                foreach (int spawnPointIndex in spawnPointIndices)
                {
                    if (spawnPointIndex < 0 || spawnPointIndex >= spawnPoints.Count)
                    {
                        Debug.LogWarning($"Índice de spawn point fuera de rango: {spawnPointIndex}");
                        continue;
                    }

                    SpawnPoint spawnPoint = spawnPoints[spawnPointIndex];
                    StartCoroutine(ActivateSpawnPoint(spawnPoint));
                }

                // Espera la mitad del tiempo entre beats para las secciones de 16 notas
                yield return new WaitForSeconds(secondsPerBeat / division);
            }
        }
    }

    IEnumerator ActivateSpawnPoint(SpawnPoint spawnPoint)
    {
        yield return spawnPoint.Activate();
    }
}

