using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastChest : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNextLevel(1f));
        }
    }

    IEnumerator LoadNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameProgress.Win();
        GameProgress.currentLevel = 1;
    }
}
