using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public bool has_patient;
    public Transform my_bar;
    public float patient_time_limit;
    private float time_spent;

    private void Start()
    {
        has_patient = false;
        my_bar = this.transform.GetChild(0);
        my_bar.localScale = new Vector3(0f, my_bar.localScale.y, my_bar.localScale.z);
        patient_time_limit = 0f;
        time_spent = 0f;
    }

    private void Update()
    {
        if(time_spent < patient_time_limit)
        {
            my_bar.localScale = new Vector3((-1+(time_spent/patient_time_limit)),my_bar.localScale.y,0f);
            time_spent += Time.deltaTime;
        }
        else
        {
            patient_time_limit = 0f;
            time_spent = 0f;
        }
    }
}
