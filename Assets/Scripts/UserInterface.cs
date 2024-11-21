using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
