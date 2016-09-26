using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{

    bool isPaused = false;


    public void pauseToggle()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            Debug.Log("Paused");
            //call other menu elements here
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("!Paused");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseToggle();
        }
    }
}
