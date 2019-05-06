using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntiyEffect", menuName = "EntiyEffect")]
public class EntityEffect : ScriptableObject
{
    public enum EffectType
    {
        colorBlink,
        explosion,
    }

    public EffectType effectType;

    #region ColorBlink
    public Color blinkColor;
    public float blinkTime;
    #endregion
}
