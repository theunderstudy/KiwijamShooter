using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public enum AIState
{
    nill = 0,
    moving = 100,
    attacking = 200,
    dying = 300
}
public abstract class EnemyBase : MonoBehaviour
{
    public EnemyType type;
    public int Health = 1;
    public Rigidbody2D RB;
    public SpriteRenderer sr;
    private Sprite sprite;
    protected TopDownCharacterController player;
    protected Collider2D Collider2D;
    [SerializeField] protected AIState CurrentState = AIState.nill;
    public float attackRange;
    public float sensoryRange;

    public float maxSpeed;
    public float acc;
    protected float distanceToPlayer;
    protected virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        player = GameManager.instance.player;
    }

    protected virtual void Update() {
        UpdateAIState();
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, sensoryRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    protected virtual void UpdateAIState()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (!Dying())
        {
            if (distanceToPlayer < sensoryRange)
            {
                if (distanceToPlayer < attackRange)
                {

                    CurrentState = AIState.attacking;
                }
                else
                {
                    CurrentState = AIState.moving;
                }
            }
        }
        // Check if in attack range
        // -> atk
        // else move?
        // maybe some 3rd state
    }

    protected virtual void MoveToPlayer()
    {
        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 myPos = transform.position;
        Vector2 target = playerPos - myPos;
        Debug.DrawLine(myPos, myPos + target.normalized * 5);
        RB.AddForce(target.normalized * acc, ForceMode2D.Force);
        RB.velocity = Vector2.ClampMagnitude(RB.velocity, maxSpeed);
    }

    protected abstract void Attack();

    public virtual void GetHit()
    {
        if (Dying())
        {
            return;
        }
        StartCoroutine(HitFlash());
        Health -= 1;
        if (Health <= 0)
        {
            StartDeath();
        }
    }

    IEnumerator HitFlash()
    {
        sr.color = Color.black;
        yield return new WaitForSeconds(0.05f);
        sr.color = Color.white;
    }

    protected virtual void StartDeath()
    {
        CurrentState = AIState.dying;
        transform.DOScale(Vector2.zero, 1.0f).SetEase(Ease.InBack).OnComplete(Die);
        Collider2D.enabled = false;
    }

    protected virtual void Die()
    {
        GameManager.instance.EnemyDied(type);
        Destroy(gameObject);
        GameManager.instance.SpawnUpgrade(transform.position, type);        
    }

    public bool Dying() 
    {

        return CurrentState == AIState.dying;
    }




}
