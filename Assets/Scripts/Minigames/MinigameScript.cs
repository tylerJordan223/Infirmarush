using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScript : MonoBehaviour
{
    [SerializeField] public string instructions;
    public PatientScript my_patient;
    private MinigameParentScript parent;

    [SerializeField] Image checkmark;
    [SerializeField] Image big_x;

    private void Start()
    {
        checkmark.enabled = false;
        big_x.enabled = false;
        parent = my_patient.minigame_parent;
    }

    public void EndMinigame(int condition)
    {
        my_patient.canDie = false;
        StartCoroutine(WinOrLose(condition));
    }

    private IEnumerator WinOrLose(int condition)
    {
        if (condition == 0)
        {
            checkmark.enabled = true;
            yield return new WaitForSeconds(1.5f);
            checkmark.enabled = false;
            parent.ExitMinigame();
            yield return new WaitForSeconds(0.5f);
            my_patient.isHealed();
        }
        else if (condition == 1)
        {
            big_x.enabled = true;
            yield return new WaitForSeconds(1.5f);
            big_x.enabled = false;
            parent.ExitMinigame();
            yield return new WaitForSeconds(0.5f);
            my_patient.deathAnimation();
        }
    }

    private void OnDestroy()
    {
        my_patient.minigame_active = false;
    }
}
