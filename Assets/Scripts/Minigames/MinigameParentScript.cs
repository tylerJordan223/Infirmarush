using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameParentScript : MonoBehaviour
{
    [SerializeField] public Slider s;
    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] public Image background;
    public GameObject minigame;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ExitMinigame()
    {
        anim.SetBool("gaming", false);
        PauseMenu.can_pause = true;
    }

    public void DestroyMinigame()
    {
        Destroy(minigame.gameObject);
    }

    public void EnterMinigame()
    {
        anim.SetBool("gaming", true);
    }
}
