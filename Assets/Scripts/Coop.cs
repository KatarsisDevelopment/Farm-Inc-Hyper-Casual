using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coop : MonoBehaviour
{
    public GameObject[] Eggs;
    public GameObject[] Bales;
    public int EggCount,BaleCount;
    public float EggInstantSpeed = 1f;
    public GameObject AnimEgg;
    void Start()
    {
        for (int i = 0; i < Eggs.Length; i++)
        {
            Eggs[i] = transform.GetChild(0).GetChild(i).gameObject;
        }
        for (int i = 0; i < Bales.Length; i++)
        {
            Bales[i] = transform.GetChild(1).GetChild(0).GetChild(i).gameObject;
        }
        StartCoroutine(EggInstanter());
        StartCoroutine(BaleDestroyer());
    }
    private void Update()
    {
        if (BaleCount == 0)
        {
            AnimEgg.SetActive(false);
        }
        else
        {
            AnimEgg.SetActive(true);
        }
    }
    IEnumerator BaleDestroyer()
    {
        while (true)
        {
            if (BaleCount > 0)
            {
                Bales[BaleCount - 1].gameObject.SetActive(false);
                BaleCount -= 1;
            }
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator EggInstanter()
    {
        while (true)
        {
            if (EggCount < Eggs.Length && BaleCount > 0)
            {
                EggCount += 1;
                Eggs[EggCount - 1].SetActive(true);
                if (EggCount < 35)
                {
                    AnimEgg.transform.DOJump(Eggs[EggCount].gameObject.transform.position, 1f, 1, EggInstantSpeed).SetLoops(-1).SetAutoKill(true);
                }
                else
                {
                    AnimEgg.SetActive(false);

                }
            }
            yield return new WaitForSeconds(EggInstantSpeed);
        }
    }
}
