using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnim : MonoBehaviour
{
    float intHoverSpeed = 0.5f; // Speed of the hover effect
    float intHoverRange = 0.5f; // Range of the hover effect
    float intRotationSpeed = 10.0f;
    
    
    public float hoverSpeed = 0.5f; // Speed of the hover effect
    public float hoverRange = 0.5f; // Range of the hover effect
    public float rotationSpeed = 10.0f; // Speed of the rotation

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float randomOffsetX;
    private float randomOffsetY;
    private float randomOffsetZ;

    private Coroutine interpolationCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        // Generate random offsets for Perlin noise to ensure different objects move differently
        randomOffsetX = Random.Range(0f, 100f);
        randomOffsetY = Random.Range(0f, 100f);
        randomOffsetZ = Random.Range(0f, 100f);
        
        // Start the interpolation coroutine
        interpolationCoroutine = StartCoroutine(ChangeValuesOverTime(3f));
    }

    // Update is called once per frame
    void Update()
    {
        HoverEffect();
    }

    void HoverEffect()
    {
        float time = Time.time * hoverSpeed;

        // Use Perlin noise to generate smooth, random movement
        float offsetX = (Mathf.PerlinNoise(time + randomOffsetX, 0) - 0.5f) * hoverRange;
        float offsetY = (Mathf.PerlinNoise(time + randomOffsetY, 1) - 0.5f) * hoverRange;
        float offsetZ = (Mathf.PerlinNoise(time + randomOffsetZ, 2) - 0.5f) * hoverRange;

        // Apply the offsets to the initial position
        Vector3 newPosition = initialPosition + new Vector3(offsetX, offsetY, offsetZ);

        // Use Perlin noise to generate smooth, random rotation
        float rotationOffsetX = (Mathf.PerlinNoise(time + randomOffsetX, 3) - 0.5f) * rotationSpeed;
        float rotationOffsetY = (Mathf.PerlinNoise(time + randomOffsetY, 4) - 0.5f) * rotationSpeed;
        float rotationOffsetZ = (Mathf.PerlinNoise(time + randomOffsetZ, 5) - 0.5f) * rotationSpeed;
        Quaternion rotationOffset = Quaternion.Euler(rotationOffsetX, rotationOffsetY, rotationOffsetZ);

        // Apply the rotation offsets to the initial rotation
        Quaternion newRotation = initialRotation * rotationOffset;

        // Apply the new position and rotation
        transform.position = newPosition;
        transform.rotation = newRotation;
    }
    
    IEnumerator ChangeValuesOverTime(float time)
    {
        float elapsedTime = 0f;

        // Store initial values
        float startHoverSpeed = hoverSpeed;
        float startHoverRange = hoverRange;
        float startRotationSpeed = rotationSpeed;

        while (elapsedTime < time)
        {
            hoverSpeed = Mathf.Lerp(startHoverSpeed, intHoverSpeed, elapsedTime / time);
            hoverRange = Mathf.Lerp(startHoverRange, intHoverRange, elapsedTime / time);
            rotationSpeed = Mathf.Lerp(startRotationSpeed, intRotationSpeed, elapsedTime / time);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final values are set
        hoverSpeed = intHoverSpeed;
        hoverRange = intHoverRange;
        rotationSpeed = intRotationSpeed;
    }
}
