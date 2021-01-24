using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    Vector3 vel = Vector3.zero;
    public float Tiemsua = .15f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 Pospla = Player.position;
        Pospla.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, Pospla, ref vel, Tiemsua);
    }
   
}
