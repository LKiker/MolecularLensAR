/* ProteinLensAR - ProteinController.cs
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
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject backButton;
    public GameObject proteinSelection;
    private Stack<GameObject> selections = new Stack<GameObject>();
    public int selectionMode { get; private set; } = 0;
    UIController uiController;

    public Material highlightMaterial;
    private Material originalMaterial;


    // Function to initialize the hierarchy
    private void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        // Initialize default protein
        Select(proteinSelection);
    }


    // Function to select a child and isolate its own children
    public void Select(GameObject selectedObject)
    {
        // If parent is main proteinModel game object
        if (selectedObject.transform.parent == gameObject.transform)
        {
            // Go back to first selectionMode
            selectionMode = 0;
        }

        // Stop at last selection mode
        if (selectionMode > 3 || (selectionMode > 2 && uiController.viewType == 2))
            return;

        // Set parent if parent exists
        Transform parent;
        if (selectedObject.transform.parent != null)
        {
            parent = selectedObject.transform.parent;

            // Deactivate all siblings of the selectedObject (children of parent)
            foreach (Transform sibling in parent)
            {
                sibling.gameObject.SetActive(false); // Hide sibling GameObjects
            }
        }

        // Activate the selected object but hide its components from interaction
        selectedObject.SetActive(true);
        if (selectionMode < 3) // if not at last selectionMode
        {
            if (selectionMode > 0 && uiController.viewType == 2) // if not folding patterns
            {
                Debug.Log($"viewType: {uiController.viewType}; selectionMode: {selectionMode}");
                toggleComponents(selectedObject.transform, true);
            }
            else
                toggleComponents(selectedObject.transform, false);
        }

        // Activate the children of the selected object
        foreach (Transform child in selectedObject.transform)
        {
            child.gameObject.SetActive(true); // Show children GameObjects
        }

        // Store the current selection for future reset on stack
        selections.Push(selectedObject);

        // Increment Selection Mode and display back button
        selectionMode++;
        if (selectionMode > 1)
            backButton.SetActive(true);

        // Display Selection on UI
        proteinSelection = selectedObject;
        uiController.displaySelection();

        // Update the selection mode display
        uiController.updateCurrentSelectionMode();
    }

    // Select tutorial 1 sphere to go to tutorial 1 success dialog
    public void selectSphere()
    {
        uiController.enableTutorial1Success();
    }

    // Toggles components of object but keeps it active
    private void toggleComponents(Transform obj, bool state)
    {
        // Toggle mesh renderer
        MeshRenderer objRenderer = obj.GetComponent<MeshRenderer>();
        if (objRenderer != null)
            objRenderer.enabled = state;
        // Toggle rest of components
        MonoBehaviour[] comps = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour comp in comps)
        {
            comp.enabled = state;
        }
        // Ensure outline stays off
        var outline = obj.GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }


    // Function to go back one selection
    public void revertSelection()
    {
        // Check if there are previous selections
        if (selections.Count == 0)
        {
            Debug.LogWarning($"No previous selections");
            return;
        }
        else
        {
            GameObject previousSelection = selections.Pop();

            // Deactivate all children of the current selection
            foreach (Transform child in previousSelection.transform)
            {
                child.gameObject.SetActive(false);
            }

            // Get the parent of the current selection
            Transform parent = previousSelection.transform.parent;

            // Display Selection on UI
            proteinSelection = parent.gameObject;
            uiController.displaySelection();

            // Reactivate all siblings
            foreach (Transform sibling in parent)
            {
                sibling.gameObject.SetActive(true);
            }

            // Reactivate components of previousSelection
            toggleComponents(previousSelection.transform, true);

            // Clear the current selection
            previousSelection = null;

            // Decrement Selection Mode
            selectionMode--;
            if (selectionMode <= 1)
                backButton.SetActive(false);

            // Update the selection mode display
            uiController.updateCurrentSelectionMode();
        }
    }


    // Find view type variant of same protein
    public void findSimilarProtein(GameObject protein)
    {
        // Set parent if parent exists
        Transform parent;
        if (protein.transform.parent != null)
        {
            parent = protein.transform.parent;

            // Activate all siblings of the protein (children of parent)
            foreach (Transform sibling in parent)
            {
                sibling.gameObject.SetActive(true); // Show sibling GameObjects
            }
        }

        // Tag of found protein should be same as current protein
        String tagToFind = protein.tag;
        // Deactivate protein to avoid finding itself
        protein.SetActive(false);
        GameObject foundProtein = GameObject.FindGameObjectWithTag(tagToFind);

        // Deactivate all siblings of the protein (children of parent)
        parent = protein.transform.parent;
        foreach (Transform sibling in parent)
        {
            sibling.gameObject.SetActive(false); // Hide sibling GameObjects
        }
        foundProtein.SetActive(true); // ensure found protein is active 

        // Select (show) found protein for manipulation
        Select(foundProtein);
    }


    // Highlight object
    public void highlight(GameObject hoveredMesh)
    {
        // Outline mesh
        var outline = hoveredMesh.GetComponent<Outline>();
        outline.enabled = true;
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


    // Unhighlight object
    public void unhighlight(GameObject hoveredMesh)
    {
        // Remove outline
        var outline = hoveredMesh.GetComponent<Outline>();
        outline.enabled = false;
        // Change material back to original
        var renderer = hoveredMesh.GetComponent<Renderer>();
        if (renderer != null && originalMaterial != null)
        {
            renderer.material = originalMaterial; 
        }

        // Remove hovering element from UI
        uiController.updateHoveringElement("");
    }
}
