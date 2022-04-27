using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator ani;

    private Transform tcamera;

    float velocity, gabiVelocity = 2;
    float horizontal, vertical, rayDistanceDown = 10;

    Vector3 v3;
    Quaternion q1, q2;
    RaycastHit hit;

    [SerializeField]
    LayerMask layerMask;

    Vector3 playerPosition;

    void Awake()
    {
        ani = GetComponent<Animator>();
        tcamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        v3 = tcamera.forward * vertical +
                    tcamera.right * horizontal;

        v3.y = 0;

        velocity = v3.normalized.magnitude;

        if (Input.GetButton("Fire3"))
        {
            velocity += gabiVelocity;
        }

        ani.SetFloat("velocity", velocity, 0.2f, Time.deltaTime);


        if (v3.magnitude > 0 && ani.GetCurrentAnimatorStateInfo(0).IsName("blLocomove"))
        {
            q1 = transform.rotation;
            q2 = Quaternion.LookRotation(v3);

            transform.rotation = Quaternion.Slerp(q1, q2, Time.deltaTime * 4);
        }

        if (Raycast())
        {
            playerPosition = hit.point;
            transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * 10);
        }

    }

    bool Raycast()
    {
        return Physics.Raycast(transform.position + Vector3.up, -transform.up, out hit, rayDistanceDown, layerMask);
    }

}
