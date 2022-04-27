using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFeetController : MonoBehaviour
{
    [Range(-1, 1)]
    [SerializeField] float groundDistance;
    [Range(-1, 1)]
    [SerializeField] float toeDistance;
    [SerializeField] LayerMask playerMask;

    Animator animator;
    bool canToe = true;
    RaycastHit footHit, toeHit;
    Ray footRay, toeRay;


    Vector3 leftFootOrigin, rightFootOrigin, rightToeOrigin, leftToeOrigin;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnAnimatorIK(int layerIndex)
    {
        leftFootOrigin = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        rightFootOrigin = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        rightToeOrigin = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        leftToeOrigin = animator.GetIKPosition(AvatarIKGoal.LeftFoot);


        FootPlacement(layerIndex);
        ToePlacement(layerIndex);

    }

    void FootPlacement(int layerIndex)
    {
        if (animator)
        {

            footRay = new Ray(leftFootOrigin + Vector3.up * 0.2f, Vector3.down);

            //Left foot
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLeftFootWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLeftFootWeight"));



            if (Physics.Raycast(footRay, out footHit, groundDistance + 1f, playerMask))
            {

                if (footHit.transform.tag == "Walkable")
                {

                    Vector3 fooPosition = footHit.point;
                    fooPosition.y += groundDistance;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, fooPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, footHit.normal));
                }
            }


            //Right Foot
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRightFootWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRightFootWeight"));

            footRay = new Ray(rightFootOrigin + Vector3.up * 0.2f, Vector3.down);

            if (Physics.Raycast(footRay, out footHit, groundDistance + 1f, playerMask))
            {
                if (footHit.transform.tag == "Walkable")
                {
                    Vector3 footPosition = footHit.point;
                    footPosition.y += groundDistance;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, footHit.normal));
                }
            }
        }
    }

    //Use the foot bone position to calculate the toe bone position.
    void ToePlacement(int layerIndex)
    {
        if (animator)
        {
            toeRay = new Ray(leftToeOrigin + Vector3.forward * 0.2f + Vector3.up * 0.1f, Vector3.down);

            //Left foot
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLeftFootWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLeftFootWeight"));



            if (Physics.Raycast(toeRay, out toeHit, toeDistance, playerMask))
            {

                if (toeHit.transform.tag == "Walkable")
                {
                    Vector3 fooPosition = toeHit.point - Vector3.forward * 0.1f;
                    fooPosition.y += groundDistance;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, fooPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, toeHit.normal));
                }
                else
                {
                    canToe = false;
                }
            }


            //Right Foot
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRightFootWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRightFootWeight"));

            toeRay = new Ray(rightToeOrigin + Vector3.forward * 0.2f + Vector3.up * 0.1f, Vector3.down);

            if (Physics.Raycast(toeRay, out toeHit, toeDistance, playerMask))
            {
                if (toeHit.transform.tag == "Walkable" && !canToe)
                {
                    Vector3 footPosition = toeHit.point - Vector3.forward * 0.1f;
                    footPosition.y += groundDistance;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, toeHit.normal));
                }
                else
                {
                    canToe = true;
                }
            }
        }
    }

}

