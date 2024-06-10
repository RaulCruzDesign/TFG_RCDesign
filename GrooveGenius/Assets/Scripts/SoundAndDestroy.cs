using UnityEngine;

public class SoundAndDestroy : MonoBehaviour
{
   /* public AudioClip sound; // Clip de sonido para este prefab
    public int points = 10; // Puntos a agregar cuando se activa
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Activate()
    {
        // Reproducir el sonido
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }

        // Agregar puntos al ScoreMaster
        ScoreMaster.Instance.AddPoints(points);

        // Destruir el objeto después de que el sonido termine de reproducirse
        Destroy(gameObject, sound != null ? sound.length : 0);
    }*/
}
