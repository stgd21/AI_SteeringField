using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToWorld : MonoBehaviour
{
    public GameObject flowerRef;
    private void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 60.5f;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = pos;
        transform.position = new 
            Vector3(transform.position.x, flowerRef.transform.position.y, transform.position.z);
    }
}
