using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelMode_Ref : MonoBehaviour
{
    public int sceneBuildIndex;
    public LevelLoader levelLoader;
    private void OnTriggerEnter2D(Collider2D other)
    {
        levelLoader.LoadNextLevel(sceneBuildIndex);
        //print("Switching Scene to " + sceneBuildIndex);
        //SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

    }
}
