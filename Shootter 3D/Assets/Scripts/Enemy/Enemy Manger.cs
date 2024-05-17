using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class EnemyManger : MonoBehaviour
{
    public TextMeshProUGUI numOfEnemyText;
    int numOfEnemies;

    void Start()
    {
        UpdateNumOfEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNumOfEnemies();
        if (numOfEnemies <= 0)
        {
            numOfEnemies = 0;
            Debug.Log("Enemies all finished");
        }
    }

    void UpdateNumOfEnemies()
    {
         numOfEnemies = GetComponentsInChildren<Enemy>().Length;
        numOfEnemyText.text = numOfEnemies.ToString();
    }
}