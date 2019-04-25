using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OnHitEffect", menuName = "OnHitEffect")]
public class OnHitEffect : ScriptableObject
{
    public enum EffectType
    {
        colorBlink,
    }

    public EffectType effectType;

    #region ColorBlink
    public Color blinkColor;
    public float blinkTime;
    #endregion
}
