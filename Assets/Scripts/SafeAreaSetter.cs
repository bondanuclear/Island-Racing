using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaSetter : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    RectTransform panelSafeArea;
    ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;
    Rect currentSafeArea = new Rect();
    // Start is called before the first frame update
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();
        currentOrientation = Screen.orientation;    
        currentSafeArea = Screen.safeArea;
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        if(panelSafeArea == null) return;
        Rect safeArea = Screen.safeArea;
        Vector2 minAnchor = safeArea.position;
        Vector2 maxAnchor = safeArea.position + safeArea.size;

        minAnchor.x /= canvas.pixelRect.width;
        minAnchor.y /= canvas.pixelRect.height;

        maxAnchor.x /= canvas.pixelRect.width;
        maxAnchor.y /= canvas.pixelRect.height;
        panelSafeArea.anchorMin = minAnchor;
        panelSafeArea.anchorMax = maxAnchor;

        currentSafeArea = Screen.safeArea;
        currentOrientation = Screen.orientation;
    }
    private void Update() {
        if(currentOrientation != Screen.orientation || currentSafeArea != Screen.safeArea)
        {
            ApplySafeArea();
        }
    }
    
}
