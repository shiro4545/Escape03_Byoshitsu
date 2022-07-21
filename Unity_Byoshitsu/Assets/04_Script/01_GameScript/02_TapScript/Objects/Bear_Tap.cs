using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear_Tap : TapCollider
{
    //Index
    public int Number;

    //シンバル
    public GameObject Open;
    public GameObject Close;
    
    //Judgeクラス
    public Bear_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        AudioManager.Instance.SoundSE("Cymbal");

        //シンバル開閉
        Open.SetActive(false);
        Close.SetActive(true);

        Invoke(nameof(AfterPush), 0.1f);

        //答え合わせ
        JudgeClass.Judge(Number);
    }

    //シンバルを元に戻す
    private void AfterPush()
    {
        Open.SetActive(true);
        Close.SetActive(false);
    }
}
