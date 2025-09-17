using UnityEngine;

public enum SC02ViewId
{
    None,
    SC02_1,
    SC02_2,
    SC02_3
}
public class SC02 : SubViewManager<SC02ViewId>
{
    public override void Start()
    {
        base.Start();
        ShowView(SC02ViewId.None);
    }

    public void OnButtonGoToSC02_1()
    {
        ShowView(SC02ViewId.SC02_1);
    }
    public void OnButtonGoToSC02_2()
    {
        ShowView(SC02ViewId.SC02_2);

    }
    public void OnButtonGoToSC02_3()
    {
        ShowView(SC02ViewId.SC02_3);
    }
    public void OnButtonBackToHome()
    {
        MainScene.Instance.LoadView(ViewId.Home);
    }
    public void BackSC02()
    {
        ShowView(SC02ViewId.None);
    }
    
}
