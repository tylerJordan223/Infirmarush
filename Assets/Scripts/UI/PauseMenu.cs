using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas pause_menu;
    private bool pause;

    private void Start()
    {
        pause_menu.gameObject.SetActive(false);
        pause = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }

        if(pause)
        {
            Time.timeScale = 0f;
            pause_menu.gameObject.SetActive(true);
        }
        else
        {
            pause_menu.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMenu()
    {
        //go to menu
    }

    public void UnPause()
    {
        pause = false;
    }
}
