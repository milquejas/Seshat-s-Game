using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * Quick fade in
 * old ref: https://forum.unity.com/threads/how-can-i-fade-in-out-a-canvas-group-alpha-color-with-duration-time.969864/#post-6311295
 * TODO: setup for scene switching
 */

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private Image blackPixel;
    [SerializeField] private GameObject fadeOutCanvas;

    public void ToggleFadeToBlack(float duration)
    {
        fadeOutCanvas.SetActive(true);

        float currentAlpha = blackPixel.color.a;
        float targetAlpha = 0;
        if (blackPixel.color.a == 0)
            targetAlpha = 1;

        StartCoroutine(StartFade(duration, targetAlpha, currentAlpha, blackPixel));
    }

    private IEnumerator StartFade(float duration, float targetAlpha, float currentAlpha, Image image)
    {
        float elapsedTime = 0f;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsedTime / duration);

            image.color = Color.LerpUnclamped(new Color(1, 1, 1, currentAlpha), new Color(1, 1, 1, targetAlpha), percent);
            yield return null;
        }
        if (targetAlpha <= 0)
            fadeOutCanvas.SetActive(false);
    }
}
