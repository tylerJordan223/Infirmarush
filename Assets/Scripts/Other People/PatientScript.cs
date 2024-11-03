using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PatientScript : MonoBehaviour
{
    private Transform t;
    private bool settled;
    public float time_limit;
    private BedScript my_bed;
    private bool on_player;

    [SerializeField] List<String> deaths;
    private Animator anim;

    private void Start()
    {
        t = GetComponent<Transform>();
        settled = false;
        time_limit = UnityEngine.Random.Range(10f, 20f);
        on_player = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(settled)
        {
            if (my_bed.my_bar.localScale.x == 0)
            {
                //pick a random death
                int temp = UnityEngine.Random.Range(0, deaths.Count);
                anim.SetBool(deaths[temp], true);
            }
        }

        //manage healing/killing
        if(on_player)
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                isHealed();
                
            }
            if(Input.GetKeyDown(KeyCode.K))
            {
                //pick a random death
                int temp = UnityEngine.Random.Range(0,deaths.Count);
                anim.SetBool(deaths[temp], true);
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
        //flip the player if the bed is flipped
        if(my_bed.gameObject.GetComponent<SpriteRenderer>().flipY)
        {
            t.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
        settled = true;
        yield return null;
    }

    public void isHealed()
    {
        NurseSpawner.patients_left--;

        //editing values to reset bed
        my_bed.patient_time_limit = 0;
        my_bed.has_patient = false;
        settled = false;
        StartCoroutine(WalkOut());
    }

    public void isKilled()
    {
        //lose a heart and disable a perfect round
        PlayerScript.health--;
        NurseSpawner.perfect = false;

        //editing values to reset bed
        my_bed.patient_time_limit = 0;
        my_bed.has_patient = false;
        settled = false;
        Destroy(this.gameObject);
    }

    private IEnumerator WalkOut()
    {
        //set parent to nothing
        t.SetParent(null, true);
        //move left
        Vector3 goal_position = new Vector3(t.position.x - 1.5f, t.position.y, 0f);
        while(t.position.x > goal_position.x)
        {
            transform.position += new Vector3(-5 * Time.deltaTime, 0f, 0f);
            yield return null;
        }
        //rotate up
        t.localRotation = Quaternion.Euler(0f, 0f, 0f);
        //move up
        float tempsign = Mathf.Sign(t.position.y);
        while(t.position.y > 1.6f || t.position.y < 1.4f)
        {
            t.position += new Vector3(0f, 5 * Time.deltaTime * -tempsign, 0f);
            yield return null;
        }
        //move out
        while(t.position.x > -10f)
        {
            t.position += new Vector3(-5 * Time.deltaTime, 0f, 0f);
            yield return null;
        }
        //destroy patient
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //makes sure the pateint is in the bed before enabling this
        if (collision.CompareTag("Player") && settled)
        {
            on_player = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //makes sure the pateint is in the bed before disabling this
        if (collision.CompareTag("Player") && settled)
        {
            on_player = false;
        }
    }
}
