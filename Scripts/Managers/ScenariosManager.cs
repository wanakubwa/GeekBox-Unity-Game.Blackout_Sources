using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using BOBCheats.Utils;
using UnityEngine.SceneManagement;
using System;
using SaveLoadSystem;

public class ScenariosManager : ManagerSingletonBase<ScenariosManager>
{
    #region Fields

    [SerializeField, ReadOnly]
    private string currentScenarioName = string.Empty;
    [SerializeField, ReadOnly]
    private ScenarioInfo currentScenarioInfo;

    #endregion

    #region Propeties

    public event Action OnGameScenarioLoaded = delegate { };
    public event Action OnEditorScenarioLoaded = delegate { };
    public event Action OnScenarioInfoUpdate = delegate { };

    public string CurrentScenarioDirectoryName { 
        get => currentScenarioName; 
        private set => currentScenarioName = value; 
    }

    public ScenarioInfo CurrentScenarioInfo
    {
        get => currentScenarioInfo;
        private set => currentScenarioInfo = value;
    }

    #endregion

    #region Methods

    public void SetScenarioInfo(ScenarioInfo info)
    {
        CurrentScenarioInfo = info;
        OnScenarioInfoUpdate();
    }

    public string[] GetScenariosEditorNames()
    {
        //todo: jak bedzie koniecznosc podzialu na scenariusze do edycji na urzadzeniu trzeba rozdzielic tu.
        return ScenarioFilePath.GetScenariosNames();
    }

    public int GetTotalScenariosCount()
    {
        return ScenariosContentSettings.Instance.ScenariosCollection.Count;
    }

    public ScenarioInfo GetNextScenarioInfo()
    {
        int nextLvlNo = CurrentScenarioInfo.OrderNo + 1;
        List<ScenarioInfo> scenariosCollection = ScenariosContentSettings.Instance.ScenariosCollection;
        foreach (ScenarioInfo info in scenariosCollection)
        {
            if(info.OrderNo == nextLvlNo)
            {
                return info;
            }
        }

        return null;
    }

    public void RestartCurrentLvl()
    {
        LoadOfficialScenario(GameManager.Instance.Managers, CurrentScenarioInfo.ScenarioDirectoryName);
    }

    public void CreateNewEditorScenario()
    {
        SetScenarioInfo(ScenarioInfo.GetDefault());
        StartCoroutine(_InitializeNewScenarioForEditor(GameManager.Instance.Managers));
    }

    public void SaveEditorScenario(string directoryName, string scenarioNameKey, string scenarioId)
    {
        CurrentScenarioDirectoryName = directoryName;
        string savePath = ScenarioFilePath.GetTargetScenarioPath(CurrentScenarioDirectoryName);
        CheckPathExists(savePath);

#if UNITY_EDITOR
        ScenarioFilePath.CreateScenarioInfoFile(CurrentScenarioDirectoryName, scenarioNameKey, scenarioId);
        // Wyslanie informacji do settingsa o aktualizacji scenariuszy.
        ScenariosContentSettings.Instance.RefreshScenarios();
#endif

        StartCoroutine(_SaveScenarioAtPath(GameManager.Instance.Managers, savePath));
    }

    /// <summary>
    /// Zapis aktualnego postepu w rozgrywanym przez gracza scenariuszu.
    /// </summary>
    public void SaveGameScenario(List<IManager> managers)
    {
        string savePath = ScenarioFilePath.GetScenarioPlayerSavePath();
        CheckPathExists(savePath);

        StartCoroutine(_SaveScenarioAtPath(managers, savePath));
    }

    public void LoadEditorScenario(string scenarioDirectory)
    {
        SetScenarioInfo(ScenariosContentSettings.Instance.GetScenarioInfoByDirectory(scenarioDirectory));
        string path = GetCurrentScenarioSavePath();

        if (Directory.Exists(path) == false)
        {
            Debug.LogErrorFormat("Brak scenariusza o sciezce {0}", path);
            return;
        }

        StartCoroutine(_LoadScenarioFromEditor(GameManager.Instance.Managers, path));
    }

    public void LoadSavedOfficialScenario(List<IManager> managers)
    {
        string path = ScenarioFilePath.GetScenarioPlayerSavePath();
        if (Directory.Exists(path) == false)
        {
            Debug.LogErrorFormat("Brak zapisanego scenariusza o sciezce {0}".SetColor(Color.red), path);
            return;
        }

        StartCoroutine(_LoadTargetScene(managers, path, SceneLabel.GAME));
    }

    public void LoadOfficialScenario(List<IManager> managers, string scenarioDirectoryName)
    {
        CurrentScenarioDirectoryName = scenarioDirectoryName;
        SetScenarioInfo(ScenariosContentSettings.Instance.GetScenarioInfoByDirectory(scenarioDirectoryName));

        string path = GetCurrentScenarioSavePath();
        if (Directory.Exists(path) == false)
        {
            Debug.LogErrorFormat("Brak scenariusza o sciezce {0}", path);
            return;
        }

        StartCoroutine(_LoadTargetScene(managers, path, SceneLabel.GAME));
    }

