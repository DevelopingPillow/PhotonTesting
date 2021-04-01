using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    public float _speed = 5;
    public bool _toggle = true;

    // Mic Input
    public float sensitivity = 200;
    public float loudness = 0;
    AudioSource _audio;

    //Player stuff
    Rigidbody rb;
    PhotonView PV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }

        _audio = GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        _audio.Play();
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        Movement();

        EmitAudio(4);
        
    }

    // Movement
    void Movement()
    {
        Vector3 movement = new Vector3(0, 0, 0);
        float rotateSpeed = 1;
        if (_toggle)
        {
            rotateSpeed = _speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(0, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector3(0, 0, -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector3(-1, 0, 0);
        }

        transform.Translate(movement.normalized * _speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -40 * rotateSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 40 * rotateSpeed, 0);
        }
    }

    // Audio Behaviour
    void EmitAudio(int scale)
    {
        GameObject sphere = GameObject.Find("Sphere");
        float scaling = GetAverageVolume() + scale;
        /*Debug.Log(GetAverageVolume());*/

        if (scaling == scale)
        {
            sphere.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            sphere.transform.localScale = new Vector3(scaling, scaling, 0.1f);
        }
    }

    // Mic Input
    float GetAverageVolume()
    {
        float[] data = new float[256];
        float a = 0;

        _audio.GetOutputData(data, 0);
        foreach (var s in data)
        {
            a += Mathf.Abs(s);
        }

        float avgVolume = a / 256;

        if (avgVolume > 3)
        {
            return 3;
        }
        else if (avgVolume > 0.001)
        {
            return a;
        }
        else
        {
            return 0;
        }
    }
}
