using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameScript : MonoBehaviour
{
    [SerializeField] public Sprite background;
    [SerializeField] public string instructions;
    public PatientScript my_patient;

    //heal the patient
    public void Win()
    {
        my_patient.isHealed();
    }

    //kill the patient
    public void Lose()
    {
        my_patient.deathAnimation();
    }

    private void OnDestroy()
    {
        my_patient.minigame_active = false;
    }
}
