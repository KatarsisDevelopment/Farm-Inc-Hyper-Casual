using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyManager : MonoBehaviour
{
    public Text TextCost;
    public Image ImagCost;
    public string CostKey;
    public float Cost,MaxCost;
    public GameObject BuyObj;
    private void Start()
    {
        Cost = PlayerPrefs.GetFloat(CostKey,Cost);
    }
    private void Update()
    {
        TextCost.text = "" + Cost;
        ImagCost.fillAmount = Cost / MaxCost;
        if (Cost <= 0)
        {
            BuyObj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
