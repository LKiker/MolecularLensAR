# MolecularLensAR

**MolecularLensAR** is a mixed-reality educational tool built for the **Meta Quest 3**.  
It places a 3D protein model into the user’s physical environment and enables interactive learning through direct manipulation, click-into-model exploration, and a built-in multiple-choice quiz.

The goal of the project is to make molecular structures intuitive to understand through immersive spatial visualization.

<div align="center">
  <img src="Assets/Media/molecular_lens_demo.gif" width="700" />
</div>

---

## Features

### 1. Click-Into-Model Exploration
- Users can select individual parts of the protein to explore deeper structural layers.
- Each selection updates the model state and highlights the currently active region.
- Designed to make complex molecular structures easier to follow.

### 2. Always-Visible Information Panel
- The information panel remains open at all times.
- Automatically updates based on the user’s current selection or model layer.
- Displays names, details, and simple descriptive information.

### 3. Guided Mixed-Reality Tutorial
- A step-by-step onboarding sequence teaches:
  - How to grab, rotate, and scale the model
  - How to select different structures
  - How the information panel works
  - How to use quiz mode
- Ensures VR beginners can interact without confusion.

### 4. Multiple-Choice Quiz Mode
- Users answer conceptual questions through on-screen multiple-choice buttons.
- Provides immediate correct/incorrect feedback.
- Does not track cumulative score; designed for guided learning.

---

## Technology
- Unity (C#)
- Meta XR SDK
- XR Interaction Toolkit
- Mixed Reality Passthrough
- Universal Render Pipeline (URP)

---

## Installation on Meta Quest 3

### 1. Download the APK
Download `MolecularLensAR.apk` from the Releases section of this repository.

### 2. Install SideQuest
https://sidequestvr.com/

### 3. Connect Your Headset
- Enable Developer Mode on your Meta Quest device
- Connect your Quest 3 to your PC via USB

### 4. Install the APK via SideQuest
- Open SideQuest
- Select **Install APK from file**
- Choose `MolecularLensAR.apk`

### 5. Launch the Application
On your Quest, navigate to:
Apps → Unknown Sources → MolecularLensAR
