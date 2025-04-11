/* ProteinLensAR - UIController.cs
 * 
 * Functions that control the usage of the user interface system
 * in conjunction with ProteinController.cs
 */

using UnityEngine;
using TMPro;
using UnityEditor;

public class UIController: MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject tut1Button;
    [SerializeField] private GameObject tut2Button;
    [SerializeField] private GameObject leftContainer;
    [SerializeField] private GameObject rightContainer;
    [SerializeField] private GameObject middleContainer;
    [SerializeField] private GameObject tutorialContainer;
    [SerializeField] private GameObject tutorialDialog;
    [SerializeField] private GameObject tutorial1Object;
    [SerializeField] private GameObject tutorial1Sphere;
    [SerializeField] private GameObject tutorial2Object;
    [SerializeField] private GameObject tutorial2Cube;
    [SerializeField] private GameObject tutorial1Success;
    [SerializeField] private GameObject tutorial2Success;
    [SerializeField] private GameObject menuExpanded;
    [SerializeField] private GameObject proteinModel;
    [SerializeField] private TextMeshProUGUI selection;
    [SerializeField] private TextMeshProUGUI selectionModeView;
    [SerializeField] private TextMeshProUGUI viewTypeView;
    [SerializeField] private TextMeshProUGUI hoveringElement;
    public int viewType = 1;
    ProteinController proteinController;
    

    // Start is called before the first frame update
    void Start()
    {
        proteinController = GameObject.FindGameObjectWithTag("ProteinController").GetComponent<ProteinController>();
        proteinModel.SetActive(false); // This ensures everything works before hiding the protein model
    }

    // Updates current selection mode in the info box
    public void updateCurrentSelectionMode()
    {
        switch (proteinController.selectionMode)
        {
            // Amino Acid Chains
            case 1:
                if (viewType == 1)
                    selectionModeView.text = "Amino Acid Chains";
                else
                    selectionModeView.text = "Folding Patterns";
                break;
            // Amino Acid Residues
            case 2:
                if (viewType == 1)
                    selectionModeView.text = "Amino Acid Residues";
                else
                    selectionModeView.text = "Folds";
                break;
            // Elements
            case 3:
            case 4:
                if (viewType == 1)
                    selectionModeView.text = "Elements";
                break;
            default:
                selectionModeView.text = "";
                break;
        }
    }

    // Updates the info box with the element being hovered by raycast
    public void updateHoveringElement(string hoveredMeshTag)
    {
        hoveringElement.text = hoveredMeshTag;
    }

    // Display info dialog about selected protein element
    public void displayProteinInfo()
    {
        // To-do
    }

    // Select protein from the proteinList
    public void selectProteinFromList(GameObject protein)
    {
        resetTransform();
        proteinController.Select(protein);
    }

    // Select view type from the View Type list
    public void selectViewTypeFromList(GameObject viewTypeSelection)
    {
        if (viewTypeSelection.name.Equals("Amino Acids"))
        {
            viewType = 1;
        }
        else
        {
            viewType = 2;
        }
        viewTypeView.text = viewTypeSelection.name; // Update viewType info
        resetTransform();
        GameObject protein = proteinController.proteinSelection;
        proteinController.findSimilarProtein(protein);
    }

    // Display current selection
    public void displaySelection()
    {
        selection.text = proteinController.proteinSelection.tag;
    }

    // Enable/Disable extended menu
    public void expandMenu()
    {
        Debug.Log("Active Self: " + menuExpanded.activeSelf);
        if (menuExpanded.activeSelf == true)
            menuExpanded.SetActive(false);
        else
            menuExpanded.SetActive(true);
    }

    // Enable/Disable default UI
    public void toggleDefaultUI()
    {
        Debug.Log("Active Self: " + leftContainer.activeSelf);
        if (leftContainer.activeSelf == true)
        {
            leftContainer.SetActive(false);
            rightContainer.SetActive(false);
        }
        else
        {
            rightContainer.SetActive(true);
            leftContainer.SetActive(true);
        }
    }

    // Toggle tutorial container 
    public void toggleTutorialContainer()
    {
        Debug.Log("Active Self: " + tutorialContainer.activeSelf);
        if (tutorialContainer.activeSelf == true)
        {
            tutorial2Success.SetActive(false);
            tutorialContainer.SetActive(false);
        }   
        else
        { 
            tutorialContainer.SetActive(true);
            tutorialDialog.SetActive(true);
        }

        toggleDefaultUI();
        if (proteinModel.activeSelf == true)
            proteinModel.SetActive(false);
        else
            proteinModel.SetActive(true);
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

    // Toggle Tutorial 1
    public void toggleTutorial1()
    {
        Debug.Log("Active Self: " + tutorial1Object.activeSelf);
        if (tutorial1Object.activeSelf == false)
        {
            tutorial1Object.SetActive(true);
            tutorialDialog.SetActive(false);
        }
        else
            tutorial1Object.SetActive(false);
    }

    // Enable tutorial 1 success dialog
    public void enableTutorial1Success()
    {
        tut1Button.SetActive(true);
        tutorial1Object.SetActive(false);
        tutorial1Sphere.SetActive(false);
        canvas.transform.position += new Vector3(-0.15f, 0f, -0.3f);
        tutorial1Success.SetActive(true);
    }

    // Toggle Tutorial 2
    public void toggleTutorial2()
    {
        Debug.Log("Active Self: " + tutorial2Object.activeSelf);
        if (tutorial2Object.activeSelf == false)
        {
            tutorial2Object.SetActive(true);
            tutorial1Success.SetActive(false);
        }
        else
            tutorial2Object.SetActive(false);
    }

    // Enable tutorial 2 success dialog
    public void enableTutorial2Success()
    {
        tut2Button.SetActive(true);
        tutorial2Object.SetActive(false);
        tutorial2Cube.SetActive(false);
        canvas.transform.position += new Vector3(-0.15f, 0f, -0.3f);
        tutorial2Success.SetActive(true);
    }

    // Makes sphere active for tutorial 1 try it
    public void tryTutorial1()
    {
        tutorial1Sphere.SetActive(true);
        canvas.transform.position += new Vector3(0.15f, 0f, 0.3f);
        tut1Button.SetActive(false);a
    }

    // Makes cube active for tutorial 2 try it
    public void tryTutorial2()
    {
        tutorial2Cube.SetActive(true);
        canvas.transform.position += new Vector3(0.15f, 0f, 0.3f);
        tut2Button.SetActive(false);
    }

    // Resets the transform of the parent protein model object
    public void resetTransform()
    {
        while (proteinController.selectionMode > 1)
        {
            proteinController.revertSelection();
        }
        proteinModel.transform.position = Vector3.zero;
        proteinModel.transform.rotation = Quaternion.identity;
        proteinModel.transform.localScale = Vector3.one;
    }

    // Reverts to previous selection
    public void backSelection()
    {
        proteinController.revertSelection();
    }

    // Quits the application
    public void quitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
