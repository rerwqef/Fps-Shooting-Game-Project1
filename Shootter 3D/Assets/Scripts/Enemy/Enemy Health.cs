 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]int maxHealth;
    [SerializeField] int currentHelth;

   void Start()
    {
        currentHelth = maxHealth; 
    }
    public void TakeDamge(int _damage)
    {
        currentHelth -= _damage;
        if(currentHelth <= 0)
        {
            Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }
}
