using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubo : MonoBehaviour
{
    [SerializeField] float velocity;

    Rigidbody rigCubo;

    [SerializeField]
    LayerMask layermask;


    moviment moviment = new moviment();
    raycast raycast = new raycast();

    void Start()
    {
        rigCubo = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        moviment.moveForward(rigCubo, velocity);
        moviment.moveBackward(rigCubo, velocity);
        moviment.moveRight(rigCubo, velocity); 
        moviment.moveLeft(rigCubo, velocity);


        if (raycast.Ray(transform.position, -transform.up, 1, layermask))
        {
            Debug.Log("to no chao");
        }
    }



}
