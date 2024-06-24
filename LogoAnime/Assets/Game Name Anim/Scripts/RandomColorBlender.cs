using UnityEngine;
using System.Collections;

public class RandomColorBlender : MonoBehaviour
{
    public Color[] colors; // Array of colors to blend between
    public float blendDuration = 2f; // Time to blend between colors

    private Renderer _targetRenderer;
    private Coroutine _colorCoroutine;
    private bool _isPaused = false;

    private void Start()
    {
        // Get the Renderer component from the target object
        _targetRenderer = GetComponent<Renderer>();

        if (colors.Length > 0)
        {
            // Start the color blending coroutine
            Play();
        }
        else
        {
            Debug.LogError("No colors specified for blending.");
        }
    }

    private IEnumerator BlendColors()
    {
        while (true)
        {
            // Choose a random color from the array
            Color startColor = _targetRenderer.material.color;
            Color targetColor = colors[Random.Range(0, colors.Length)];

            float elapsedTime = 0f;

            while (elapsedTime < blendDuration)
            {
                if (!_isPaused)
                {
                    _targetRenderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / blendDuration);
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }

            // Ensure the final color is set
            _targetRenderer.material.color = targetColor;

            // Wait for a short duration before choosing the next color
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Play()
    {
        if (_colorCoroutine == null)
        {
            _colorCoroutine = StartCoroutine(BlendColors());
        }
        else
        {
            _isPaused = false;
        }
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Stop()
    {
        if (_colorCoroutine != null)
        {
            StopCoroutine(_colorCoroutine);
            _colorCoroutine = null;
        }
    }
}
