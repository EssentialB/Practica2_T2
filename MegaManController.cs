using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MegaManController : MonoBehaviour
{
    public float JumpForce = 10;
    public float Velocity = 10;
    public GameObject Misil;
    public GameObject Misil2;
    public Text PuntajeTexto;
    private int puntaje = 0;

    private SpriteRenderer spriteRenderer; 
    private Rigidbody2D rigidbody; 
    private Animator animator;

    private static readonly string Animator_State = "Estado";
    private static readonly int Animation_Jump = 2;
    private static readonly int Animation_Run = 1;
    private static readonly int Animation_Idle = 0;
    private static readonly int Animation_Hit_Run = 3;
    private static readonly int Rigth = 1;
    private static readonly int Left = -1;
    private static readonly int Up = 1;
    private static readonly int Down = -1;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var velocidadActualX = rigidbody.velocity.x;
        var velocidadActualY = rigidbody.velocity.y;

        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        Change_Animation(Animation_Idle);

        PuntajeTexto.text = "Puntaje: " + puntaje;

        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            Desplazarse(Rigth);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            Desplazarse(Left);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Disparar();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Disparar2();
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            Correr_Disparar();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            Change_Animation(Animation_Jump);
        }
    }
    public void Incrementar_Puntaje(int puntos)
    {
        puntaje += puntos;
    }
    private void Disparar()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var MisilGo = Instantiate(Misil, new Vector3(x, y), Quaternion.identity);
        var Controller = MisilGo.GetComponent<Misil>();
        Controller.SetMegaman(this);
        if (spriteRenderer.flipX)
        {

            Controller.Velocidad = Controller.Velocidad * -1;
        }

    }
    private void Disparar2()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var MisilGo = Instantiate(Misil2, new Vector3(x, y), Quaternion.identity);
        var Controller = MisilGo.GetComponent<Misil2>();
        Controller.SetMegaman2(this);
        if (spriteRenderer.flipX)
        {

            Controller.Velocidad = Controller.Velocidad * -1;
        }

    }
    private void Correr_Disparar()
    {
        Change_Animation(Animation_Hit_Run);
    }
    private void Desplazarse(int Position)
    {
        rigidbody.velocity = new Vector2(Velocity * Position, rigidbody.velocity.y); // x , y // +1 -1 derecha - izquierda // +1 -1 arriba-abajo
        spriteRenderer.flipX = Position == Left;
        Change_Animation(Animation_Run);
    }
    private void Change_Animation(int Animation)
    {
        animator.SetInteger(Animator_State, Animation);
    }
}
