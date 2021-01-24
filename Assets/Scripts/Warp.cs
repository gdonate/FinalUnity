using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Warp : MonoBehaviour
{
    public GameObject Portal, Player;

    


    void OnTriggerEnter2D( Collider2D collision){
        if (collision.tag == "Player"){
           
           Player.transform.position = Portal.transform.GetChild(0).transform.position;
           
        } 
    }
   
}
