using UnityEngine;

public class ScoreMaster : MonoBehaviour
{
    private int totalScore = 0;
    private int comboMultiplier = 1;
    private int maxCombo = 0;
    private int currentCombo = 0;

    private int perfectCount = 0;
    private int greatCount = 0;
    private int goodCount = 0;
    private int failCount = 0;

    public UIManager uiManager;

    void Start()
    {
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        uiManager.UpdateTotalScore(totalScore);
    }

    public void UpdateCombo(bool success, int scoreRangeIndex)
    {
        if (success)
        {
            currentCombo++;
            if (currentCombo > maxCombo)
            {
                maxCombo = currentCombo;
                uiManager.UpdateMaxCombo(maxCombo);
            }
            uiManager.UpdateCombo(currentCombo);

            switch (scoreRangeIndex)
            {
                case 0:
                    perfectCount++;
                    break;
                case 1:
                    greatCount++;
                    break;
                case 2:
                    goodCount++;
                    break;
                default:
                    break;
            }
        }
        else
        {
            failCount++;
            currentCombo = 0;
            uiManager.UpdateCombo(currentCombo);
        }

        uiManager.UpdateNoteCounts(perfectCount, greatCount, goodCount, failCount);
    }
}
