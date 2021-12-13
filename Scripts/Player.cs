using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rig;
    public bool isJumping;
    public bool doubleJump;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    private GameObject LastCheckPoint;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     Move(); 
     Jump();
     Attack();  
     DeathCollision();
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }  
        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }      
    }
    void Jump(){
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse); 
                doubleJump = true;
                anim.SetBool("jump", true);
            }else{
                if (doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse); 
                    doubleJump = false; 
                    anim.SetBool("doublejump", true);
                }
            }
           
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            LastCheckPoint = collision.gameObject;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
            anim.SetBool("doublejump", false);
        }
        if(collision.gameObject.layer == 10){
            GameController.instance.deathCollision = true;
        }
    }

    void DeathCollision(){
        if (GameController.instance.deathCollision == true)
        {
            anim.SetTrigger("die");
            rig.AddForce(new Vector2(0f, 5), ForceMode2D.Impulse);
            transform.position = LastCheckPoint.transform.position;
            GameController.instance.UpdateHpImage();
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }        
    }

    void Attack(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("attack");
            Collider2D[] hitEnimes = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnimes){
                Debug.Log("We hit" + enemy.name);

            }
        }
        
    }
}
