﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏核心管理中心
/// </summary>
public class GameManager
{

    private static GameManager instance;

    /// <summary>
    /// Launcher的Obj
    /// </summary>
    private GameObject gameLauncherObj;

    /// <summary>
    /// 系统管理器
    /// </summary>
    private ModuleMgr moduleMgr;

    /// <summary>
    /// 场景/关卡管理器
    /// </summary>
    private SceneMgr sceneMgr;

    /// <summary>
    /// 资源管理器
    /// </summary>
    private ResourceMgr resourceMgr;

    /// <summary>
    /// Lua的资源管理器
    /// </summary>
    private LuaResourceMgr luaResourceMgr;

    /// <summary>
    /// UI管理器
    /// </summary>
    private UIMgr uiMgr;

    private LuaEngine luaClient;

    private GameManager()
    {

    }

    public static GameManager GetInstance()
    {
        if (null == instance)
        {
            instance = new GameManager();
        }
        return instance;
    }

    /// <summary>
    /// 初始化游戏核心
    /// </summary>
    public void InitGameCore(GameObject gameObject)
    {
        //初始化各种管理器
        resourceMgr = ResourceMgr.GetInstance();

        //Lua的资源管理器接口
        luaResourceMgr = LuaResourceMgr.GetInstance();

        gameLauncherObj = gameObject;
        LocalDataMgr.GetInstance().LoadStartConfig(() =>
        {
            resourceMgr.Init();
        });

        uiMgr = new UIMgr();
        moduleMgr = new ModuleMgr();
        sceneMgr = gameObject.AddComponent<SceneMgr>();

        luaClient = gameObject.AddComponent<LuaEngine>();

        GameStart();
    }

    /// <summary>
    /// 游戏模块开始运行入口
    /// </summary>
    public void GameStart()
    {
        //将lua初始化移动到这里，所有的必要条件都准备好以后再初始化lua虚拟机
        //LoginModule loginModule = moduleMgr.GetModule<LoginModule>();
        //loginModule.Login();
    }

    /// <summary>
    /// 模拟 Update
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        if (null != resourceMgr)
        {
            resourceMgr.Update(deltaTime);
            luaResourceMgr.Update(deltaTime);
        }
    }

    /// <summary>
    /// 模拟 LateUpdate
    /// </summary>
    /// <param name="deltaTime"></param>
    public void LateUpdate(float deltaTime)
    {

    }

    /// <summary>
    /// 模拟 FixedUpdate
    /// </summary>
    /// <param name="fixedDeltaTime"></param>
    public void FixedUpdate(float fixedDeltaTime)
    {

    }

    public void OnApplicationQuit()
    {

    }

    public void OnApplicationPause(bool pause)
    {

    }

    public void OnApplicationFocus(bool focus)
    {

    }

    /// <summary>
    /// 获取系统管理器
    /// </summary>
    /// <returns></returns>
    public ModuleMgr GetModuleMgr()
    {
        if (null != moduleMgr)
        {
            return moduleMgr;
        }
        Debug.LogWarning("subSysMgr构造异常");
        return null;
    }

    /// <summary>
    /// 获取UI管理器
    /// </summary>
    /// <returns></returns>
    public UIMgr GetUIMgr()
    {
        if (null != uiMgr)
        {
            return uiMgr;
        }
        Debug.LogWarning("uiMgr构造异常");
        return null;
    }

    public SceneMgr GetSceneMgr()
    {
        if(null != sceneMgr)
        {
            return sceneMgr;
        }
        Debug.LogWarning("sceneMgr构造异常");
        return null;
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void ApplicationQuit()
    {
        Application.Quit();
    }

}
