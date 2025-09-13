using UnityEngine;

public enum SC01ViewId 
{
    None,
    SC01_1,
    SC01_2
}
public class SC01 : SubViewManager<SC01ViewId>
{
    public void OnButtonGoToSC01_1()
    {
        ShowView(SC01ViewId.SC01_1);
    }
    public void OnButtonGoToSC01_2()
    {
        ShowView(SC01ViewId.SC01_2);
    }
    public void OnButtonBackToHome()
    {
        MainScene.Instance.LoadView(ViewId.Home);
    }
    public void OnButtonBackSC01()
    {
        ShowView(SC01ViewId.None);
    }
}
