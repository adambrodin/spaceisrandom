/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float destructionTime, fadeOutTime;
    private TextMeshPro text => GetComponent<TextMeshPro>();
    #endregion

    private void Start()
    {
        StartCoroutine(FadeOut());
        Destroy(gameObject, destructionTime);
    }

    private IEnumerator FadeOut()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);

        while (text.color.a > 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / fadeOutTime));
            yield return null;
        }
    }
}
