using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHelth : MonoBehaviour
{
    [SerializeField] int playerhelth=100;
   [SerializeField] int playerCurrentHelth;
    public TextMeshProUGUI helthText;
    bool StillAlive = true;
    public GameObject gameoverScreen;
    void Start()
    {
        gameoverScreen.SetActive(false);
        playerCurrentHelth = playerhelth;
        helthText.text=playerCurrentHelth.ToString();
    }
    private void Update()
    {
        
    }

    // Update is called once per frame

    public void TakeDamge(int Damage)
    {
        if (StillAlive)
        {
            playerCurrentHelth -= Damage;

            if (playerCurrentHelth > 0)
            {
                helthText.text = playerCurrentHelth.ToString();

            }
            else
            {
                playerCurrentHelth = 0;
                helthText.text = playerCurrentHelth.ToString();

                StillAlive = false;
                Die();

            }
        }
     

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Sw"))
        {
            Debug.Log("Helth Kuranju");
            TakeDamge(20);
        
        }
    }
    public void Die()
    {
        print("Plyer died");
        gameoverScreen.SetActive(true);
    }
}
