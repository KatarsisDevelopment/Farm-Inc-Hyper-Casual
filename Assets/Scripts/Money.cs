using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Money : MonoBehaviour
{
    public Seller Seller;
    private void OnDisable()
    {
        Seller.MoneyCount -= 1;
    }

}
