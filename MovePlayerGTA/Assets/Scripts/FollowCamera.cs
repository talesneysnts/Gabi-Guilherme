using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField]
    float distanceCameraHead = 1;

    Ray ray;
    Animator animatorPlayer;

    [SerializeField]
    LayerMask layerMask;

    Camera cam;
    GameObject playerGameobject;

    //private GameObject gabi;

    [SerializeField] SOPlayersIndexer playersIndexer;

    Transform pivot;
    Transform internalCamera;

    float mouseX = 0, mouseY = 0, headPlayer = 0.5f, distance;

    Vector3 originalPositionInternalCamera;

    void Start()
    {
        ChangeAnimator();
        //gabi = GameObject.FindGameObjectWithTag("Player");
        pivot = transform.Find("pivo");
        internalCamera = transform.GetChild(0).GetChild(0);
        originalPositionInternalCamera = internalCamera.localPosition;
        cam = internalCamera.GetComponent<Camera>();

        distance = (internalCamera.position - pivot.position).magnitude;
    }

    void Update()
    {
        if(playerGameobject == null)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, playerGameobject.transform.position, Time.deltaTime * 8);

        mouseX += Input.GetAxis("Mouse X") * 2;
        mouseY -= Input.GetAxis("Mouse Y") * 2;

        mouseY = Mathf.Clamp(mouseY, -55, 55); //Mathf.Clamp = ele grampeia um valor entre minimo e maximo

        //pivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        RaycastHit hit;
        ray = new Ray(pivot.position, (-pivot.forward * 3));
        Debug.DrawLine(pivot.position, pivot.position + (-pivot.forward * 3), Color.black);

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            if (Vector3.Distance(hit.point, pivot.position) < distanceCameraHead)
            {
                internalCamera.localPosition = Vector3.zero; //pivot
                //cam.cullingMask = ~((1 << 7) | (1 << 9)); //faz a camera não mostrar o que tem na layer 7 e 9 (player)

            }
            else
            {
                internalCamera.position = Vector3.Lerp(internalCamera.position,
                hit.point + internalCamera.forward * headPlayer,
                Time.deltaTime * 20f);
                //cam.cullingMask = cam.cullingMask | (1 << 7);
            }
        }
        else
        {
            internalCamera.localPosition = Vector3.Lerp(internalCamera.localPosition, originalPositionInternalCamera, Time.deltaTime * 5f);
            //cam.cullingMask = cam.cullingMask | (1 << 7);
        }


        if (Input.mousePosition.x > Screen.width - 10f)
        {
            mouseX += 10;
            pivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else if (Input.mousePosition.x < 10f)
        {
            mouseX -= 10;
            pivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            pivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }

    }

    void ChangeAnimator()
    {
        playerGameobject = PlayerIndexerManager.Instance.players[playersIndexer.currentPlayerIndexer];
        animatorPlayer = playerGameobject.GetComponent<Animator>();

    }

    void OnEnable()
    {
        PlayerIndexerManager.playerIndexer += ChangeAnimator;
    }

    void OnDisable()
    {
        PlayerIndexerManager.playerIndexer -= ChangeAnimator;
    }
}
