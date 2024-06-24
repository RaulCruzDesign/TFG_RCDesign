using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI maxComboText;
    public TextMeshProUGUI perfectCountText;
    public TextMeshProUGUI greatCountText;
    public TextMeshProUGUI goodCountText;
    public TextMeshProUGUI failCountText;
    public TextMeshProUGUI timerText; // Campo de texto para el temporizador

    private float startTime;

    void Start()
    {
        InitializeTextFields();
        startTime = Time.time;
    }

    void Update()
    {
        UpdateTimer();
    }

    private void InitializeTextFields()
    {
        UpdateTotalScore(0);
        UpdateCombo(0);
        UpdateMaxCombo(0);
        UpdateNoteCounts(0, 0, 0, 0);
    }

    public void UpdateTotalScore(int score)
    {
        totalScoreText.text = "Puntuación Total: " + score;
    }

    public void UpdateCombo(int combo)
    {
        comboText.text = "Combo: " + combo;
    }

    public void UpdateMaxCombo(int maxCombo)
    {
        maxComboText.text = "Combo Máximo: " + maxCombo;
    }

    public void UpdateNoteCounts(int perfect, int great, int good, int fail)
    {
        perfectCountText.text = "Perfectos: " + perfect;
        greatCountText.text = "Geniales: " + great;
        goodCountText.text = "Buenos: " + good;
        failCountText.text = "Fallos: " + fail;
    }

    public void UpdateTimer()
    {
        float currentTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
