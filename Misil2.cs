using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misil2 : MonoBehaviour
{
    public float Velocidad = 10;
    private Rigidbody2D rigidbody;
    private MegaManController megaManController2;

    public void SetMegaman2(MegaManController MegaManController2)
    {
        megaManController2 = MegaManController2;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        Destroy(this.gameObject, 5);
    }

    void Update()
    {
        rigidbody.velocity = new Vector2(Velocidad, rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D Other)
    {
        var tag = Other.gameObject.tag;

    }
    private void OnTriggerEnter2D(Collider2D Other)
    {
        var tag = Other.gameObject.tag;
        if (tag == "Enemigo")
        {
            Destroy(this.gameObject);
            Destroy(Other.gameObject);
            megaManController2.Incrementar_Puntaje(10);
        }

    }
}
