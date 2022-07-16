using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf5Slide_Tap : TapCollider
{
    //0:閉まり 1:開き
    public int status;

    public GameObject CloseSlide;
    public GameObject OpenSlide;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        AudioManager.Instance.SoundSE("Slide");
        if(status == 0)
        {
            CloseSlide.SetActive(false);
            OpenSlide.SetActive(true);
        }
        else
        {
            CloseSlide.SetActive(true);
            OpenSlide.SetActive(false);
        }

    }
}
