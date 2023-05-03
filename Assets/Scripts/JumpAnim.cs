using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class JumpAnim : MonoBehaviour
{
    Player Player;
    private void OnEnable()
    {
        Player = FindObjectOfType<Player>();
        gameObject.transform.DOLocalJump(gameObject.transform.localPosition, Player.SpeedTakeEgg * 2f, 0, Player.SpeedTakeEgg).SetLoops(0);
    }
}
