﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public delegate void OnLevelLoaded(int levelIndex);

public delegate void OnAdditiveLevelLoaded(string levelName);


/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : MonoBehaviour
{

    public Scene currentScene;

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// 以异步-叠加方式加载场景
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="onAdditiveLevelLoaded"></param>
    public void LoadAdditiveLevelAsync(string levelName, OnAdditiveLevelLoaded onAdditiveLevelLoaded)
    {
        StartCoroutine(LoadTargetLevelAdditiveAsync(levelName, onAdditiveLevelLoaded));
    }

    /// <summary>
    /// 以异步-叠加方式加载场景(携程调用)
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="onAdditiveLevelLoaded"></param>
    /// <returns></returns>
    private IEnumerator LoadTargetLevelAdditiveAsync(string levelName, OnAdditiveLevelLoaded onAdditiveLevelLoaded)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
        {
            yield return asyncOperation;
        }
        currentScene = SceneManager.GetActiveScene();
        if (null != onAdditiveLevelLoaded)
        {
            onAdditiveLevelLoaded(levelName);
        }
        Scene scene = SceneManager.GetSceneByName(levelName);
        SceneManager.SetActiveScene(scene);
    }

    /// <summary>
    /// 以同步的方式加载场景
    /// </summary>
    /// <param name="levelName"></param>
    public void LoadLevel(string levelName)
    {
        if (SceneManager.GetActiveScene().name == levelName)
        {
            Debug.LogWarning(string.Format("名为{0}的场景已经加载过了！", levelName));
        }
        SceneManager.LoadScene(levelName);
        currentScene = SceneManager.GetActiveScene();
    }

    /// <summary>
    /// 以异步-单独的方式加载场景
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <param name="onLevelLoaded"></param>
    public void LoadLevelAsync(int levelIndex, OnLevelLoaded onLevelLoaded)
    {
        StartCoroutine(LoadTargetLevelAsync(levelIndex, onLevelLoaded));
    }

    /// <summary>
    /// 以异步-单独的方式加载场景(携程调用)
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <param name="onLevelLoaded"></param>
    /// <returns></returns>
    private IEnumerator LoadTargetLevelAsync(int levelIndex, OnLevelLoaded onLevelLoaded)
    {
        if (SceneManager.GetActiveScene().buildIndex == levelIndex)
        {
            Debug.LogWarning(string.Format("索引为{0}的场景已经加载过了！", levelIndex));
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return asyncOperation;
        }
        currentScene = SceneManager.GetActiveScene();
        if (null != onLevelLoaded)
        {
            onLevelLoaded(levelIndex);
        }
    }

    /// <summary>
    /// 以异步-单独的方式加载场景
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="onLevelLoaded"></param>
    public void LoadLevelAsync(string levelName, OnAdditiveLevelLoaded onLevelLoaded)
    {
        StartCoroutine(LoadTargetLevelAsync(levelName, onLevelLoaded));
    }

    /// <summary>
    /// 以异步-单独的方式加载场景(携程调用)
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="onLevelLoaded"></param>
    /// <returns></returns>
    private IEnumerator LoadTargetLevelAsync(string levelName, OnAdditiveLevelLoaded onLevelLoaded)
    {
        if (SceneManager.GetActiveScene().name == levelName)
        {
            Debug.LogWarning(string.Format("名为{0}的场景已经加载过了！", levelName));
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return asyncOperation;
        }
        currentScene = SceneManager.GetActiveScene();
        yield return null;
        if (null != onLevelLoaded)
        {
            onLevelLoaded(levelName);
        }
    }
}
