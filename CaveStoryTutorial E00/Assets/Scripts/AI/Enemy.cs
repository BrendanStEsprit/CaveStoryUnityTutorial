using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IHittable
{
    public GameObject healthup;
    public GameObject ExpUp;
    public bool candrop;
    

    
    
    public void OnHit(Vector3 position, Projectile projectile)
    {
        SubtractHealth(projectile.damage);
    }

    protected override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        if(candrop)
        {
            Instantiate(healthup, this.transform.position, Quaternion.identity);
            Instantiate(ExpUp, this.transform.position, Quaternion.identity);
        }
        
    }
    
}
