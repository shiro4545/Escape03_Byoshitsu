using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenkey_Tap : TapCollider
{
    public int Number;

    //Judgeクラス
    public Tenkey_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        AudioManager.Instance.SoundSE("TapButton");

        //ボタンを後ろに移動
        this.gameObject.transform.Translate(new Vector3(0, 0, -0.03f));
        Invoke(nameof(AfterPush), 0.1f);
        //入力
        JudgeClass.Input(Number);
    }

    //ボタンを元に戻す
    private void AfterPush()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, 0.03f));
    }
}
