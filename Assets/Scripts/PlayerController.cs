using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    private Vector3 startPosition;

    public float jumpForce = 5.0f;
    public float runningSpeed = 6.0f;
    public float downForce = 6.0f;

    private float TimeNivel;

    public LayerMask groundMask;

    public static PlayerController sharedControllerInstance;

    //Frame n°0
    private void Awake()
    {
        if (sharedControllerInstance == null) {
            sharedControllerInstance = this;
        }
    }



    //Frame n°1
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    public void StartGame() {

        //TODO: Poner animaciones de muerte en ofF 

        Invoke("RestartPosition", 0.1f);
    }

    void RestartPosition() {
        this.transform.position = startPosition;
        playerRigidbody.velocity = Vector2.zero;
        TimeNivel = 0f;
        runningSpeed = 10f;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Input.GetAxis("Vertical") < 0) {
                Down();
            }

            AvanzarTiempo();
            AumentarVelocidad();
        }


        Debug.Log("Velocidad: " +runningSpeed);
        Debug.Log("Tiempo: " + TimeNivel);
        Debug.DrawRay(this.transform.position, Vector2.down * 0.7f, Color.red);
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            playerRigidbody.gravityScale = 1;
            if (playerRigidbody.velocity.x < runningSpeed)
            {
                playerRigidbody.velocity = new Vector2(runningSpeed, playerRigidbody.velocity.y);
            }
        }
        else {//SI NO ESTAMOS INGAME. DESACTIVAMOS LA VELOCIDAD Y LA GRAVEDAD
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.gravityScale = 0;
        }
        
    }

    void Down() {
        if (IsTouchingTheGround() == false) {
            playerRigidbody.AddForce(Vector2.down * downForce, ForceMode2D.Impulse);
        }
    }
    void Jump() {
        if (IsTouchingTheGround()) {
            playerRigidbody.AddForce(Vector2.up * jumpForce);
        }

    }

    bool IsTouchingTheGround() {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.7f, groundMask))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            return true;
        }
        else {
            return false;
        }
    }
    void AumentarVelocidadMejorado() { 
        
    }

    void AumentarVelocidad() {
        if (TimeNivel >= 0 && TimeNivel < 10f) {
            runningSpeed = 10f;
        } else if (TimeNivel > 10f && TimeNivel < 15f) {
            runningSpeed = 14f;
        } else if (TimeNivel > 15f && TimeNivel < 25f) {
            runningSpeed = 19f;
        }
    }

    void AvanzarTiempo() {
        TimeNivel = TimeNivel + Time.deltaTime;
    }

    public void Die() {
        //PROGRAMAR ANIMACION DE MUERTE
        //playerAnimator.SetBool(STATE_ALIVE, false);

        GameManager.sharedInstance.GameOver();
    }

}
