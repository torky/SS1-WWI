using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public CanvasGroup option;
    public CanvasGroup menu;
    public void goToScene(int i)
    {
        if (i <= 0)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(i);
        }
    }

    public void goToOptions()
    {

        this.GetComponent<CanvasGroup>().alpha = 0;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        option.alpha = 1f;
        option.blocksRaycasts = true;

    }

    public void Options(string optionSelection)
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;

        menu.alpha = 1f;
        menu.blocksRaycasts = true;
    }

}
