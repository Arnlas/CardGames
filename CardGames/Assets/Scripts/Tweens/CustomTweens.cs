using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class CustomTweens
{
    public static void Pos2PosScale(this Transform target, Vector3 pos, Vector3 rotation, Vector3 scale, float time, Action action)
    {
        target.DOKill();
        target.DOScale(scale, time);
        target.DORotate(rotation, time);
        Tween move = target.DOMove(pos, time);
        move.onComplete += ()=>action.Invoke();
    }
    public static void Pos2PosArc(this Transform target, Vector3 center, float radius, float angleRange, int count, int order, float time, bool lookUp = false)
    {
        target.DOKill();
        Vector3 upDir = lookUp ? Vector3.up : Vector3.down;
        float flip = lookUp ? 0.0f : 1.0f;
        Vector3 initDir = Quaternion.Euler(0, 0, angleRange / 2.0f) * upDir;
        float step = angleRange / (count + 1);
        Vector3 rotation = new Vector3(0, 0, angleRange / 2.0f - (order + 1) * step + 180 * flip);
        Vector3 curDir = Quaternion.Euler(0, 0, -(order + 1) * step) * initDir.normalized;
        Vector3 targetPosition = (center - upDir * radius) + curDir * radius;

        target.DOMove(targetPosition, time);
        target.DORotate(rotation, time);
    }
    
    
    
    public static void Origin2Pos(Transform target, Vector3 targetPosition, Vector3 targetRotation, float time)
    {
        target.DOMove(targetPosition, time);
        Tween rotate = target.DORotate(new Vector3(0, -90, 0) + targetRotation / 2, time / 2.0f);
        rotate.onComplete += () => { rotate = target.DORotate(targetRotation, time / 4.0f);};
    }
}
