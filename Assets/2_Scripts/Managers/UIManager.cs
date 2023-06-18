using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public GamePanelCanvasController gamePanelCanvas { get; private set; }
    public TotalSoldGemController gemSoldScrollView { get; private set; }

    private void Awake()
    {
        gemSoldScrollView = GetComponentInChildren<TotalSoldGemController>();
        gamePanelCanvas = GetComponentInChildren<GamePanelCanvasController>();
    }
}
