using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public GameObject middleContainer;
    public GameObject menuExpanded;
    public GameObject currentModel;

    // Enable/Disable Menu_Extended box
    public void expandMenu()
    {
        Debug.Log("Active Self: " + menuExpanded.activeSelf);
        if (menuExpanded.activeSelf == true)
            menuExpanded.SetActive(false);
        else
            menuExpanded.SetActive(true);
    }

    // Enable/Disable middle container which contains quit dialog and pointable surface
    public void toggleQuitDialog()
    {
        Debug.Log("Active Self: " + middleContainer.activeSelf);
        if (middleContainer.activeSelf == true)
            middleContainer.SetActive(false);
        else
            middleContainer.SetActive(true);
    }

    // Resets the transform of the current protein model
    public void resetTransform()
    {
        currentModel.transform.position = Vector3.zero;
        currentModel.transform.rotation = Quaternion.identity;
        currentModel.transform.localScale = Vector3.one;
    }

    // Quits the application
    public void quitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
