using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0.2f, 0.0f, -15f); //distancia que debe perseguir al personaje

    public float dampingTime = 0.3f;

    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        //los frames que debe ir el juego 
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition() {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth) {

        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);

        if (smooth)
        {

            //EL SMOOTHDAMP CAMBIA EL VALOR DE UN VECTOR DE MANERA GRADUAL, NO SERÁ DE GOLPE 
            this.transform.position = Vector3.SmoothDamp(this.transform.position, destination
                , ref velocity, dampingTime);
        }
        else {
            //ACÁ SI SERÁ DE GOLPE 
            this.transform.position = destination;
        }
    }
}
