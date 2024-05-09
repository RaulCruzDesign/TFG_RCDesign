using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronomo : MonoBehaviour
{
    public float bpm = 120f; // BPM inicial
    public AudioClip metronomeSound; // Sonido del metrónomo
    public float volume = 1.0f; // Volumen inicial
    public float metronomeDelay = 0f; // Retraso entre el inicio del metrónomo y el inicio del beat section
    private AudioSource audioSource;
    private float nextBeatTime;
    private float beatTime;
    private SpawnController spawnController;

    void Start()
    {
        // Asegúrate de que haya un AudioSource adjunto a este objeto
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = metronomeSound;
        audioSource.volume = volume;
        spawnController = FindObjectOfType<SpawnController>(); // Encontrar el SpawnController en la escena

        // Calcular el tiempo de un beat en segundos
        beatTime = 60f / bpm;

        // Obtener el tiempo del primer beat section con el retraso aplicado
        if (spawnController != null && spawnController.beatSections.Count > 0)
        {
            float firstSectionTime = beatTime / spawnController.beatSections[0].sectionsPerBeat;
            nextBeatTime = Time.time + firstSectionTime + metronomeDelay; // Iniciar el tiempo del próximo pulso con el retraso
        }
        else
        {
            nextBeatTime = Time.time + metronomeDelay; // Si no hay beat sections, iniciar el tiempo de inmediato con el retraso
        }
    }

    void Update()
    {
        if (Time.time >= nextBeatTime)
        {
            // Reproducir el sonido del metrónomo
            audioSource.Play();

            // Calcular el tiempo del próximo pulso basado en el tiempo del último pulso
            nextBeatTime += beatTime;
        }
    }

    // Método para actualizar el volumen del metrónomo
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        audioSource.volume = volume;
    }
}
