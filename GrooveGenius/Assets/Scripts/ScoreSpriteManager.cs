using UnityEngine;
using System.Collections;

public class ScoreSpriteManager : MonoBehaviour
{
    public void DisplaySprite(GameObject sprite)
    {
        StartCoroutine(ShowSprite(sprite));
    }

    private IEnumerator ShowSprite(GameObject sprite)
    {
        sprite.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sprite.SetActive(false);
    }
}

