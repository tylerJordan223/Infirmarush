using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas pause_menu;
    [SerializeField] Canvas game_over;
    private bool pause;
    public static bool can_pause;
    private MinigameParentScript mgp;

    private void Start()
    {
        mgp = GameObject.Find("Minigame").GetComponent<MinigameParentScript>();
        pause_menu.gameObject.SetActive(false);
        game_over.gameObject.SetActive(false);
        pause = false;
        can_pause = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && can_pause)
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

        //game over
        if(PlayerScript.health == 0)
        {
            GameOver();
        }
    }

    public void UnPause()
    {
        pause = false;
    }

    public void GameOver()
    {
        mgp.ExitMinigame();
        game_over.gameObject.SetActive(true);
    }

}
