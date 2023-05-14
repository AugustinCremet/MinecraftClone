using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    [SerializeField] float maxDistance = 1.0f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform cam;
    Animator animator;
    bool leftClick;
    float time = 0.0f;
    Vector3 previousTarget = Vector3.zero;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnLeftClickPressed(float value)
    {
        if(value == 1.0f)
            leftClick = true;
        else
            leftClick = false;
    }

    void Update()
    {
        RemoveBlock();
        AnimateArm();
    }

    void RemoveBlock()
    {
        int test = 0;
        RaycastHit currentHitInfo;
        if(Physics.Raycast(cam.position, cam.transform.forward, out currentHitInfo, maxDistance, groundMask))
        {
            Vector3 currentTarget = currentHitInfo.transform.position;
            Blocks b = currentHitInfo.collider.GetComponent(typeof(Blocks)) as Blocks;
            
            if(currentTarget == previousTarget && leftClick)
            {
                time += Time.deltaTime;
                b.StartParticles(cam.transform);
                b.PlayMiningSound();
                test++;
                Debug.Log(test);
            }
            else
            {
                time = 0.0f;
                b.StopParticles();
                b.StopMiningSound();
            }

            if(time >= b.GetTimeToDestroy())
            {
                b.DestroyBlock();
                time = 0.0f;
            }
        }
        RaycastHit previousHitInfo;
        if(Physics.Raycast(cam.position, cam.transform.forward, out previousHitInfo, maxDistance, groundMask))
        {
            previousTarget = previousHitInfo.transform.position;
        }
    }

    void AnimateArm()
    {
        if(leftClick)
            animator.SetBool("Mining", true);
        else
            animator.SetBool("Mining", false);
    }
}
