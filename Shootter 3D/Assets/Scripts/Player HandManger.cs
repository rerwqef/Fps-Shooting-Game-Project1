using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerHandManger : MonoBehaviour
{
    public GameObject BaseHand;
    public GameObject firstGUn;
    public GameObject secondGUn;
    public bool FirstGunEquipied=false;
    public bool SecondGunEquipied=false;

    GameObject intaractedhand;

    [Header("Hands")]
    public GameObject knifeHand;
    public GameObject PistolHand;
    public GameObject ScarHand;
    public GameObject SniperHand;

    GameObject currentHand;
 
    void Start()
    {
        currentHand = knifeHand;
       currentHand.SetActive(true);
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            setCurrenthand(1);

        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setCurrenthand(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setCurrenthand(3);
        }
    }
    void setCurrenthand(int n)
    {

        currentHand.SetActive(false);
        if (n == 1&&currentHand!=knifeHand)
        {

            currentHand = knifeHand;
     
        }else if(n == 2)
        {

       
            currentHand = BaseHand;
        }
        else if( n== 3)
        {

            currentHand = BaseHand;
        }
        currentHand.SetActive(true);
    }
    // Update is called once per frame
    void Gunname(string s)
    {
        if (s == "sniper")
        {

         intaractedhand=  SniperHand;
        }
        else if(s=="scar")
        {
          intaractedhand=ScarHand;
        }
        GunPicker();
    }

    void GunPicker()
    {
        if(!FirstGunEquipied) {
            SetGunIN(1);
        }else if (FirstGunEquipied && !SecondGunEquipied)
        {
            SetGunIN(2);
        }else
        {
            changeGunIn(1);
        }
    }
    void SetGunIN(int n)
    {
       if(n == 1)
        {
         
            firstGUn = intaractedhand;
            FirstGunEquipied = true;
        }
        else if (n == 2)
        {

            secondGUn = intaractedhand;
            SecondGunEquipied = true;
        }
    }
    void changeGunIn(int n)
    {

    }
}
