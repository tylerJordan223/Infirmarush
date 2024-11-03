using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NurseSpawner : MonoBehaviour
{
    [SerializeField] GameObject nurse_object;
    [SerializeField] public List<GameObject> beds;

    public static int level;
    public static int patients_left;

    private float time_between_nurses;
    private float current_time;

    public static bool perfect;
    private bool leveling_up;
    private bool patient_pause;

    //UI
    [SerializeField] TextMeshProUGUI level_UI;
    private Animator level_anim;

    private void Start()
    {
        patient_pause = false;
        level_anim = level_UI.gameObject.GetComponent<Animator>();
        time_between_nurses = 0f;
        current_time = 0f;
        level = 0;
        patients_left = 1;
        perfect = true;
        level_UI.text = "Level 0";

        //flags
        leveling_up = false;
    }

    void Update()
    {
        
        //makes sure that there are no patients left and all the beds are empty
        if (patients_left <= 0 && beds.Count == 6 && !leveling_up)
        {
            StartCoroutine(LevelUp());
            leveling_up = true;
        }

        //DEBUG ONLY REMOVE LATER//
        if (Input.GetKeyDown(KeyCode.P) && beds.Count > 0)
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
        //DEBUG ONLY REMOVE LATER//

        if (current_time > time_between_nurses || (beds.Count == 6 && patients_left > 0 && !patient_pause && !leveling_up))
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
                patient_pause = true;
                StartCoroutine(HandlePause());
            }

            //reset the time
            current_time = 0;
            //scale the time between
            int low_time = 25 + (-2 * level);
            int high_time = 30 + (-2 * level);

            //clamp at a certain time
            if (low_time < 5) { low_time = 5; }
            if (high_time < 10) { high_time = 10; }

            //generate random time
            time_between_nurses = Random.Range(low_time, high_time + 1);
        }

        //update the time
        current_time += Time.deltaTime;
    }

    private IEnumerator HandlePause()
    {
        patient_pause = false;
        yield return new WaitForSeconds(3f);
    }

    private IEnumerator LevelUp()
    {
        //never will this animation take over 10 seconds
        current_time = 0;

        level_anim.SetBool("levelingUp", true);
        yield return new WaitForSeconds(1f);
        if(perfect)
        {
            level_UI.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "PERFECT ROUND!";
        }
        level++;
        level_UI.text = "Level " + level;
        patients_left = 3 * level;
        yield return new WaitForSeconds(0.5f);
        level_UI.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        level_anim.SetBool("levelingUp", false);
        //couple of seconds of rest before level starts
        yield return new WaitForSeconds(2f);
        if(PlayerScript.health < 5)
        {
            PlayerScript.health++;
            //add another health if perfect round
            if (perfect)
            {
                PlayerScript.health++;
            }
        }
        perfect = true;
        yield return new WaitForSeconds(3f);
        //spawn the first nurse and stop leveling up
        leveling_up = false;
        time_between_nurses = -1;

        //delete excess minigames
        GameObject minigameParent = GameObject.Find("Game");
        for (int i = minigameParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(minigameParent.transform.GetChild(i).gameObject);
        }

    }
}
