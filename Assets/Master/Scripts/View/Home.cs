using UnityEngine;

public class Home : MonoBehaviour
{
   public void OnButtonGoToSC01()
    {
        Debug.Log("OnButtonGoToSC01");
        MainScene.Instance.LoadView(ViewId.SC01);
    }
   
    public void OnButtonGoToSC02()
    {
        Debug.Log("OnButtonGoToSC02");
        MainScene.Instance.LoadView(ViewId.SC02);
    }
    public void OnButtonGoToSC03()
    {
        Debug.Log("OnButtonGoToSC03");
        MainScene.Instance.LoadView(ViewId.SC03);
    }
    public void OnButtonQuit()
    {
        Debug.Log("QuitApplication");
        Application.Quit();
    }
}
