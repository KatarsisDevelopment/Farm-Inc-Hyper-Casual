using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
{

    public GameObject[] Moneys;
    public int MoneyCount = 0;
    void Start()
    {
        for (int i = 0; i < Moneys.Length; i++)
        {
            Moneys[i] = transform.GetChild(0).GetChild(i).gameObject;
        }
    }
  
}
