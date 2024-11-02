using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class NurseScript : MonoBehaviour
{
    private Transform t;
    private bool moving;
    public GameObject patient_bed;
    private PatientScript patient;
    private float random_offset;
    private bool has_patient;

    private void Start()
    {
        t = GetComponent<Transform>();
        moving = true;
        random_offset = Random.Range(-5f,5f);
        patient = t.GetChild(0).gameObject.GetComponent<PatientScript>();
        has_patient = true;
    }

    private void Update()
    {

        if (moving)
        {
            t.position += new Vector3(-0.1f,0f,0f);
        }
        
        //destroy the object if its offscreen
        if(t.position.x < -10f)
        {
            Destroy(this.gameObject);
        }

        //removes the patient when its in the right place
        if(t.position.x < (patient_bed.transform.position.x + random_offset))
        {
            if(has_patient)
            {
                has_patient = false;
                patient.StartCoroutine(patient.GoToBed(patient_bed));
            }
        }
    }
}
