using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleType
{
    public Action<GameObject> OnTapAction { get; }
    public Action<GameObject> OnExplosionAction { get; }
    public Color CircleColor { get; }
    public bool IsProgressActive { get; }
    public Color ProgressColor { get; }
    
    public CircleType(Action<GameObject> onTapAction, Action<GameObject> onExplosionAction, Color circleColor)
    {
        OnTapAction = onTapAction;
        OnExplosionAction = onExplosionAction;
        CircleColor = circleColor;
        IsProgressActive = true;
        ProgressColor=Color.red;
    }

    public CircleType(Action<GameObject> onTapAction, Action<GameObject> onExplosionAction, Color circleColor, bool isProgressActive, Color progressColor)
    {
        OnTapAction = onTapAction;
        OnExplosionAction = onExplosionAction;
        CircleColor = circleColor;
        IsProgressActive = isProgressActive;
        ProgressColor = progressColor;
    }
}
