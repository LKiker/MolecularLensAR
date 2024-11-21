using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackUI : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Transform UI;

    // Update is called once per frame
    void Update()
    {
        if(UI)
        {
            transform.position = MainCamera.WorldToScreenPoint(UI.position);
        }
    }
}
