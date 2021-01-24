using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    public float visionRadius;
    public float speed;
    public float attackRadius;
    
    
    [Tooltip("Prefab de la roca")]
    public GameObject rockPrefab;
    [Tooltip("Velocidad de ataque")]
    public float attackSpeed = 2f;
    bool attacking;

//Variables para la vida del enemigo//
    [Tooltip("Puntos de vida")]
    public int maxHP = 3;
    [Tooltip("Vida actual")]
    public int hp;

    GameObject player;

    Animator anim;

    Vector3 initialPosition, target;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        initialPosition = transform.position;
       
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        
        //Iniciamos la vida del enemigo al maximo//
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = initialPosition;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        Vector3 dir = (target - transform.position).normalized;
        
        // Si el Raycast encuentra al jugador lo ponemos de target
       
        if (dist < visionRadius)
        {
            target = player.transform.position;

            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.SetBool("walking", true);
            if (!attacking) StartCoroutine(Attack(attackSpeed));

            
        } 
        
             
        float fixedSpeed = speed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
 
        Debug.DrawLine(transform.position, target, Color.green);

   
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        
    }

    IEnumerator Attack (float seconds)
    {
        attacking = true;
        if (target != initialPosition && rockPrefab != null)
        {
            Instantiate (rockPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds (seconds);
        }
        attacking = false;
    }

    //Función para restar una vida cuando ha sido atacado//
    public void Attacked()
    {
        if (--hp <= 0)
        {
            Destroy(gameObject);
        }
    }
/*
    void OnGUI()
    {
        //Este es el vector donde va estar el enemigo en todo momento//
        Vector2 pos = Camera.main.WorldToScreenPoint (transform.position);

        //Dibujamos un cuadrado debajo del enemigo para ver el hp//
        GUI.Box(
            new Rect(
                pos.x = 350,                 //posicion x de la barra//
                Screen.height - pos.y +40,  //posicion y de la barra//
                30,                         //anchura de la barra//
                20                          //altura de la barra//
            ), hp + "/" + maxHP             //texto de la barra//
        );
    } */
}
