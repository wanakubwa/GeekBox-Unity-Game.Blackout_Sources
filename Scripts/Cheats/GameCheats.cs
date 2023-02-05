using BOBCheats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameCheats : CheatBase
{
    private static Action<float> recorderHandler = null;

    public const string NODE_CATEGORY = "NODES";
    public const string MENU_CATEGORY = "MENU";
    public const string WALLET_CATEGORY = "WALLET";
    public const string EDITOR_CATEGORY = "EDITOR";

    [Cheat]
    public static void ShowTutorialPopUpCheat()
    {
        PopUpManager.Instance.ShowTutorialPopUp(TutorialSystem.TutorialType.MENU_TECHNOLOGY_TREE, delegate { });
    }

    [Cheat]
    public static void ShowLosePopUpCheat()
    {
        PopUpManager.Instance.ShowLosePopUp(TimeManager.Instance.CurrentMilisecondsCounter);
    }

    [Cheat]
    public static void ShowWinPopUpCheat()
    {
        PopUpManager.Instance.ShowWinPopUp(TimeManager.Instance.CurrentMilisecondsCounter);
    }

    [Cheat]
    public static void EnableRecordingTimeScoreCheat()
    {
        string fileName = "recordScores.csv";

        if(recorderHandler == null)
        {

            recorderHandler = (playerTimeMs) =>
            {
                List<string> existingScores = new List<string>();

                string jsonPath = Path.Combine(PathFacade.CurrentPath.DataSavePath, fileName);
                if (File.Exists(jsonPath))
                {
                    existingScores = File.ReadAllLines(jsonPath).ToList();
                }

                existingScores.Add(string.Format("{0}; {1}", ScenariosManager.Instance.CurrentScenarioInfo.ScenarioNameKey, playerTimeMs));

                File.WriteAllLines(jsonPath, existingScores);
            };

            GameEventsManager.Instance.OnPlayerWinScenario += recorderHandler;

            Debug.Log("Score recording ENABLED".SetColor(Color.green));
        }
        else
        {
            GameEventsManager.Instance.OnPlayerWinScenario -= recorderHandler;
            recorderHandler = null;

            Debug.Log("Score recording DISABLED".SetColor(Color.red));
        }
    }

    [Cheat]
    public static void ToggleTimeCheat()
    {
        TimeManager.Instance.SetIsTimeCounting(!TimeManager.Instance.IsTimeCounting);
    }

    [Cheat(WALLET_CATEGORY)]
    public static void AddOneUpgradePointCheat()
    {
        PlayerManager.Instance.Wallet.AddUpgradePoints(1);
    }

    [Cheat(WALLET_CATEGORY)]
    public static void AddKPointsCheat(int value)
    {
        PlayerManager.Instance.Wallet.AddKPoints(value);
    }

    [Cheat(NODE_CATEGORY)]
    public static void PrintAllNodesModValuesCheat()
    {
        foreach (MapNode node in MapManager.Instance.MapNodesCollection)
        {
            Debug.Log(node.ToString());
        }
    }

    [Cheat(NODE_CATEGORY)]
    public static void FullFillSelectedNodeCheat()
    {
        MapNode selectedNode = SelectingManager.Instance.LastSelectedNode;
        selectedNode.Values.AddChargeValue(1000000);
    }

    [Cheat(NODE_CATEGORY)]
    public static void ToggleConnectionsValuesCheat()
    {
        string extendElementName = "values-drawer-cheat";

        ConnectionValuesExtendElement elementPrefab = Resources.Load<ConnectionValuesExtendElement>("Prefabs/GUI/ScenarioEditor/ExtendUIElements/ConnectionValuesExtendUI");

        List<MapConnection> mapConnections = MapManager.Instance.MapConnectionsCollection;

        if(mapConnections.First().ConnectionVisualization.gameObject.GetComponentInChildren<ConnectionValuesExtendElement>() != null)
        {
            for (int i = 0; i < mapConnections.Count; i++)
            {
                GameObject.Destroy(mapConnections[i].ConnectionVisualization.gameObject.GetComponentInChildren<ConnectionValuesExtendElement>().gameObject);
            }

            return;
        }

        for(int i =0; i < mapConnections.Count; i++)
        {
            ConnectionValuesExtendElement spawnedElement = GameObject.Instantiate(elementPrefab);
            spawnedElement.transform.ResetParent(mapConnections[i].ConnectionVisualization.transform);
            spawnedElement.Initialize(mapConnections[i], delegate { });
            spawnedElement.gameObject.name = extendElementName;
        }
    }

    [Cheat(NODE_CATEGORY)]
    public static void AddModeToNodeCheat(int modeNo)
    {
        MapNode selectedNode = SelectingManager.Instance.LastSelectedNode;
        NodeProfileBase profile = null;

        switch (modeNo)
        {
            case 0:
                profile = NodeContentSettings.Instance.GetNodeProfileByModeType(NodeModeType.DEFEND);
                break;
            case 1:
                profile = NodeContentSettings.Instance.GetNodeProfileByModeType(NodeModeType.ATTACK);
                break;
            case 2:
                profile = NodeContentSettings.Instance.GetNodeProfileByModeType(NodeModeType.SPEED);
                break;
            case 3:
                profile = NodeContentSettings.Instance.GetNodeProfileByModeType(NodeModeType.NORMAL);
                break;
            default:
                break;
        }

        if(selectedNode != null)
        {
            selectedNode.SetNodeModeProfile(profile);
        }
    }

    [Cheat(MENU_CATEGORY)]
    public static void CheatUnlockAllLvls()
    {
        foreach (var scenario in ScenariosContentSettings.Instance.ScenariosCollection)
        {
            PlayerManager.Instance.Wallet.AddFinishedLvl(scenario.ID, 0f, ScenarioDataManager.RewardType.ONE_STAR);
        }
    }

    [Cheat(EDITOR_CATEGORY)]
    public static void CheatFixAllScenarios()
    {
        string[] scenariosNames = ScenariosManager.Instance.GetScenariosEditorNames();

        int scenarioIndex = 0;

        ScenariosManager.Instance.LoadEditorScenario(scenariosNames[scenarioIndex]);
        ScenariosManager.Instance.OnEditorScenarioLoaded += () => 
        {
            List<MapNode> nodes = MapManager.Instance.MapNodesCollection;
            for(int i =0; i < nodes.Count; i++)
            {
                int charge = nodes[i].ParentId == Constants.NODE_NEUTRAL_PARENT_ID ? Constants.NODE_CREATE_DEFAULT_CHARGE : Constants.NODE_WITH_PARENT_DEFAULT_CHARGE;
                nodes[i].Values.SetChargeValue(charge);
            }

            ScenarioInfo currentInfo = ScenariosManager.Instance.CurrentScenarioInfo;
            ScenariosManager.Instance.SaveEditorScenario(currentInfo.ScenarioDirectoryName, currentInfo.ScenarioNameKey, currentInfo.ScenarioId.ToString());

            scenarioIndex++;
            if(scenarioIndex < scenariosNames.Length)
            {
                ScenariosManager.Instance.LoadEditorScenario(scenariosNames[scenarioIndex]);
            }
        };

        //int charge = CachedNode.ParentId == Constants.NODE_NEUTRAL_PARENT_ID ? Constants.NODE_CREATE_DEFAULT_CHARGE : Constants.NODE_WITH_PARENT_DEFAULT_CHARGE;
        //CachedNode.Values.SetChargeValue(charge);
    }
}
