using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public List<Gun> gunsOwned;

    private int equippedGunID;
    private Gun equippedGun;
    Player player;
    SpriteRenderer gunRenderer;
    Transform gunTransform;

    public AudioSource audioSource;
    public AudioClip fireweapon1;
    public AudioClip fireweapon2;

    private void Start()
    {
        player = Player.instance;
        gunRenderer = GetComponentInChildren<SpriteRenderer>();
        gunTransform = gunRenderer.transform;
        InitGun();
    }

    private void Update()
    {
        SwitchingWeapons();
        PointInDirectionOfPlayer();
        Shooting();
    }

    private void InitGun()
    {
        equippedGun = gunsOwned[equippedGunID];
        gunRenderer.sprite = equippedGun.sprite;
    }

    private void SwitchingWeapons()
    {
        int gunIDMod = 0;

        if(Input.GetButtonDown("Fire3"))
        {
            gunIDMod = -1;
        }
        else if(Input.GetButtonDown("Fire4"))
        {
            gunIDMod = 1;
        }

        if(gunIDMod != 0)
        {
            equippedGunID = (equippedGunID + gunIDMod) % gunsOwned.Count;
            InitGun();
        }
    }

    private void PointInDirectionOfPlayer()
    {
        bool facingRight = player.IsFacingRight();
        Direction direction = player.GetDirection();

        Vector2 handleOffset = new Vector2(equippedGun.handleOffsetX, equippedGun.handleOffsetY);
        float facingRightMultiplier = facingRight ? -1 : 1;
        float facingUpMultiplier = (direction == Direction.UP ? 1 : -1);
        float x = 0;
        float y = 0;
        float rot = 0;

        if(direction == Direction.UP || direction == Direction.DOWN)
        {
            
            y = facingUpMultiplier * facingRightMultiplier * handleOffset.y;
            rot = facingUpMultiplier * -90f;

            
        }
        else
        {
            rot = 90f - (facingRightMultiplier * 90f);
            
            y = facingRightMultiplier * handleOffset.y;

           
        }
        x = handleOffset.x;

        gunRenderer.flipX = false;
        gunRenderer.flipY = facingRight;

        gunTransform.localPosition = new Vector3(x, y, 0) * 0.0625f;
        transform.eulerAngles = new Vector3(0, 0, rot);
    }
    private void Shooting()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            audioSource.PlayOneShot(fireweapon1);
            if (player.level==1)
            {
                GameObject shootableObject = Instantiate(equippedGun.shootablePrefab, gunTransform.position, Quaternion.identity);
                IShootable shootable = shootableObject.GetComponent<IShootable>();
                shootable.Shoot();
            }
            if(player.level==2)
            {
                GameObject shootableObject = Instantiate(equippedGun.shootablePrefab2, gunTransform.position, Quaternion.identity);
                IShootable shootable = shootableObject.GetComponent<IShootable>();
                shootable.Shoot();
            }
            if(player.level==3)
            {
                GameObject shootableObject = Instantiate(equippedGun.shootablePrefab3, gunTransform.position, Quaternion.identity);
                IShootable shootable = shootableObject.GetComponent<IShootable>();
                shootable.Shoot();
            }
            
        }

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
