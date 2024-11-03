using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeNurseObject : MonoBehaviour
{
    private Transform t;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        t = this.gameObject.GetComponent<Transform>();
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        t.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        if(Mathf.Abs(t.position.x) > 50)
        {
            Destroy(this.gameObject);
        }
    }
}
