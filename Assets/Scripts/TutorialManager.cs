/* Molecule Lens AR - TutorialManager.cs
 * 
 * Functions that control the tutorial sequence.
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialContainer;
    [SerializeField] private GameObject tutorialDialog;
    [SerializeField] private GameObject tutorial1Object;
    [SerializeField] private GameObject tutorial1Sphere;
    [SerializeField] private GameObject tutorial2Object;
    [SerializeField] private GameObject tutorial2Cube;
    [SerializeField] private GameObject tutorial1Success;
    [SerializeField] private GameObject tutorial2Success;
    [SerializeField] private GameObject tut1Button;
    [SerializeField] private GameObject tut2Button;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject proteinModel;
    [SerializeField] private UIController uiController;

    public void ToggleTutorialContainer()
    {
        bool isActive = tutorialContainer.activeSelf;
        tutorial2Success.SetActive(false);
        tutorialContainer.SetActive(!isActive);
        tutorialDialog.SetActive(!isActive);
        proteinModel.SetActive(isActive);
        uiController.toggleDefaultUI();
    }

    public void ToggleTutorial1()
    {
        bool isActive = tutorial1Object.activeSelf;
        tutorial1Object.SetActive(!isActive);
        if (!isActive) tutorialDialog.SetActive(false);
    }

    public void ToggleTutorial2()
    {
        bool isActive = tutorial2Object.activeSelf;
        tutorial2Object.SetActive(!isActive);
        if (!isActive) tutorial1Success.SetActive(false);
    }

    public void EnableTutorial1Success()
    {
        tut1Button.SetActive(true);
        tutorial1Object.SetActive(false);
        tutorial1Sphere.SetActive(false);
        canvas.transform.position += new Vector3(-0.15f, 0f, -0.3f);
        tutorial1Success.SetActive(true);
    }

    public void EnableTutorial2Success()
    {
        tut2Button.SetActive(true);
        tutorial2Object.SetActive(false);
        tutorial2Cube.SetActive(false);
        canvas.transform.position += new Vector3(-0.15f, 0f, -0.3f);
        tutorial2Success.SetActive(true);
    }

    public void TryTutorial1()
    {
        tutorial1Sphere.SetActive(true);
        canvas.transform.position += new Vector3(0.15f, 0f, 0.3f);
        tut1Button.SetActive(false);
    }

    public void TryTutorial2()
    {
        tutorial2Cube.SetActive(true);
        canvas.transform.position += new Vector3(0.15f, 0f, 0.3f);
        tut2Button.SetActive(false);
    }
}
