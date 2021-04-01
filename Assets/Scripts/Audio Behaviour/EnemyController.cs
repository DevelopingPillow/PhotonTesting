using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*Vector3 player;*/
    float speed = 2;
    bool staying = false;
    float chaseTime;
    bool isChasing;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        chaseTime = 10f;
        isChasing = false;
        player = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        Chase();
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Debug.Log("entered");*/
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        staying = true;
    }

    private void OnTriggerExit(Collider other)
    {
        /*Debug.Log("exit");*/
        staying = false;
    }

    void Chase()
    {
        if (staying)
        {
            isChasing = true;
            chaseTime = 10f;
        }
        else if (isChasing == true)
        {
            if (chaseTime > 0)
            {
                Vector3 dir = (player.position - gameObject.transform.position).normalized;
                transform.Translate(dir * Time.deltaTime * speed);
                chaseTime -= Time.deltaTime;
            }
            else
            {
                isChasing = false;
            }
        }
        else
        {
            isChasing = false;
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
