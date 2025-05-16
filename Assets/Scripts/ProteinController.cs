/* Molecule Lens AR - ProteinController.cs
 * 
 * Functions that control the selection, highlight, and control of
 * the current protein structure.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ProteinController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject backButton;
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private Material highlightMaterial;

    [Header("Initial Selection")]
    public GameObject proteinSelection;

    private Stack<GameObject> selections = new Stack<GameObject>();
    private Material originalMaterial;
    public int selectionMode { get; private set; } = 0;
    public bool isGrabbing = false;

    // Function to initialize the hierarchy
    private void Start()
    {
        // Initialize default protein
        Select(proteinSelection);
    }

    // Selects and displays a GameObject in the protein hierarchy
    public void Select(GameObject selectedObject)
    {
        if (selectedObject == null) 
            return;

        // If parent is main proteinModel game object
        if (selectedObject.transform.parent == transform)
            // Go back to first selectionMode
            selectionMode = 0;

        // Get max depth of model layers
        var settings = selectedObject.GetComponent<ModelSettings>();
        int maxDepth = 3;
        if (settings != null)
            maxDepth = settings.maxSelectionDepth;
        if (selectionMode > maxDepth)
            return;

        // Deactivate siblings
        Transform parent = selectedObject.transform.parent;
        if (parent != null)
        {
            foreach (Transform sibling in parent)
                sibling.gameObject.SetActive(false);
        }
        selectedObject.SetActive(true);

        // Handle internal component visibility
        if (selectionMode < maxDepth)
        {
            bool enableComponents = selectionMode > 0 && uiController.viewType == 2;
            toggleComponents(selectedObject.transform, enableComponents);
        }

        // Activate children
        foreach (Transform child in selectedObject.transform)
            child.gameObject.SetActive(true);

        selections.Push(selectedObject);
        selectionMode++;

        if (selectionMode > 1 && backButton != null)
            backButton.SetActive(true);

        // Display Selection on UI
        proteinSelection = selectedObject;
        string molType = settings.moleculeType;
        uiController.displaySelection(molType);
    }


    // Goes back one level in the selection stack
    public void revertSelection()
    {
        // Check if there are previous selections
        if (selections.Count == 0)
        {
            Debug.LogWarning("No previous selections to revert to.");
            return;
        }

        GameObject previousSelection = selections.Pop();

        // Deactivate all children of the current selection
        foreach (Transform child in previousSelection.transform)
            child.gameObject.SetActive(false);

        // Get the parent of the current selection
        Transform parent = previousSelection.transform.parent;
        if (parent != null)
        {
            proteinSelection = parent.gameObject;
            uiController.displaySelection();

            // Reactivate all siblings
            foreach (Transform sibling in parent)
                sibling.gameObject.SetActive(true);

            // Reactivate components of previousSelection
            toggleComponents(previousSelection.transform, true);
        }

        // Decrement Selection Mode
        selectionMode--;
        if (selectionMode <= 1)
            backButton.SetActive(false);
    }

    // Ensures updating the PDBinfo looks at topmost object tag
    public string GetRootTag()
    {
        if (proteinSelection == null)
            return "";

        Transform current = proteinSelection.transform;

        // Traverse up until we reach a direct child of the root, ProteinModel
        while (current.parent != null && current.parent != transform)
        {
            current = current.parent;
        }

        return current.tag;
    }


    // Highlights an object visually and in UI
    public void highlight(GameObject hoveredMesh)
    {
        if (hoveredMesh == null)
            return;

        // Suppress highlight to fix color change bug
        var suppressor = hoveredMesh.GetComponent<SuppressHighlightOnGrabAttempt>();
        if (suppressor != null && suppressor.suppressHighlight)
            return;

        // Outline mesh
        var outline = hoveredMesh.GetComponent<Outline>();
        if (outline != null) outline.enabled = true;

        // Change material to a highlight material
        var renderer = hoveredMesh.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
            renderer.material = highlightMaterial;
        }
        // Display hovering element on UI
        uiController.updateHoveringElement(hoveredMesh.tag);
    }

    // Removes highlight from object
    public void unhighlight(GameObject hoveredMesh)
    {
        if (hoveredMesh == null) 
            return;

        // Suppress highlight to fix color change bug
        var suppressor = hoveredMesh.GetComponent<SuppressHighlightOnGrabAttempt>();
        if (suppressor != null && suppressor.suppressHighlight)
            return;

        // Remove outline
        var outline = hoveredMesh.GetComponent<Outline>();
        if (outline != null) outline.enabled = false;

        // Change material back to original
        var renderer = hoveredMesh.GetComponent<Renderer>();
        if (renderer != null && originalMaterial != null)
        {
            renderer.material = originalMaterial;
        }

        // Remove hovering element from UI
        uiController.updateHoveringElement("");

    }

    // Enables tutorial success when selecting the sphere
    public void selectSphere()
    {
        tutorialManager.EnableTutorial1Success();
    }

    // Enables/disables visual components on a GameObject
    private void toggleComponents(Transform obj, bool state)
    {
        // Toggle mesh renderer
        var meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
            meshRenderer.enabled = state;

        // Toggle rest of components
        foreach (var comp in obj.GetComponents<MonoBehaviour>())
            comp.enabled = state;

        // Ensure outline stays off
        var outline = obj.GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }
}
