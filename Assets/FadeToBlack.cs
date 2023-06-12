using UnityEngine;
using UnityEngine.UI;

/*
 * Quick fade in
 * ref: https://forum.unity.com/threads/how-can-i-fade-in-out-a-canvas-group-alpha-color-with-duration-time.969864/#post-6311295
 * TODO: setup for scene switching
 */

public class FadeToBlack : MonoBehaviour
{
    private float desiredAlpha;
    private float currentAlpha;
    [SerializeField] private Image blackPixel;
    [SerializeField] private GameObject fadeOutCanvas;

    private void OnEnable()
    {
        currentAlpha = blackPixel.color.a;
        if (currentAlpha <= 0)
            desiredAlpha = 1f;

        if (currentAlpha >= 1)
            desiredAlpha = 0f;
    }

    void Update()
    {
        currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 2.0f * Time.deltaTime);
        blackPixel.color = new Color(1,1,1, currentAlpha);

        if (currentAlpha == desiredAlpha)
            this.gameObject.SetActive(false);
    }
}
