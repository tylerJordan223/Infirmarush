using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class FakeNurse : MonoBehaviour
{
    [SerializeField] GameObject nurse_object;

    private float time_between_nurses;
    private float current_time;
    private float speed;

    private void Start()
    {
        speed = Random.Range(5f, 15f);
        time_between_nurses = 0f;
        current_time = 0f;
        time_between_nurses = Random.Range(1f, 5f);
    }

    private void Update()
    {
        if(current_time > time_between_nurses)
        {
            GameObject temp = Instantiate(nurse_object);
            temp.transform.position = this.transform.position;
            temp.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            temp.gameObject.GetComponent<FakeNurseObject>().speed = this.speed;
            current_time = 0f;
        }

        current_time += Time.deltaTime;
    }
}