    public void LoadMenuScene(List<IManager> managers)
    {
        StartCoroutine(_LoadTargetScene(managers, string.Empty, SceneLabel.MAIN_MENU));
    }

    private string GetCurrentScenarioSavePath()
    {
        return ScenarioFilePath.GetTargetScenarioPath(CurrentScenarioInfo.ScenarioDirectoryName);
    }

    private void CheckPathExists(string path)
    {
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }
    }

    private IEnumerator _SaveScenarioAtPath(List<IManager> managers, string path)
    {
        yield return null;

        foreach (IManager manager in managers)
        {
            if (manager is IScenarioSaveable scenarioSaveable)
            {
                scenarioSaveable.Save(path);
            }

            yield return null;
        }

        yield return null;
    }

    private IEnumerator _InitializeNewScenarioForEditor(List<IManager> managers)
    {
        yield return null;

        foreach (IManager manager in managers)
        {
            if(manager is IContentLoadable loadable)
            {
                loadable.UnloadContent();
            }

            if (manager is IScenarioSaveable scenarioSaveable)
            {
                scenarioSaveable.CreateNewScenario();
            }

            yield return null;
        }

        yield return null;
    }

    private IEnumerator _LoadScenarioFromEditor(List<IManager> managers, string path)
    {
        //todo loading screen.

        yield return null;

        foreach (IManager manager in managers)
        {
            if (manager is IContentLoadable contentLoadable)
            {
                contentLoadable.UnloadContent();
            }

            if (manager is IScenarioSaveable scenarioSaveable)
            {
                scenarioSaveable.Load(path);
            }

            yield return null;
        }

        // Zapobieganie powstawaniu zaleznosci miedzy managerami.
        foreach (IManager manager in managers)
        {
            if (manager is IContentLoadable contentLoadable)
            {
                contentLoadable.LoadContent();
            }
        }

        SceneManager.LoadScene(ManagersContentSetup.Instance.GetSceneIndexByLabel(SceneLabel.SCENARIO_EDITOR), LoadSceneMode.Single);
        Debug.LogFormat("Zaladowano scenariusz {0}".SetColor(Color.green), path);

        OnEditorScenarioLoaded();

        yield return null;
    }

    private IEnumerator _LoadTargetScene(List<IManager> managers, string path, SceneLabel sceneToLoad)
    {
        int loadingProgress = Constants.DEFAULT_VALUE;
        LoadingPopUpController loadingPopUpController = PopUpManager.Instance.ShowLoadingPopUp(managers.Count * 2);
        yield return null;

        string savePath = SaveLoadManager.Instance.Path.DataSavePath;

        foreach (IManager manager in managers)
        {
            try
            {
                bool hasContentOnScene = manager.HasContentOnScene(sceneToLoad);

                if (manager is IContentLoadable contentLoadable)
                {
                    contentLoadable.UnloadContent();
                }

                if (manager is IScenarioSaveable scenarioSaveable)
                {
                    if (hasContentOnScene == true)
                    {
                        scenarioSaveable.Load(path);
                    }
                }
                else
                {
                    // Managery nie zapisywane dla kazdego scenariusza tylko ogolnie dla calosci gry.
                    if (manager is ISaveable saveableManager)
                    {
                        if (hasContentOnScene == true)
                        {
                            if (saveableManager.IsLoaded == true)
                            {
                                saveableManager.Save(savePath);
                            }
                            
                            saveableManager.Load(savePath);
                        }
                        else if(saveableManager.IsLoaded == true)
                        {
                            saveableManager.Save(savePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("{0}!: {1}", ex.Message, ex.StackTrace);
            }

            loadingPopUpController.RefreshPopUp(++loadingProgress);
            yield return new WaitForSeconds(0.05f);
        }

        // Zapobieganie powstawaniu zaleznosci miedzy managerami podczas ladowania kontentu.
        foreach (IManager manager in managers)
        { 
            if (manager is IContentLoadable contentLoadable)
            {
                if (manager.HasContentOnScene(sceneToLoad) == true)
                {
                    contentLoadable.LoadContent();
                }
            }

            loadingPopUpController.RefreshPopUp(++loadingProgress);
            yield return new WaitForSeconds(0.05f);
        }

        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(ManagersContentSetup.Instance.GetSceneIndexByLabel(sceneToLoad), LoadSceneMode.Single);
        sceneLoading.allowSceneActivation = false;

        while (sceneLoading.isDone == false)
        {
            if (sceneLoading.progress >= 0.9f)
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        sceneLoading.allowSceneActivation = true;
        yield return new WaitForFixedUpdate();
        Debug.LogFormat("Zaladowano scene {0}".SetColor(Color.green), sceneToLoad);

        if (sceneToLoad == SceneLabel.GAME)
        {
            OnGameScenarioLoaded();
        }

        yield return null;
    }

    #endregion

    #region Enums

    #endregion
}
