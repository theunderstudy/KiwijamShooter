using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setup stuff")]
    public Transform gunLocator;
    public Rigidbody2D rb;

    [Header("Movement stuff")]
    public float minAcc = 10;
    public float maxAcc = 30;
    public float maxSpeed = 80f;
    private float acc = 0;
    public float maxAccDur = 1.5f;
    private float accDur;
    public float maxChangeDirFactor = 3;
    public float brakingFactor = 200;

    [Header("Debug stuff")]
    public Vector2 input;
    private Vector2 position;
    public Vector2 v = Vector2.zero;
    public Vector2 dir = Vector2.zero;
    public Vector2 appliedBraking;
    public float speed;
    public float angleDiff;
    public float changeDirFactor = 0 ;


    void Update()
    {
        #region setting stuff
        position = rb.position;
        v = rb.velocity;
        dir = v.normalized;
        speed = v.magnitude;
        #endregion
        #region input getting
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input = Vector2.ClampMagnitude(input, 1);
        #endregion

        #region speed stuff
        if (input.magnitude > 0)
        {
            accDur = Mathf.Clamp(accDur + Time.deltaTime, 0, maxAccDur);
            if (acc < minAcc)
                acc = minAcc;
        }
        else accDur = Mathf.Clamp(accDur - Time.deltaTime, 0, maxAccDur);


        acc = Mathf.Lerp(minAcc, minAcc, accDur/maxAccDur);
        #endregion
        #region changeDirFactor
        angleDiff = Vector2.Angle(dir, input.normalized);
        changeDirFactor = Mathf.Lerp(1, maxChangeDirFactor, angleDiff / 180);
        #endregion

        

        if(speed < maxSpeed)
            rb.AddForce(input * acc  * changeDirFactor * Time.deltaTime, ForceMode2D.Impulse);

        #region braking forces

        //if (input.magnitude == 0)
        //{
        //    if (speed > 0)
        //    {
        //        appliedBraking =  (-rb.velocity.normalized * brakingFactor) * Time.deltaTime;
        //        Debug.DrawRay(rb.position, appliedBraking, Color.grey);
        //        rb.AddForce( appliedBraking * Time.deltaTime, ForceMode2D.Impulse);
        //    }
        //    else appliedBraking = Vector2.zero;
        //}
        //else appliedBraking = Vector2.zero;
        #endregion

        if (input.magnitude < 0.01f)
        {
            Vector2 velocity = rb.velocity;
            velocity *= -0.5f;
            rb.AddForce(velocity);
        }
    }


}
