using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Bullet BulletPrefab;
    public Transform FireLocation;



    [Range(1.0f, 0.05f)]
    public float FireRate = 0.5f;

    [Range(1, 100)]
    public int MagazineCapacity = 5;

    [Range(1, 10)]
    public int BounceCount = 1;

    [Range(10.0f, 100.0f)]
    public float BarrelSpeed = 10;

    [Range(2.0f, 0.0f)]
    public float ReloadTime = 1;
    

    private float FireRateTimer = 0;
    private int CurrentMagazineCount;
    private bool bReloading = false;
    private float ReloadTimer = 0;
    private AudioSource GunAudioSource;

    private TopDownCharacterController Player;

    private void Awake()
    {
        GunAudioSource = GetComponent<AudioSource>();
        FireRateTimer = FireRate;
        CurrentMagazineCount = MagazineCapacity;
    }
    private void Start()
    {
        Player = GameManager.instance.player;

    }


    void Update()
    {
        FireRateTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            TryFire();
        }
        if (bReloading)
        {
            ReloadTimer += Time.deltaTime;
            if (ReloadTimer >= ReloadTime)
            {
                CurrentMagazineCount = MagazineCapacity;
                bReloading = false;
            }
        }

        return;        
    }


    public bool TryFire()
    {
        if (CurrentMagazineCount <= 0)
        {
            Reload();
            return false;
        }
        if (FireRateTimer > FireRate)
        {
            Shoot();
            FireRateTimer = 0;
            return true;
        }

        return false;
    }

    
    public void Shoot()
    {
        GunAudioSource.Play();

        Bullet bullet = Instantiate<Bullet>(BulletPrefab);
        bullet.transform.position = FireLocation.position;
        bullet.transform.rotation = FireLocation.rotation;
        bullet.Init(BarrelSpeed , BounceCount);
        CurrentMagazineCount -= 1;
    }

    private void Reload()
    {
        if (bReloading)
        {
            return;
        }
        bReloading = true;
        ReloadTimer = 0;
    }
}
