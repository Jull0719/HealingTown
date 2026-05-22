using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
#if UNITY_EDITOR
        //使用Unity进行调试时
        UnityEditor.EditorApplication.isPlaying = false;
        //打包发布后
        #else
            Application.Quit();
        #endif
    }

}
