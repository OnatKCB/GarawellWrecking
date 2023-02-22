using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public float deactivationInterval = 10f;
    public List<GameObject> objectsToDeactivate = new List<GameObject>();

    private void Start()
    {
        // Start the deactivation loop
        StartCoroutine(DeactivateLoop());
    }

    IEnumerator DeactivateLoop()
    {
        while (objectsToDeactivate.Count > 0)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(deactivationInterval);
            // Choose a random index to deactivate
            int index = Random.Range(0, objectsToDeactivate.Count);
            // Deactivate the chosen object
            Debug.Log("Deactivating object at index " + index);
            objectsToDeactivate[index].SetActive(false);
            // Remove the chosen object from the list
            objectsToDeactivate.RemoveAt(index);
        }
    }
}
