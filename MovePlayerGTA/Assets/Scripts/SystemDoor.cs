using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDoor : MonoBehaviour
{

    [SerializeField] Transform doorLeft, doorRight;
 
    float time, maxTime = 0.6f, perc;

    void OpenDoor(Transform doorLeft, Transform doorRight, float time)
    {
        doorLeft.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, -90, 0), time);
        doorRight.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 90, 0), time);
    }

     void CloseDoor(Transform doorLeft, Transform doorRight, float time)
    {
        doorLeft.rotation = Quaternion.Lerp(Quaternion.Euler(0, -90, 0), Quaternion.identity, time);
        doorRight.rotation = Quaternion.Lerp(Quaternion.Euler(0, 90, 0), Quaternion.identity, time);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("está dentro abrir porta");
            StartCoroutine(OpenDoor());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("está fora fechar porta");
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        //while (time > 0)
        //{
        //    time -= Time.deltaTime;
        //    yield return null;
        //}
        //time = maxTime;

        perc = 0;
        while (perc < 1)
        {
            yield return null;
            time = time + Time.deltaTime;
            perc = time / maxTime;
            OpenDoor(doorLeft, doorRight, perc);
        }
        time = 0;
    }

    IEnumerator CloseDoor()
    {
        //while (time > 0)
        //{
        //    time -= Time.deltaTime;
        //    yield return null;
        //}
        //time = maxTime;

        perc = 0;
        while (perc < 1)
        {
            yield return null;
            time = time + Time.deltaTime;
            perc = time / maxTime;
            CloseDoor(doorLeft, doorRight, perc);
        }
        time = 0;
    }
}
