using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial_Tap : TapCollider
{

    //0:左,1:中央,2:右
    public int PositionNo;

    //Judgeクラス
    public Dial_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (JudgeClass.isClear)
            return;

        AudioManager.Instance.SoundSE("Dial");

        JudgeClass.Judge(PositionNo);
    }
}
