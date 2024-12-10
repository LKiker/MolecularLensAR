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
    [SerializeField] private GameObject middleContainer;
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

    // Enable/Disable middle container which contains quit dialog and pointable surface
    public void toggleQuitDialog()
    {
        Debug.Log("Active Self: " + middleContainer.activeSelf);
        if (middleContainer.activeSelf == true)
            middleContainer.SetActive(false);
        else
            middleContainer.SetActive(true);
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
