using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public GameObject[] Bales;
    public  int BaleCount = 0;
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            Bales[i] = transform.GetChild(0).GetChild(i).gameObject;
        }
        StartCoroutine(BaleEnum());
    }
    IEnumerator BaleEnum()
    {
        while (true)
        {
            if (BaleCount < 9)
            {
                Bales[BaleCount].SetActive(true);
                BaleCount += 1;
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
