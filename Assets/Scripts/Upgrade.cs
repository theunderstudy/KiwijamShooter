using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public abstract class Upgrade : MonoBehaviour
{
    private Collider2D Collider;

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpgradePlayer();
            AttachToPlayer();
        }   
    }
    protected virtual void AttachToPlayer()
    {
        transform.SetParent(GameManager.instance.player.transform);
        Collider.isTrigger = false;
        gameObject.tag = "Player";

    }
    protected abstract void UpgradePlayer();
}
