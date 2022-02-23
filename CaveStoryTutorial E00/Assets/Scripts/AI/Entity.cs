using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{
    public int maxHealth = 5;
    public int maxLevel = 3;
    public int level;
    public int health;

    protected virtual void Awake()
    {
        health = maxHealth;
        level = 1;
    }
    public void AddHealth(int m) { Modhealth(m); }
    public void SubtractHealth(int m) { Modhealth(-m); }
    public void AddLevel(int n) { ModLevel(n); }
    public void SubstactLevel(int n) { ModLevel(-n); }


    private void ModLevel(int n)
    {
        level += n;
        CapLevel();
    }
    private void Modhealth(int m)
    {
        health += m;
        CapHealth();

    }
    private void CapHealth()
    {
        if(health <= 0)
        {
            Die();
        }else if (health > maxHealth)
        {
            health = maxHealth;
        }
    
    } 
    private void CapLevel()
    {
        if(level<=0)
        {
            level = 1;
        }else if(level>maxLevel)
        {
            level = maxLevel;
        }

    }
    protected virtual void Die()
    {
        Debug.Log(name + "died");
        
    }
    
        
        
    

}
