using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleType
{
    private readonly Action<GameObject> _onTapAction;
    private readonly Action<GameObject> _onExplosionAction;
    private readonly Color _circleColor;
    private readonly bool _isProgressActive;
    private readonly Color _progressColor;

    public CircleType(Action<GameObject> onTapAction, Action<GameObject> onExplosionAction, Color circleColor)
    {
        _onTapAction = onTapAction;
        _onExplosionAction = onExplosionAction;
        _circleColor = circleColor;
        _isProgressActive = true;
        _progressColor=Color.red;
    }

    public CircleType(Action<GameObject> onTapAction, Action<GameObject> onExplosionAction, Color circleColor, bool isProgressActive, Color progressColor)
    {
        _onTapAction = onTapAction;
        _onExplosionAction = onExplosionAction;
        _circleColor = circleColor;
        _isProgressActive = isProgressActive;
        _progressColor = progressColor;
    }

    public Action<GameObject> OnTapAction
    {
        get { return _onTapAction; }
    }

    public Action<GameObject> OnExplosionAction
    {
        get { return _onExplosionAction; }
    }

    public Color CircleColor
    {
        get { return _circleColor; }
    }

    public bool IsProgressActive
    {
        get { return _isProgressActive; }
    }

    public Color ProgressColor
    {
        get { return _progressColor; }
    }
}
