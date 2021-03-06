using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLarva : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public float speed;

    public Transform rightCol;
    public Transform leftCol;
    public Transform headPoint;

    public LayerMask layer;

    public int Score;

    private bool colliding;
    public CapsuleCollider2D capsuleColider2D;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed,rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed = -speed;
        }      
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float height = col.contacts[0].point.y - headPoint.position.y;
            
            if (height > 0)
            {
                speed = 0;
                GameController.instance.TotalScore += Score;
                GameController.instance.UpdateScoreText();
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                anim.SetTrigger("die");
                capsuleColider2D.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.15f);
            }else
            {
               GameController.instance.deathCollision = true;
            }
        }
    }


}
