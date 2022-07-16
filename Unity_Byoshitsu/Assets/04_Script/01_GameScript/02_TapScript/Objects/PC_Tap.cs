using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Tap : TapCollider
{
    //入力コード
    public string code;

    //Judgeクラス
    public PC_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (JudgeClass.isClear)
            return;

        if(code == "Enter")
            AudioManager.Instance.SoundSE("PushEnter");
        else
            AudioManager.Instance.SoundSE("PushKey");

        //ボタンを後ろに移動
        this.gameObject.transform.Translate(new Vector3(0, -0.01f, 0));
        Invoke(nameof(AfterPush), 0.05f);

        //キーン入力
        JudgeClass.Input(code);
    }

    //ボタンを元に戻す
    private void AfterPush()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0.01f, 0));
    }
}
