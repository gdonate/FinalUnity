using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col)
    {
        //Con esta funcion restamos uno de vida si es un enemigo//
        if (col.tag == "Enemy")
        {
            col.SendMessage("Attacked");
        }
    }
}
