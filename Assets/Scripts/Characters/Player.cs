using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rBody;
    private Animator anim;
    private float horizontal;

    [SerializeField]private float speed = 4;
    [SerializeField]private float jumpForce = 10;

    [SerializeField]bool isGrounded;
    
    [SerializeField]Transform groundSensor;
    [SerializeField]float sensorRadius;
    [SerializeField]LayerMask sensorLayer;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        //el sprite del rogue rota izquierda/derecha
        if(horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("isRunning", true);
        }
        else if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("isRunning", true);
        }
        else if(horizontal == 0)
        {
            anim.SetBool("isRunning", false);
        }
            
        isGrounded = Physics2D.OverlapCircle(groundSensor.position, sensorRadius, sensorLayer);

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
    }

    void FixedUpdate() 
    {
        rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
        //3 pq es la layer de ground en el inspector
        if(coll.gameObject.layer == 3)
        {
            anim.SetBool("isJumping", false);
        }
    }
}
