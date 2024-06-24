using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialAnimator : MonoBehaviour
{
    // List to hold your GameObjects with Animator components
    public List<Animation> gameObjects;
    public float delay = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to play animations sequentially
        StartCoroutine(PlayDropInAnimationsSequentially());
    }

    // Coroutine to play dropIn animations sequentially
    IEnumerator PlayDropInAnimationsSequentially()
    {
        foreach (var obj in gameObjects)
        {
            // Animator animator = obj.GetComponent<Animator>();

            if (obj != null)
            {
                obj.Play("dropIn");

                yield return new WaitForSeconds(delay);
            }
        }
    }

    [ContextMenu("Drop In animation")]
    public void DropIn()
    {
        StartCoroutine(PlayDropInAnimationsSequentially());
    }
    
    [ContextMenu("Drop Out animation")]
    public void DropOut()
    {
        StartCoroutine(PlayDropOutAnimationsSequentially());
    }
    
    // Coroutine to play dropOut animations sequentially
    IEnumerator PlayDropOutAnimationsSequentially()
    {
        for (int i = gameObjects.Count-1; i >= 0 ; i--)
        {
            if (gameObjects[i] != null)
            {
                gameObjects[i].Play("DropOut");
            }
            yield return new WaitForSeconds(delay);
            
        }
    }
}
