using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinigameScript : MonoBehaviour
{
    [SerializeField] public string instructions;
    public PatientScript my_patient;
    private MinigameParentScript parent;

    private void Start()
    {
        parent = my_patient.minigame_parent;
    }

    public void EndMinigame(int condition)
    {
        parent.ExitMinigame();
        if (condition == 0)
        {
            my_patient.isHealed();
        }else if(condition == 1)
        {
            my_patient.deathAnimation();
        }
    }

    private void OnDestroy()
    {
        my_patient.minigame_active = false;
    }
}
