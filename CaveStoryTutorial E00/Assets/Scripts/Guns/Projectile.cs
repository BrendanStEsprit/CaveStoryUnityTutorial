using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IShootable
{
    public float speed;
    public float range;
    public int damage;

    Player player;
    Vector3 direction;
    float time;

    public LayerMask hittableMask;

    SpriteRenderer spriteRenderer;
    ParticleSystem Cog_Flash;
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Shoot()
    {
        Cog_Flash = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = Player.instance;
        direction = General.Direction2Vector(player.GetDirection());

        transform.eulerAngles = Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(-direction.y, -direction.x);
        time = range / speed;
    }
    void Update()
    {
        time -= Time.deltaTime;
        transform.position += direction * speed * Time.deltaTime;

        if(time <0f)
        {
            Die();
        }

        CheckForCollisions2D();
    }
    public void Die()
    {
        Cog_Flash.Play();
        spriteRenderer.enabled = false;
        this.enabled = false;
        
        
    }
    void CheckForCollisions2D()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, speed * Time.deltaTime, hittableMask.value);

        if(hit.collider != null)
        {
            IHittable[] hittables = hit.collider.GetComponents<IHittable>();

            foreach(IHittable hittable in hittables)
            {
                hittable.OnHit(hit.point, this);
            }
            Die();
        }
        
    }
}
