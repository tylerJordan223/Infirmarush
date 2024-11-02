using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientScript : MonoBehaviour
{
    private Transform t;
    private bool settled;
    public float time_limit;
    private BedScript my_bed;

    private void Start()
    {
        
        t = GetComponent<Transform>();
        settled = false;
        time_limit = UnityEngine.Random.Range(10f, 20f);
    }

    private void Update()
    {
        if(settled)
        {
            if (my_bed.my_bar.localScale.x == 0)
            {
                my_bed.has_patient = false;
                Destroy(this.gameObject);
            }
        }
    }

    public IEnumerator GoToBed(GameObject bed)
    {
        t.SetParent(null, true);

        //makes sure the guy is close enough
        while (Vector2.Distance(t.position, bed.transform.position) > 0.1f)
        {
            //move towards bed
            t.position = Vector2.Lerp(t.position, bed.transform.position, Time.deltaTime * 3f);
            //rotate like crazy
            t.Rotate(t.up, 10f);
            yield return new WaitForEndOfFrame();
        }

        t.SetParent(bed.transform, true);
        my_bed = bed.gameObject.GetComponent<BedScript>();
        t.rotation = Quaternion.Euler(0f, 0f, 0f);
        t.position = bed.transform.position;
        bed.GetComponent<BedScript>().patient_time_limit = time_limit;
        settled = true;
        yield return null;
    }
}
