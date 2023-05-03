using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    RaycastHit hit;
    FixedJoystick joystick;
    Animator Animator;
    public GameObject[] Eggs , Bales , Moneys;
    public int EggCount,BalesCount, money;
    public  float SpeedTakeEgg = 0.3f;
    public Animator SellAnimator,ImageAnimator;
    public Text MoneyText;
    public GameObject MoneyPref,BaleAnimPref,MaxText;
    void Start()
    {
        for (int i = 0; i < Eggs.Length; i++)
        {
            Eggs[i] = transform.GetChild(0).GetChild(i).gameObject;
        }
        for (int i = 0; i < Bales.Length; i++)
        {
            Bales[i] = transform.GetChild(1).GetChild(i).gameObject;
        }
      
        joystick = FindObjectOfType<FixedJoystick>();
        Animator = GetComponent<Animator>();
        StartCoroutine(TakeEggEnum());
        StartCoroutine(TakeBales());
        PlayerPrefs.GetInt("Money",money);
    }
    void Update()
    {
        MoneyText.text = "" + money;
        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            Animator.SetFloat("Run", 1f);
            transform.position += new Vector3(joystick.Horizontal, 0, joystick.Vertical) * 3f * Time.deltaTime;
            transform.forward = new Vector3(joystick.Horizontal * 5f, 0, joystick.Vertical * 5f);
        }
        else
        {
            Animator.SetFloat("Run", 0f);
        }
        if (EggCount > 0)
        {
            Animator.SetBool("HaveBox", true);
        }
        else
        {
            Animator.SetBool("HaveBox", false);
        }
        Camera.main.transform.position = new Vector3(0, 6, -4) + transform.position;
        if (BalesCount >= 9)
        {
            MaxText.SetActive(true);
        }
        else
        {
            MaxText.SetActive(false);
        }
        GiveBales();
        SellEgg();
    }
    void GiveBales()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            Coop coop = hit.transform.gameObject.GetComponent<Coop>();
            if (hit.transform.gameObject.GetComponent<Coop>())
            {
                if (BalesCount > 0 && coop.BaleCount < 9)
                {
                    coop.BaleCount += 1;
                    coop.Bales[BalesCount - 1].gameObject.SetActive(true);
                    BalesCount -= 1;
                    Bales[BalesCount].SetActive(false);
                }
            }
        }
    }
    void SellEgg()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            Seller seller = hit.transform.gameObject.GetComponent<Seller>();
            if (seller)
            {
                if (EggCount > 0)
                {
                    Eggs[EggCount - 1].SetActive(false);
                    EggCount -= 1;
                    seller.MoneyCount += 1;
                    if (seller.MoneyCount < seller.Moneys.Length)
                    {
                        seller.Moneys[seller.MoneyCount - 1].SetActive(true);
                    }
                }
            }
        }
    }
    IEnumerator TakeEggEnum()
    {
        while (true)
        {
            if (Physics.Raycast(transform.position,transform.forward,out hit,1f ))
            {
                Coop coop = hit.transform.gameObject.GetComponent<Coop>();
                if (hit.transform.gameObject.GetComponent<Coop>())
                {
                    if (coop.EggCount > 0)
                    {
                        if (EggCount < Eggs.Length)
                        {
                            coop.Eggs[coop.EggCount - 1].SetActive(false);
                            coop.EggCount -= 1;
                            EggCount += 1;
                            Eggs[EggCount - 1].SetActive(true);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(SpeedTakeEgg);
        }
    }
    IEnumerator TakeBales()
    {
        while (true)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
            {
                Farm farm = hit.transform.gameObject.GetComponent<Farm>();
                if (hit.transform.gameObject.GetComponent<Farm>())
                {
                    if (farm.BaleCount > 0 && BalesCount < 9)
                    {
                        farm.BaleCount -= 1;
                        farm.Bales[farm.BaleCount].SetActive(false);
                        BalesCount += 1;
                        if (BalesCount > 0)
                        {
                            Bales[BalesCount - 1].SetActive(true);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MoneyPref"))
        {
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Money", money++);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BuyArea"))
        {
            if (money > 0)
            {
                PlayerPrefs.SetFloat(other.gameObject.GetComponent<BuyManager>().CostKey, other.gameObject.GetComponent<BuyManager>().Cost -= 1);
                PlayerPrefs.SetInt("Money", money--);
            }
        }
    }
}
