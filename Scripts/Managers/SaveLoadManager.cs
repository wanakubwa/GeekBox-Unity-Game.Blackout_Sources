using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;

using Debug = UnityEngine.Debug;
using SaveLoadSystem;
using GeekBox.OdinSerializer;

public class SaveLoadManager : ManagerSingletonBase<SaveLoadManager>
{
    #region Fields

    [Space, Title("Ustawienia plikow")]
    [SerializeField]
    private string fileExtension = ".blackOut";

    #endregion
    #region Propeties

    public event Action OnResetGame = delegate { };
    public event Action OnLoadGame = delegate { };
    public event Action OnSaveGame = delegate { };
    public event Action OnResetCompleted = delegate { };
    public event Action OnSaveCompleted = delegate { };
    public event Action OnLoadCompleted = delegate { };

    public string FileExtension
    {
        get => fileExtension;
        private set => fileExtension = value;
    }

    public PathControllerBase Path
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();

#if (UNITY_ANDROID && !UNITY_EDITOR)
        Path = new AndroidPathController();
#else
        Path = new PathControllerBase();
#endif

        Path.Init();
    }

    public string GetSavePathSubfolder(string folderName)
    {
        string subfolderPath = Path.DataSavePath +  System.IO.Path.AltDirectorySeparatorChar + folderName;
        if(Directory.Exists(subfolderPath) == false)
        {
            Directory.CreateDirectory(subfolderPath);
        }

        return subfolderPath;
    }

    public void DeleteCurrentSaveFiles()
    {
        Path.ResetDataPathContent();
    }

    public void SaveGameAtEndScenario(List<IManager> managers, SceneLabel currentScene)
    {
        for (int i = 0; i < managers.Count; i++)
        {
            if (managers[i] is ISaveable saveable && (managers[i] is IScenarioSaveable) == false)
            {
                if(managers[i].HasContentOnScene(currentScene) == true)
                {
                    saveable.Save(Path.DataSavePath);
                    Debug.LogFormat("Manager: {0} - zapisany po zakonczeniu scenariusza".SetColor(Color.magenta), managers[i].GetType().Name);
                }
            }
        }

        // Usuwanie danych gracza z zapisu scenariusza poniewaz scenariusz sie zakonczyl.
        ScenarioFilePath.ClearScenarioPlayerSave();
        PlayerManager.Instance.Wallet.SetSavedLvlId(Constants.DEFAULT_ID);

        OnSaveCompleted();
    }

    public void SaveGame(List<IManager> managers, SceneLabel currentScene)
    {
        ////// WYLACZENIE ZAPISU STANU MAPY
        //string saveScenarioPath = ScenarioFilePath.GetScenarioPlayerSavePath();
        //if(currentScene == SceneLabel.GAME)
        //{
        //    if(Directory.Exists(saveScenarioPath) == true)
        //    {
        //        Directory.Delete(saveScenarioPath, true);
        //    }
            
        //    Directory.CreateDirectory(saveScenarioPath);

        //    // Informacja o zapisanym scenariuszu.
        //    PlayerManager.Instance.Wallet.SetSavedLvlId(ScenariosManager.Instance.CurrentScenarioInfo.ScenarioId);
        //}

        for (int i = 0; i < managers.Count; i++)
        {
            bool hasContent = managers[i].HasContentOnScene(currentScene);

            if (managers[i] is IScenarioSaveable scenarioSaveable && currentScene == SceneLabel.GAME)
            {
                ////// WYLACZENIE ZAPISU STANU MAPY
                //scenarioSaveable.Save(saveScenarioPath);
            }
            else if (managers[i] is ISaveable saveable)
            {
                if (hasContent == true)
                {
                    saveable.Save(Path.DataSavePath);
                }
            }
        }

        OnSaveCompleted();
    }

    public void ResetGame(List<IManager> managers)
    {
        for(int i =0; i < managers.Count; i++)
        {
            if(managers[i] is ISaveable saveable)
            {
                saveable.ResetGameData();
            }

            Debug.LogFormat("Manager: {0} - zresetowany".SetColor(Color.magenta), managers[i].GetType().Name);
        }

        DeleteCurrentSaveFiles();
        OnResetCompleted();
    }

    /// <summary>
    /// Zapis pamiatki managera pod wskazana sciezke.
    /// </summary>
    /// <typeparam name="U">Typ pamiatki</typeparam>
    /// <param name="fileName">Nazwa pliku z pamiatka, ktory zostanie utworzony.</param>
    /// <param name="directoryPath">Ssciezka do katalogu, w ktorym utworzyc plik zapisu.</param>
    public void SaveManager<U>(U memento, string fileName, string directoryPath)
    {
        DataFormat dataFormat = DataFormat.Binary;

        // Tworzenie sciezki do zapisu plikow.
        string savePath = string.Format("{0}/{1}{2}", directoryPath, fileName, FileExtension);
        byte[] bytes = SerializationUtility.SerializeValue(memento, dataFormat);
        File.WriteAllBytes(savePath, bytes);

        Debug.LogFormat("[SAVE] Successfully saved memento: {0} at path: {1}".SetColor(Color.green), typeof(U), savePath);
    }

    /// <summary>
    /// Wczytanie zapisanej pamiatki managera.
    /// </summary>
    /// <typeparam name="U">Typ pamiatki do wczytania.</typeparam>
    /// <param name="fileName">Nazwa pliku z pamiatka.</param>
    /// <param name="directoryPath">Sciezka do katalogu z zapisanym plikiem.</param>
    public U GetSavedManagerMemento<U>(string fileName, string directoryPath) where U : MementoBase
    {
        U managerMemento = default;
        string savePath = string.Format("{0}/{1}{2}", directoryPath, fileName, FileExtension);

        if (File.Exists(savePath))
        {
            byte[] bytes = File.ReadAllBytes(savePath);
            DataFormat dataFormat = DataFormat.Binary;
            managerMemento = SerializationUtility.DeserializeValue<U>(bytes, dataFormat);
        }

        // Komunikaty o bledach w ladowaniu - mozna dodac jakies wyjatki jak bedzie taka potrzeba.
        if(managerMemento != default)
        {
            Debug.LogFormat("[LOAD] Successfully loaded memento: {0} at path: {1}".SetColor(Color.green), typeof(U), savePath);
        }
        else
        {
            Debug.LogFormat("[LOAD] Failed loaded memento: {0} can't find at path: {1}".SetColor(Color.red), typeof(U), savePath);
        }

        return managerMemento;
    }

    #endregion

    #region Handlers

    #endregion
}
