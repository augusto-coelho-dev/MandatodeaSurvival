using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    public float Jumpforce;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("jump");
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
            
        }

    }
}
