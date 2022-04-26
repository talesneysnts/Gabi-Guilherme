using System.Collections;
using System.Collections.Generic;
using UnityEngine; //acesso aos componentes da unity (capsula, rigidbody)

//SEM SER MONOBEHAVIOUR, VC NÃO PODE COLOCAR EM UM OBJ DE JOGO
public class moviment //mover/pulo
{

    public void moveForward(Rigidbody rig, float velocity)
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            rig.AddForce(Vector3.forward * velocity);
        }
    }

    public void moveBackward(Rigidbody rig, float velocity)
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            rig.AddForce(-Vector3.forward * velocity);
        }
    }

    public void moveRight(Rigidbody rig, float velocity)
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            rig.AddForce(Vector3.right * velocity);
        }
    }

    public void moveLeft(Rigidbody rig, float velocity)
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            rig.AddForce(Vector3.left * velocity);
        }
    }

}
