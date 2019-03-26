using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] public int numberOfBlocks = 0;

    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        numberOfBlocks++;
    }

    public void BrokenBlock()
    {
        numberOfBlocks--;
        if (numberOfBlocks == 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
