using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientScript : MonoBehaviour
{
    private Transform t;
    private bool settled;

    private void Start()
    {
        t = GetComponent<Transform>();
        settled = false;
    }

    public IEnumerator GoToBed(GameObject bed)
    {
        t.SetParent(null, true);

        //makes sure the guy is close enough
        while (Vector2.Distance(t.position, bed.transform.position) > 0.1f)
        {
            Debug.Log(Vector2.Distance(t.position, bed.transform.position));
            //move towards bed
            t.position = Vector2.Lerp(t.position, bed.transform.position, Time.deltaTime * 3f);
            //rotate like crazy
            t.Rotate(t.up, 10f);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Made it out!");

        t.SetParent(bed.transform, true);
        t.rotation = Quaternion.Euler(0f, 0f, 0f);
        t.position = bed.transform.position;
        settled = true;
        yield return null;
    }
}
