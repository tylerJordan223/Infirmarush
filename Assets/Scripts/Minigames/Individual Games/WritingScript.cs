using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WritingScript : MonoBehaviour
{
    private bool can_write;
    [SerializeField] Image ink;
    private List<GameObject> inkList;
    [SerializeField] TextMeshProUGUI warning;
    private int can_write_int;
    private bool writing;
    private int ink_count;
    private MinigameScript my_minigame;

    void Start()
    {
        inkList = new List<GameObject>();
        warning.text = "";
        my_minigame = this.gameObject.GetComponent<MinigameScript>();
        can_write = false;
        can_write_int = 0;
        writing = false;
        ink_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        List<RaycastResult> r = GetEventSystemRaycastResults();
        foreach(RaycastResult rr in r)
        {
            if(rr.gameObject.CompareTag("script"))
            {
                can_write_int++;
            }
        }

        if(can_write_int > 0)
        {
            can_write = true;
        }
        else
        {
            can_write = false;
        }
        can_write_int = 0;

        if (can_write && !writing)
        {
            if(Input.GetMouseButtonDown(0))
            {
                writing = true;
                StartCoroutine(Writing());
            }
        }
    }

    private IEnumerator Writing()
    {
        
        if(writing)
        {
            while (writing && can_write)
            {
                if (Input.GetMouseButton(0))
                {                
                    GameObject go = Instantiate(ink).gameObject;
                    go.transform.SetParent(this.transform.Find("Signature"));
                    go.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
                    inkList.Add(go);
                    ink_count++;
                }
                else
                {
                    writing = false;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        if(inkList.Count > 0)
        {
            StartCoroutine(CheckSignature());
        }
    }

    private IEnumerator CheckSignature()
    {
        if(ink_count > 50 && ink_count < 100)
        {
            my_minigame.EndMinigame(0);
        }

        if(ink_count < 50)
        {
            for(int i = inkList.Count-1; i >= 0; i--)
            {
                GameObject ink = inkList[i];
                inkList.Remove(ink);
                Destroy(ink);
            }
            ink_count = 0;
            warning.text = "not detailed enough!";
            yield return new WaitForSeconds(1f);
            warning.text = "";
            writing = false;
            can_write = true;
        }

        if(ink_count > 100)
        {
            ink_count = 0;
            for (int i = inkList.Count - 1; i >= 0; i--)
            {
                GameObject ink = inkList[i];
                inkList.Remove(ink);
                Destroy(ink);
            }
            warning.text = "too detailed!";
            yield return new WaitForSeconds(1f);
            warning.text = "";
            writing = false;
            can_write = true;
        }
    }

    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];

            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("Signature"))
                return true;
        }

        return false;
    }

    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        return raysastResults;
    }
}
