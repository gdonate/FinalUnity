using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento en unidades")]
    public float speed;

    GameObject player; //Recuperamos objeto jugador
    Rigidbody2D rb2d;
    Vector3 target, dir; //Vectores para almacenar el objetivo y la direccion
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();

        if(player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;
        }
    }

    void FixedUpdate ()
    {
        //Si hay un objetivo movemos la roca hacia su posicion
        if (target != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Si choca contra el jugador o un ataque lo borramos
        if (col.transform.tag == "Player" || col.transform.tag == "Attack")
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        //Esto es por si sale de la pantalla se borra
        Destroy(gameObject);
    }

   
}
