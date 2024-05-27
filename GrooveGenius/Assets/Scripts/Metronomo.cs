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

    void Start()
    {
        // Asegúrate de que haya un AudioSource adjunto a este objeto
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = metronomeSound;
        audioSource.volume = volume;

        // Calcular el tiempo de un beat en segundos
        beatTime = 60f / bpm;

        // Obtener el tiempo del primer beat con el retraso aplicado
        nextBeatTime = Time.time + beatTime + metronomeDelay; // Iniciar el tiempo del próximo pulso con el retraso
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
