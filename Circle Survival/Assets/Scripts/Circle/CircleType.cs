using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleType
{
    public Action<GameObject> OnTapAction { get; private set; }
    public Action<GameObject> OnExplosionAction { get; private set; }
    public Color CircleColor { get; private set; }
    public bool IsProgressActive { get; private set; }
    public Color ProgressColor { get; private set; }

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
