using UnityEngine;

public enum SC03ViewId
{
    None,
    SC03_1,
    SC03_2,
    SC03_3
}
public class SC03 : SubViewManager<SC03ViewId>, ISceneController
{
    public override void Start()
    {
        base.Start();
        ShowView(SC03ViewId.None);
    }

    public void OnButtonGoToSC03_1()
    {
        ShowView(SC03ViewId.SC03_1);
    }
    public void OnButtonGoToSC03_2()
    {
        ShowView(SC03ViewId.SC03_2);
    }

    public void OnButtonGoToSC03_3()
    {
        ShowView(SC03ViewId.SC03_3);
    }
    public void OnButtonBackToHome()
    {
        MainScene.Instance.LoadView(ViewId.Home);
    }
    public void BackPreviousScene()
    {
        ShowView(SC03ViewId.None);
    }
}
