using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public abstract class Upgrade : MonoBehaviour
{
    private Collider2D Collider;
    private SpriteRenderer SpriteRenderer;
    public Sprite AttachedSprite;

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack);
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            UpgradePlayer();
        }   
    }
    protected virtual void AttachToPlayer()
    {
        transform.SetParent(GameManager.instance.player.transform);
        Collider.isTrigger = false;
        gameObject.tag = "Player";
        GameManager.instance.player.AttachedUpgrades.Add(this);
        SpriteRenderer.sprite = AttachedSprite;
    }

    public void DestroyUpgrade()
    {
        Collider.enabled = false;
        transform.parent = null;
        transform.DOScale(Vector3.zero, 0.7f).OnComplete(() => { Destroy(gameObject); }); 
    }
    protected abstract void UpgradePlayer();
    public abstract void DowngradePlayer();
}
