using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformfall : MonoBehaviour
{

    public float Falldelay = 1f;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", Falldelay);
        }
    }
    void Fall()
    {
        rb2d.isKinematic = false;
    }

}