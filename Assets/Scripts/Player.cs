using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Player : MonoBehaviour
{
    public float speed = 3f;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov; //Ahora es visible entre los metodos
    CircleCollider2D attackCollider;

    //Variables para tener el contador de la vida//
    
    //Variables para Android//
    private Vector2 touchOrigin = -Vector2.one;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        

        attackCollider =transform.GetChild(0).GetComponent<CircleCollider2D>();
        
       

    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;
        int vertical = 0;
        
            
       //Para Android//
        //Codigo de los gestos tactiles
            if (Input.touchCount > 0)
            {
                
                Touch myTouch = Input.touches[0];
                if (myTouch.phase == TouchPhase.Began)
                {
                    touchOrigin = myTouch.position;
                }
                else if (myTouch.phase == TouchPhase.Ended && touchOrigin != -Vector2.one)
                {
                    Vector2 touchEnd = myTouch.position;
                    float x = touchEnd.x - touchOrigin.x;
                    float y = touchEnd.y - touchOrigin.y;
                    if (x != 0 || y != 0)
                    {
                        if (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            horizontal = x > 0 ? 1 : -1;

                        }
                        else
                        {
                            vertical = y > 0 ? 1 : 1;
                        }
                    }
                    

                }
            }
            else{
                horizontal = (int)Input.GetAxisRaw("Horizontal");
                vertical = (int)Input.GetAxisRaw("Vertical");
                 mov = new Vector2(horizontal, vertical);
                if (horizontal != 0) vertical =0;
            }
        


        if (mov != Vector2.zero){
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("walking", true);
        }
        else{
            anim.SetBool("walking", false);
        }

        //Buscamos el estado actual mirando la informacion del animador
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("PlayerAttack");
        //Detectamos el ataque, tiene prioridad por lo que va abajo del todo
        if (Input.GetKeyDown("space") && !attacking)
        {
            anim.SetTrigger("attacking");
        }
        
        //Vamos actualizando la posicion del vector de ataque
        if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x/2, mov.y/2);
        
        //Activamos el Collider a la mitad de la animacion
        if(attacking)
        {
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > 0.33 && playbackTime < 0.66) 
            {
                attackCollider.enabled = true;
            }
            else 
            {
                attackCollider.enabled = false;
            }

        }
    }
    void FixedUpdate()
    {
        //Nos movemos en el fixed por las fisicas
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
    }

    
}
