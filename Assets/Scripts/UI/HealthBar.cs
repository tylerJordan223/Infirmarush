using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //player for the sake of health
    private PlayerScript player;
    private float health;

    //images of heart
    [SerializeField] Sprite full_heart;

    //actual health objects
    private List<Image> hearts;
    [SerializeField] Image first_heart;

    private void Start()
    {
        //make the list and get the first heart
        hearts = new List<Image>();
    }

    private void Update()
    {
        //update to get the player's health
        health = PlayerScript.health;

        //adding new hearts
        for (int i = 0; i < health; i++)
        {
            if (i > hearts.Count - 1)
            {
                //create a new heart
                GameObject new_heart_go = Instantiate(first_heart).gameObject;
                new_heart_go.SetActive(true);
                //rename it properly
                new_heart_go.name = "heart" + (i + 1).ToString();
                //parent it properly
                new_heart_go.transform.SetParent(this.gameObject.transform, false);
                //set the image
                Image new_heart = new_heart_go.GetComponent<Image>();
                //add to the list
                hearts.Add(new_heart);
                Material new_mat = new Material(new_heart.GetComponent<Image>().material);
                new_heart.GetComponent<Image>().material = new_mat;
                StartCoroutine(FadeIn(hearts[i].gameObject));
            }
        }

        
        //removing hearts
        for(int j = hearts.Count-1; j >= 0; j--)
        {
            if(j+1 > health)
            {
                StartCoroutine(FadeOut(hearts[j].gameObject));
                hearts.Remove(hearts[j]);
            }
        }

        //loop to update the heart visuals
        for (int f = 0; f < hearts.Count; f++)
        {
            //if the heart is below or equal to health its full
            hearts[f].sprite = full_heart;
        }
    }

    private IEnumerator FadeOut(GameObject h)
    {
        Material m = h.GetComponent<Image>().material;
        float fade = 1f;
        while(fade > 0f)
        {
            fade -= Time.deltaTime;
            m.SetFloat("_Fade", fade);
            yield return null;
        }
        Destroy(h);
    }

    private IEnumerator FadeIn(GameObject h)
    {
        Material m = h.GetComponent<Image>().material;
        float fade = 0f;
        while (fade < 1f)
        {
            fade += Time.deltaTime;
            m.SetFloat("_Fade", fade);
            yield return null;
        }
    }
}
