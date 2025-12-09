using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //  public float fuerzaSalto, velocidad;
    public float velocidad;
    public string escena;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Animator anim;
    private bool tieneLlave = false, tieneHerramienta = false;

    void Start()
    {
        // inicio con animación por defecto (idle)
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // añadir efectos de sonido
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // movimiento
        moverPj();
        // salir
        Salir();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // cambiar de animación al aterrizar
        if (collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "EnMovimiento")
        {
            // cambiar a animación a aterrizar (+ paso de vuelta a idle si no se mueve)
            anim.SetBool("estaSaltando", false);
            if (!Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
            {
                anim.SetBool("estaMoviendose", false);
                audioSource.Play();
            }
        }

        // recoger llave al entrar en contacto con ella
        if (collision.gameObject.tag == "Llave")
        {
            tieneLlave = true;
            collision.gameObject.SetActive(false);
            audioSource.Play();
        }

        // recoger herramienta al entrar en contacto con ella
        if (collision.gameObject.tag == "Herramienta")
        {
            tieneHerramienta = true;
            collision.gameObject.SetActive(false);
            audioSource.Play();
        }

        // pasar a siguiente nivel/escena al pasar por la puerta
        if (collision.gameObject.tag == "Puerta" && tieneLlave == true)
        {
            audioSource.Play();
            cambioEscena(escena);
        }

        // eliminar obstáculo al entrar en contacto si se tiene la herramienta
        if (collision.gameObject.tag == "Obstaculo" && tieneHerramienta == true)
        {
            collision.gameObject.SetActive(false);
        }

        // ajustar velocidad a plataforma en movimiento (evitar caída)
        if (collision.gameObject.tag == "EnMovimiento")
        {
            transform.parent = collision.transform;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // recuperar movimiento original al salir de la plataforma en movimiento
        if (collision.gameObject.tag == "EnMovimiento")
        {
            transform.parent = null;
        }

        // contar estar en el aire como saltar aunque no se pulse la tecla de salto
        if (collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "EnMovimiento")
        {
            // cambiar animación (salto)
            anim.SetBool("estaSaltando", true);
        }
    }

    void moverPj()
    {
        anim.SetBool("estaMoviendose", false);
        /*
        // saltar al pulsar flecha hacia arriba
        if (Keyboard.current.upArrowKey.isPressed)
        {
            // saltar + cambiar animación (salto)
            anim.SetBool("estaSaltando", true);
            rb.AddForce(new Vector2(0, fuerzaSalto));
        }
        */

        // ir hacia la derecha al pulsar tecla de flecha
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            // movimiento hacia derecha + cambiar animación (moverse hacia el lado)
            anim.SetBool("estaMoviendose", true);
            rb.AddForce(new Vector2(velocidad, 0));
        }

        // ir hacia la izquierda al pulsar tecla de flecha
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            // movimiento hacia izquierda + cambiar animación (moverse hacia el lado)
            anim.SetBool("estaMoviendose", true);
            rb.AddForce(new Vector2(-velocidad, 0));
        }

    }

    // cambio de escena
    public void cambioEscena(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    // salir del juego al pulsar escape
    public void Salir()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }    

}
