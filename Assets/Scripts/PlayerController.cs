using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody RB;

    [Header("Swap")] 
    [SerializeField] private float swapSpeed = 10; //variables for swap speed
    [SerializeField] private float leftRightSwapCooltime = 0.5f; //cooltime for swap
    private float lastLeftRightSwapTime; //last swap time

    [Header("Touch Input Swap")]
    [SerializeField] private float pixelDistanceToDetect = 20; //how many pixel distance of touch input to move to detect it as a swap
    private bool fingerDown; //is finger pressed
    private Vector2 startPos; 
    private Vector2 endPos;

    private bool swapLeft;
    private bool swapRight;
    
    private bool canSwap;

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        ReceiveInput();
        MoveAction();
    }

    void ReceiveInput()
    {
        if (!fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        } //start pressing
        
        if (fingerDown) //if start pressing
        {
            if (Input.touchCount > 0 && Input.touches[0].position.x >= startPos.x + pixelDistanceToDetect)
            {
                fingerDown = false;
                swapRight = true;
                //print("swipe right");
            }
            else if (Input.touchCount > 0 && Input.touches[0].position.x <= startPos.x - pixelDistanceToDetect)
            {
                fingerDown = false;
                swapLeft = true;
                //print("swipe left");
            }
            
            if (Input.touchCount == 0) fingerDown = false;
        }
        //https://www.youtube.com/watch?v=ZKTs1DYCEDI my resource
    }
    
    void MoveAction()
    {
        if (lastLeftRightSwapTime + leftRightSwapCooltime < Time.time && canSwap)
        {
            if (swapLeft) //swap left
            {
                //print("swap left!");
                RB.velocity = Vector3.zero;
                RB.AddForce(new Vector3(-1,1f,0) * swapSpeed); 
                lastLeftRightSwapTime = Time.time;
                swapLeft = false;
                canSwap = false;
            } else if (swapRight)
            {
                //print("swap right!");
                RB.velocity = Vector3.zero;
                RB.AddForce(new Vector3(1,1f,0) * swapSpeed); //add force
                lastLeftRightSwapTime = Time.time;
                swapRight = false;
                canSwap = false; //reset input
            }
            else
            {
                swapLeft = false;
                swapRight = false;
            }
        }
        else
        {
            swapLeft = false;
            swapRight = false;
        }
    }
    
    void CheckGrounded() //check ground
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.55f, 1 << LayerMask.NameToLayer("Ground"));

        if (colliders.Length > 0)
        {
            canSwap = true;
        }
    }
}
