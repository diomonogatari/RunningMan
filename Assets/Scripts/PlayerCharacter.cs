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
    public float jumpForce;
    public float cameraTurningRate = 0.8f;
    public float REALSPEED = 0;

    private int currentHP;
    private Rigidbody rigidBody;
    private Animator animator;

    private bool isGrounded = true;
    private ScoreManager scoreManager;
    private GameStateManager gsm;
    private bool isAccelerationLimited = true;

    private bool isImmune = false;

    private bool isCameraAllowedToRotate = false;
    private Vector3 cameraDesiredRotation;
    private int cameraDesiredFov = 100;

    private float nextTimeKickInmune = 0;

    private float speedMultiply = 0;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        rigidBody.freezeRotation = true;

        cameraDesiredRotation = new Vector3(5, 0, 0);

        currentHP = maxHP;

        scoreManager = FindObjectOfType<ScoreManager>();
        gsm = FindObjectOfType<GameStateManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vamos a ir incrementando la velocidad a la que tiene que ir en intervalos regulares hasta alcanzar la velocidad que le corresponde
        speedMultiply += acceleration;
        if (speedMultiply > 1 && isAccelerationLimited)
            speedMultiply = 1;
        if (speedMultiply > 2.5f)
            speedMultiply = 2.5f;

        float realSpeed = speedMultiply * speed;
        float realLeftRightSpeed = speedMultiply * leftRightSpeed;
        REALSPEED = realSpeed;

        Vector3 movement = Vector3.forward * realSpeed * Time.deltaTime;

        if (Input.GetButton(Constants.Input.horizontal))
        {
            movement += new Vector3(Input.GetAxis(Constants.Input.horizontal.Normalize()), 0, 0) * realLeftRightSpeed * Time.deltaTime;
        }
        if (Input.GetButton(Constants.Input.jump) && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        transform.position += movement;

        if (isImmune)
        {
            if (Time.timeSinceLevelLoad >= nextTimeKickInmune)
            {
                EndImmunity();
            }
        }

        CheckIfRotateCamera();
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
        if (other.tag.Equals(Constants.Tags.enemy))
        {
            BlockHit();
        }
        if (other.name.Equals(Constants.Collectables.coin))
        {
            other.enabled = false; //disable the collider to not interfere
            var child = other.gameObject.transform.GetChild(0);

            Destroy(child.gameObject);//destroy the child as it only contains the mesh and material

            scoreManager.IncreaseCoin(); //add score

            var coinSound = other.gameObject.GetComponent<AudioSource>();
            coinSound.Play(); //play the sound

            Destroy(other.transform.parent.gameObject, 3);
        }
        if (other.tag.Equals(Constants.Tags.newPhase))
        {
            StartNewPhase();
        }
        if (other.tag.Equals(Constants.Tags.newPhase))
        {
            StartNewPhase();
        }
        if (other.tag.Equals(Constants.Tags.finish))
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        //detach cam
        //call GameStateManager.EndTheGame()
        gsm.EndTheGame();
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void StartNewPhase()
    {   // start the rotation
        isCameraAllowedToRotate = true;

        //no speed limiter
        isAccelerationLimited = false;
        //gotta go fast
    }

    #region CameraRotations
    void RotateCamera()
    {
        if (!Camera.main.transform.eulerAngles.Equals(this.cameraDesiredRotation) && !Camera.main.fieldOfView.Equals(cameraDesiredFov)) //if fov and angle isn't what we want, rotate
        {
            //Camera.main.transform.eulerAngles = new Vector3(5, 0, 0);
            Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.eulerAngles, cameraDesiredRotation, cameraTurningRate * Time.fixedDeltaTime);
            //camera fov increase
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, this.cameraDesiredFov, cameraTurningRate * Time.fixedDeltaTime);
        }
        else
        {
            isCameraAllowedToRotate = false; //desired rotation achieved
        }

    }
    void CheckIfRotateCamera()
    {
        if (this.isCameraAllowedToRotate)//if camera is allowed means that the trigger was reached and the camera is not at the desired position
        {
            RotateCamera();
        }
    }
    #endregion

    public int GetCurrentHp()
    {
        return this.currentHP;
    }

}
