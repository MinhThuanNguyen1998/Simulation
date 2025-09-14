using UnityEngine;

public class Home : MonoBehaviour
{
   public void OnButtonGoToSC01()
    {
        Debug.Log("OnButtonGoToSC01");
        MainScene.Instance.LoadView(ViewId.SC01);
    }
}
