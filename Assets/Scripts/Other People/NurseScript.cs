using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;

public class NurseScript : MonoBehaviour
{
    private Transform t;
    private bool moving;
    public GameObject patient_bed;
    private PatientScript patient;
    private float random_offset;
    private bool has_patient;
    private float speed;
    private float level;
    public int patients_left;
    private GameObject player;


    private void Start()
    {
        t = GetComponent<Transform>();
        moving = true;
        random_offset = Random.Range(-3f,3f);
        patient = t.GetChild(0).gameObject.GetComponent<PatientScript>();
        has_patient = true;
        level = NurseSpawner.level;
        speed = 0f;
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        //scale the speed based on level
        if (level == 0)
        {
            speed = 5f;
        }
        else
        {
            speed = 5f * level;
        }

        //actually move
        if (moving)
        {
            t.position += new Vector3(-speed * Time.deltaTime,0f,0f);
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

        //update sort order for character
        if (this.transform.position.y < player.transform.position.y)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 3;
        }

        if (has_patient)
        {
            patient.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            patient.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 2;
        }
    }
}
