using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button[] categoryButtons;
    public Button[] levelButtons;
    public Text centralTitle;
    
    private int currentCategoryIndex = 0;
    private int currentLevelIndex = 0;
    private bool inCategoryMenu = true;

    void Start()
    {
        UpdateCategoryMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (inCategoryMenu)
            {
                currentCategoryIndex = (currentCategoryIndex - 1 + categoryButtons.Length) % categoryButtons.Length;
                UpdateCategoryMenu();
            }
            else
            {
                currentLevelIndex = (currentLevelIndex - 1 + levelButtons.Length) % levelButtons.Length;
                UpdateLevelMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (inCategoryMenu)
            {
                currentCategoryIndex = (currentCategoryIndex + 1) % categoryButtons.Length;
                UpdateCategoryMenu();
            }
            else
            {
                currentLevelIndex = (currentLevelIndex + 1) % levelButtons.Length;
                UpdateLevelMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inCategoryMenu)
            {
                inCategoryMenu = false;
                UpdateLevelMenu();
            }
            else
            {
                // Aquí puedes agregar la lógica para cargar el nivel seleccionado
                Debug.Log("Nivel seleccionado: " + levelButtons[currentLevelIndex].name);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inCategoryMenu)
            {
                inCategoryMenu = true;
                UpdateCategoryMenu();
            }
        }
    }

    void UpdateCategoryMenu()
    {
        for (int i = 0; i < categoryButtons.Length; i++)
        {
            if (i == currentCategoryIndex)
            {
                categoryButtons[i].transform.localScale = Vector3.one * 1.2f;
                centralTitle.text = categoryButtons[i].GetComponentInChildren<Text>().text;
            }
            else
            {
                categoryButtons[i].transform.localScale = Vector3.one;
            }
        }

        // Asegurarse de que todos los botones de nivel estén inactivos cuando estemos en el menú de categorías
        foreach (Button button in levelButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    void UpdateLevelMenu()
    {
        centralTitle.text = "Niveles de " + categoryButtons[currentCategoryIndex].GetComponentInChildren<Text>().text;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].gameObject.SetActive(i == currentLevelIndex);
            if (i == currentLevelIndex)
            {
                levelButtons[i].transform.localScale = Vector3.one * 1.2f;
            }
            else
            {
                levelButtons[i].transform.localScale = Vector3.one;
            }
        }
    }
}
