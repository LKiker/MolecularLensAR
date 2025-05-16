/* Molecule Lens AR - UIController.cs
 * 
 * Functions that control the usage of the user interface system
 * in conjunction with ProteinController.cs
 */

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class UIController: MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject leftContainer;
    [SerializeField] private GameObject rightContainer;
    [SerializeField] private GameObject middleContainer;
    [SerializeField] private GameObject quizPanelContainer;
    [SerializeField] private GameObject menuExpanded;
    [SerializeField] private GameObject proteinModel;

    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI selection;
    [SerializeField] private TextMeshProUGUI selectionModeView;
    [SerializeField] private TextMeshProUGUI viewTypeView;
    [SerializeField] private TextMeshProUGUI hoveringElement;
    [SerializeField] private TMPro.TextMeshProUGUI infoText;
    [SerializeField] private TMPro.TextMeshProUGUI infoScrollText;

    [Header("Managers")]
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private ProteinController proteinController;
    [SerializeField] private QuizManager quizManager;

    [Header("Protein Info")]
    [SerializeField] private List<InfoSO> PDBinfo;

    private Dictionary<string, InfoSO> pdbInfoMap;
    private int currentInfoIndex = 0;
    public int viewType = 1;
    InfoSO currentInfo;

    // Start is called before the first frame update
    void Start()
    {
        proteinModel.SetActive(false); // This ensures everything works before hiding the protein model
        leftContainer.SetActive(false);
        rightContainer.SetActive(false);
        middleContainer.SetActive(false);
        menuExpanded.SetActive(false);
        pdbInfoMap = PDBinfo.ToDictionary(info => info.tag);
        displayPDBInfo();
    }

    // Updates the info box with the element being hovered by raycast
    public void updateHoveringElement(string hoveredMeshTag)
    {
        hoveringElement.text = hoveredMeshTag;
    }

    // Display info dialog about selected protein element
    public void displayPDBInfo()
    {
        if (proteinController.proteinSelection == null)
            return;

        string tag = proteinController.GetRootTag();
        if (pdbInfoMap.TryGetValue(tag, out InfoSO newInfo))
        {
            if (currentInfo != newInfo)
                currentInfoIndex = 0;

            currentInfo = newInfo;
            infoText.text = currentInfo.PDBinformation[currentInfoIndex];
            infoScrollText.text = $"{currentInfoIndex + 1}/{currentInfo.PDBinformation.Length}";
        }
        else
        {
            Debug.LogWarning($"No PDB info found for tag: {tag}");
        }
    }

    // Scrolls the PDB information to the right
    public void rightScrollInfo()
    {
        if (currentInfo == null) 
            return;
        if (currentInfoIndex < currentInfo.PDBinformation.Length - 1)
        {
            currentInfoIndex++;
            displayPDBInfo();
        }
    }

    // Scrolls the PDB information to the left
    public void leftScrollInfo()
    {
        if (currentInfo == null) 
            return;
        if (currentInfoIndex > 0)
        {
            currentInfoIndex--;
            displayPDBInfo();
        }
    }

    // Select protein from the proteinList
    public void selectProteinFromList(GameObject protein)
    {
        while (proteinController.selectionMode > 1)
        {
            proteinController.revertSelection();
        }
        proteinController.Select(protein);
    }


    // Display current selection
    public void displaySelection(string molType = null)
    {
        if (proteinController.proteinSelection != null)
            selection.text = proteinController.proteinSelection.tag;
        if (!string.IsNullOrEmpty(molType))
            viewTypeView.text = molType;
        displayPDBInfo();
    }

    // Enable/Disable extended menu
    public void expandMenu()
    {
        menuExpanded.SetActive(!menuExpanded.activeSelf);
    }

    // Enable/Disable default UI
    public void toggleDefaultUI()
    {
        bool newState = !leftContainer.activeSelf;
        leftContainer.SetActive(newState);
        rightContainer.SetActive(newState);
    }

    // Enable/Disable middle container which contains quit dialog and pointable surface
    public void toggleQuitDialog()
    {
        middleContainer.SetActive(!middleContainer.activeSelf);
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

    // Starts multiple choice quiz
    public void startQuiz()
    {
        menuExpanded.SetActive(false);
        quizManager.StartQuiz();
    }
}
