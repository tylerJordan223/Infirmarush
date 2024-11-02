using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public bool has_patient;
    public Transform my_bar;
    public float patient_time_limit;
    private float time_spent;
    private Color my_bar_color;
    private NurseSpawner n;

    private void Start()
    {
        has_patient = false;
        my_bar = this.transform.GetChild(0);
        my_bar.localScale = new Vector3(0f, my_bar.localScale.y, my_bar.localScale.z);
        patient_time_limit = 0f;
        time_spent = 0f;
        n = GameObject.Find("NurseSpawner").GetComponent<NurseSpawner>();
    }

    private void Update()
    {
        if(time_spent < patient_time_limit)
        {
            my_bar.localScale = new Vector3((-2+(time_spent/patient_time_limit)*2),my_bar.localScale.y,0f);
            my_bar.GetComponent<SpriteRenderer>().color = new Color((255f * (time_spent / patient_time_limit)) / 255f, (255f - (255f * (time_spent / patient_time_limit))) / 255f, 0f,255f);
            time_spent += Time.deltaTime;
        }
        else
        {
            my_bar.localScale = new Vector3(0f, my_bar.localScale.y, my_bar.localScale.z);
            patient_time_limit = 0f;
            time_spent = 0f;
            my_bar.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
            if(!n.beds.Contains(this.gameObject) && !has_patient)
            {
                n.beds.Add(this.gameObject);
            }
        }
    }
}
