using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float walkSpeed = 1;
    [SerializeField] float runSpeed = 2;
    [SerializeField] float movdur = 0;
    [SerializeField] float movdurDecc = 0.5f;
    [SerializeField] float inputCutoffTolerance = 0;
    [SerializeField] float inputCutoff = 0.2f;
    [SerializeField] float dur2Run = 0.8f;
    float acceleration = 2;
    //[SerializeField] float maxSpeed = 1;
    //[SerializeField] float reactivity = 0.2f;
    //[SerializeField] float wallFriciton = 0.15f;
    [SerializeField] float friction = 0.3f;
    [SerializeField] Vector2 input = Vector2.zero;
    [SerializeField] Vector2 movement;
    [SerializeField] Vector2 forces;
    [SerializeField] Vector2 velocity;
    [SerializeField] float speed;
    [SerializeField] float forceGravity;


    [SerializeField] LayerMask wall;
    [SerializeField] float knockbackForce = 50;


    private Vector2 cachedDir;
    private float cacheDurationBefore = 0.05f;
    private float cacheT;


    public Gun playerGun;

    public List<Upgrade> AttachedUpgrades;
    public int Health = 10;
    private bool alive = true;
    public SpriteRenderer sr;

    private void Awake()
    {
        playerGun = GetComponentInChildren<Gun>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region input

        //Input
        if (Health > 0)
        {
            if (Input.GetKey(KeyCode.W))
                input.y = 1;
            if (Input.GetKey(KeyCode.S))
                input.y = -1;
            if (Input.GetKey(KeyCode.A))
                input.x = -1;
            if (Input.GetKey(KeyCode.D))
                input.x = 1;
            if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
                input.y = 0;
            if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
                input.x = 0;
        }
        else
        {
            input = Vector3.zero;
            if (alive)
            {
                GameManager.instance.GameOver();
                alive = false;
            }
        }

        if (input.magnitude > 0)
        {
            if (cachedDir != input)
            {
                if (input.magnitude > 0)
                    cacheT += Time.deltaTime;
            }
            if (cacheT > cacheDurationBefore)
            {
                cacheT = 0;
                cachedDir = input;
            }
        }

        #endregion
        #region input vs collisions
        //if (Physics2D.BoxCast(col.bounds.center, col.bounds.size, transform.rotation.z, Vector2.up, 0.01f, wall))
        //    uCol = true;
        //else
        //    uCol = false;
        //if (Physics2D.BoxCast(col.bounds.center, col.bounds.size, transform.rotation.z, Vector2.down, 0.01f, wall))
        //    dCol = true;
        //else
        //    dCol = false;
        //if (Physics2D.BoxCast(col.bounds.center, col.bounds.size, transform.rotation.z, Vector2.left, 0.01f, wall))
        //    lCol = true;
        //else
        //    lCol = false;
        //if (Physics2D.BoxCast(col.bounds.center, col.bounds.size, transform.rotation.z, Vector2.right, 0.01f, wall))
        //    rCol = true;
        //else
        //    rCol = false;



        //if ((uCol && input.y > 0) || (dCol && input.y < 0))
        //{
        //    input.y = 0;
        //    if (input.x != 0)
        //        input.x = input.x * wallFriciton;
        //}

        //if ((lCol && input.x < 0) || (rCol && input.x > 0))
        //{
        //    input.x = 0;
        //    if (input.y != 0)
        //        input.y = input.y * wallFriciton;
        //}
        #endregion
        #region movement calculation
        if (input.magnitude > 0)
        {
            inputCutoffTolerance = inputCutoff;
            movdur = Mathf.Clamp(movdur + Time.deltaTime, 0, dur2Run);
            acceleration = Mathf.Lerp(0, 1, movdur / dur2Run) * runSpeed;

            //Vector2 dir = Vector2.Lerp(rb.velocity.normalized, input, 0.4f);
            
            movement = Vector2.ClampMagnitude(input * (walkSpeed + acceleration), runSpeed);
        }
        else
        {
            movement = cachedDir.normalized * movement.magnitude * friction;
            //movement *= friction;

            if (movement.magnitude < 0.05f) //Cut movement off to avoid crazy floating point values
                movement = Vector2.zero;
            if (inputCutoffTolerance > 0)
                inputCutoffTolerance = Mathf.Clamp(inputCutoffTolerance - Time.deltaTime, 0, 100000000000);
            else
                movdur = Mathf.Clamp(movdur - movdurDecc, 0, dur2Run);
        }
        #endregion



        #region reaction to forces

        if (Input.GetKeyDown(KeyCode.Space))
            AddForce(Vector2.up * knockbackForce);

        forces = forces * forceGravity;
        if (forces.magnitude < 0.01f)
            forces = Vector2.zero;
        #endregion

        velocity = movement + forces; // Gets added during Fixed Update
        speed = velocity.magnitude; // Just for debug
    }

    public void AddForce(float _strength, Vector2 _dir)
    {
        forces = _strength * _dir;
    }

    public void AddKnockback()
    {

    }

    public void AddForce(Vector2 _force)
    {
        forces = _force;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * Time.fixedDeltaTime;
    }

    IEnumerator HitFlash()
    {
        sr.color = Color.black;
        yield return new WaitForSeconds(0.05f);
        sr.color = Color.white;
    }

    public void GetHit()
    {
        if (AttachedUpgrades.Count > 0)
        {
          
            Upgrade last = AttachedUpgrades[AttachedUpgrades.Count - 1];

            AttachedUpgrades.Remove(last);

            last.DowngradePlayer();

            last.DestroyUpgrade();
        }
        else
        {
            Health -= 1;
            rb.mass -= 0.5f;

        }
        StartCoroutine(HitFlash());

        //
        //      Debug.Log(Health);
    }

    public void UpgradeFireRate(bool Upgrade)
    {
        playerGun.FireRate *= Upgrade ? 0.95f : 1.05f;
        playerGun.ReloadTime *= Upgrade ? 0.9f : 1.1f;
        UpdateUpgrade(Upgrade);
    }


    public void UpgradeBulletSpeed(bool Upgrade)
    {
        playerGun.BarrelSpeed *= Upgrade ? 1.05f : 0.95f;
        UpdateUpgrade(Upgrade);
    }
        public void UpgradeAmmoCapacity(bool Upgrade)
    {
        playerGun.MagazineCapacity += Upgrade ? 1 : -1;
        UpdateUpgrade(Upgrade);
    }

    public void UpgradePenetration(bool Upgrade)
    {
        playerGun.BounceCount += Upgrade ? 1 : -1;
        UpdateUpgrade(Upgrade);
    }

    public void UpdateUpgrade(bool upgrade)
    {
        GameManager.instance.GameStage = AttachedUpgrades.Count / 2;
        Health += upgrade? 1 : -1;  
        rb.mass += upgrade ?  0.5f : -0.5f;

    }


}
