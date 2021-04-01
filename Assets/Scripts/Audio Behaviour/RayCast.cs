using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{

    int totalRays = 30;

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = 0;
        for (int i=0; i < totalRays; i++) {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / totalRays;

            

            /*Vector3 dir = new Vector3(transform.position.x * x, transform.position.y * y, 0);

            RaycastHit hit;*/
            /*Debug.DrawLine(transform.position, dir, Color.red);*/
            /*if (Physics.Raycast(transform.position, dir, out hit))*/
                /*print(dir);*/
        }
    }
}
