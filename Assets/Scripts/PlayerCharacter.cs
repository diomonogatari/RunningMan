using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerCharacter : MonoBehaviour
{

    public float acceleration = 0.0015f;
    public float speed = 5;
    public float leftRightSpeed = 5;
    public int secondsImmune = 3;
    public int maxHP;
    public GameObject hitFX;
    public AudioSource hitSound;

    private int currentHP;
    private Rigidbody rigidBody;
    private Animator animator;

    private ScoreManager scoreManager;

    private bool isImmune = false;

    private float nextTimeKickInmune = 0;

    private float speedMultiply = 0;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        rigidBody.freezeRotation = true;

        currentHP = maxHP;

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vamos a ir incrementando la velocidad a la que tiene que ir en intervalos regulares hasta alcanzar la velocidad que le corresponde
        speedMultiply += acceleration;
        if (speedMultiply > 1)
            speedMultiply = 1;

        float realSpeed = speedMultiply * speed;
        float realLeftRightSpeed = speedMultiply * leftRightSpeed;


        Vector3 movement = Vector3.forward * realSpeed * Time.deltaTime;

        if (Input.GetKey(Constants.Input.left))
        {
            movement += Vector3.left * realLeftRightSpeed * Time.deltaTime;
        }

        if (Input.GetKey(Constants.Input.right))
        {
            movement += Vector3.right * realLeftRightSpeed * Time.deltaTime;
        }

        transform.position += movement;

        if (isImmune)
        {
            if (Time.timeSinceLevelLoad >= nextTimeKickInmune)
            {
                EndImmunity();
            }
        }
    }

    public void StopCharacter()
    {
        speed = 0;
        leftRightSpeed = 0;
        animator.SetTrigger(Constants.AnimationVariables.stopRunningTrigger);
    }

    public void ResetHP()
    {
        if (!isImmune)
        {
            hitFX.SetActive(true);
            hitSound.pitch = Random.Range(0.8f, 1.2f);
            hitSound.Play();
            currentHP--;
            Debug.Log("Current HP " + currentHP);
        }
    }

    public bool IsCharacterDead()
    {
        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void EndImmunity()
    {
        //rigidBody.isKinematic = false;
        isImmune = false;
    }
    public void StartImmunity()
    {
        if (!isImmune)
        {
            //rigidBody.isKinematic = true;
            isImmune = true;
            nextTimeKickInmune = Time.timeSinceLevelLoad + secondsImmune;
        }
    }

    public void BlockHit()
    {
        ResetHP();
        StartImmunity();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.enemy)
        {
            BlockHit();
        }
        if(other.name == Constants.Collectables.coin)
        {
            scoreManager.IncreaseCoin();
            Destroy(other.transform.parent.gameObject);
        }
    }

    public int GetCurrentHp()
    {
        return this.currentHP;
    }

}
