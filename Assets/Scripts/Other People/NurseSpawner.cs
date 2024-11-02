using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NurseSpawner : MonoBehaviour
{
    [SerializeField] GameObject nurse_object;
    [SerializeField] public List<GameObject> beds;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(beds.Count > 0)
            {
                GameObject temp = Instantiate(nurse_object);
                temp.transform.position = this.transform.position;
                temp.transform.rotation = Quaternion.Euler(0f, 0f, -90f);

                //get and set the patient bed, removing it from available beds

                int b = Random.Range(0, beds.Count);
                temp.GetComponent<NurseScript>().patient_bed = beds[b];
                beds[b].GetComponent<BedScript>().has_patient = true;
                beds.Remove(beds[b]);
            }
        }
    }
}
