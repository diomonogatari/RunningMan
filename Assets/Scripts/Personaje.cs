using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour {

    public float aceleracion = 0.0015f;
    public float speed = 5;
    public float leftRightSpeed = 5;
    public int segundosInmune = 3;
    public int vidaMaxima;
    public GameObject hitFX;
    public AudioSource hitSound;

    private int vidaActual;
    private Rigidbody rigidBody;
    private Animator animator;

    private bool isInmune = false;

    private float nextTimeKickInmune = 0;

    private float speedMultiply = 0;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        
        rigidBody.freezeRotation = true;

        vidaActual =  vidaMaxima;


	}

    // Update is called once per frame
    void Update()
    {
        //Vamos a ir incrementando la velocidad a la que tiene que ir en intervalos regulares hasta alcanzar la velocidad que le corresponde
        speedMultiply += aceleracion;
        if (speedMultiply > 1) speedMultiply = 1;

        float realSpeed = speedMultiply * speed;
        float realLeftRightSpeed = speedMultiply * leftRightSpeed;


        Vector3 movimiento = Vector3.forward * realSpeed * Time.deltaTime;

        if (Input.GetKey("left"))
        {
            movimiento += Vector3.left * realLeftRightSpeed * Time.deltaTime;
        }

        if (Input.GetKey("right"))
        {
            movimiento += Vector3.right * realLeftRightSpeed * Time.deltaTime;
        }

        transform.position += movimiento;

        if (isInmune)
        {
            if (Time.timeSinceLevelLoad >= nextTimeKickInmune)
            {
                AcabarInmunidad();
            }
        }

    }

    public void DetenerPersonaje()
    {
        speed = 0;
        leftRightSpeed = 0;
        animator.SetTrigger("Detener");
    }

    public void RestarVida()
    {
        if (!isInmune)
        {
            hitFX.SetActive(true);
            hitSound.pitch = Random.Range(0.8f, 1.2f);
            hitSound.Play();
            vidaActual--;
            Debug.Log("Vida actual " + vidaActual);
        }
    }

    public bool EstaMuerto()
    {
        if (vidaActual <= 0) return true;
        else return false;
    }

    public void AcabarInmunidad()
    {
        //rigidBody.isKinematic = false;
        isInmune = false;
    }
    public void EmpezarInmunidad()
    {
        if (!isInmune)
        {
            //rigidBody.isKinematic = true;
            isInmune = true;
            nextTimeKickInmune = Time.timeSinceLevelLoad + segundosInmune;
        }
    }

    public void BloqueGolpeado()
    {
        RestarVida();
        EmpezarInmunidad();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            BloqueGolpeado();
        }
    }
}
