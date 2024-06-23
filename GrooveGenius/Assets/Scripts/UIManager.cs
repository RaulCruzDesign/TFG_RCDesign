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
}
