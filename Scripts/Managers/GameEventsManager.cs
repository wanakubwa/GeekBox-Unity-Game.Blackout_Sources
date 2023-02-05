using UnityEngine;
using System.Collections.Generic;
using SaveLoadSystem;
using PlayerData;
using System;

public class GameEventsManager : ManagerSingletonBase<GameEventsManager>, IContentLoadable
{
    #region Fields



    #endregion

    #region Propeties

    /// <summary>
    /// float - win time.
    /// </summary>
    public event Action<float> OnPlayerWinScenario = delegate { };
    public event Action OnPlayerLooseScenario = delegate { };

    private List<int> CurrentParentsIds { 
        get; 
        set; 
    } = new List<int>();

    #endregion

    #region Methods

    public void LoadContent()
    {
        List<NodeParent> parents = ParentsManager.Instance.ParentsCollection;
        foreach (NodeParent parent in parents)
        {
            // Wszystkie parenty, ktore sa nadal w aktywnej grze za wyjatkiem nautralnego.
            if (parent.NodesIdCollection.Count > 0 && parent.IDEqual(Constants.NODE_NEUTRAL_PARENT_ID) == false)
            {
                CurrentParentsIds.Add(parent.ID);
            }
            else if(parent.NodesIdCollection.Count < 1 && parent.IDEqual(Constants.NODE_NEUTRAL_PARENT_ID) == false)
            {
                Debug.LogFormat("Parent id: {0} nie posiada nodek.".SetColor(Color.yellow), parent.ID);
            }
        }
    }

    public void UnloadContent()
    {
        CurrentParentsIds.Clear();
    }

    public void CheckGameOverState(NodeParent parent)
    {
        if(CurrentParentsIds.Count < 1 || GameManager.Instance.ActualSetScene == SceneLabel.SCENARIO_EDITOR)
        {
            return;
        }

        if(parent.IDEqual(Constants.NODE_PLAYER_PARENT_ID) == true)
        {
            HandlePlayerLooseGame();
        }
        else
        {
            CurrentParentsIds.Remove(parent.ID);
            if(CurrentParentsIds.Count == 1)
            {
                HandlePlayerWinnGame();
            }
        }
    }

    public void SkipLvl()
    {
        ScenarioInfo scenarioInfo = ScenariosManager.Instance.CurrentScenarioInfo;
        int currentScenarioId = scenarioInfo.ScenarioId;
        ScenarioDataManager.RewardType starsReward = ScenarioDataManager.Instance.GetRewardForTime(int.MaxValue);

        PlayerManager.Instance.Wallet.AddFinishedLvl(currentScenarioId, int.MaxValue, starsReward);
        GameManager.Instance.SaveGameAtScenarioEnd();
    }

    private void HandlePlayerLooseGame()
    {
        InGameEvents.Instance.NotifyPauseTime(true);
        Debug.Log("Gracz przegral gre!".SetColor(Color.yellow));

        PopUpManager.Instance.ShowLosePopUp(TimeManager.Instance.CurrentMilisecondsCounter);
        GameManager.Instance.SaveGameAtScenarioEnd();

        OnPlayerLooseScenario();
    }

    private void HandlePlayerWinnGame()
    {
        InGameEvents.Instance.NotifyPauseTime(true);

        Debug.LogFormat("Rodzic o ID {0} wygral!".SetColor(Color.yellow), CurrentParentsIds[0]);

        if(CurrentParentsIds[0] == Constants.NODE_PLAYER_PARENT_ID)
        {
            float currentScenarioTime = TimeManager.Instance.CurrentMilisecondsCounter;
            PopUpManager.Instance.ShowWinPopUp(currentScenarioTime);

            ScenarioInfo scenarioInfo = ScenariosManager.Instance.CurrentScenarioInfo;
            int currentScenarioId = scenarioInfo.ScenarioId;
            ScenarioDataManager.RewardType starsReward = ScenarioDataManager.Instance.GetRewardForTime(currentScenarioTime);

            OnPlayerWinScenario(currentScenarioTime);

            PlayerManager.Instance.Wallet.AddFinishedLvl(currentScenarioId, currentScenarioTime, starsReward);
            GameManager.Instance.SaveGameAtScenarioEnd();

            // Dodanie ukonczonego poziomu do tabeli.
            GeekBox.Ads.EasyMobileManager.Instance.ReportScoreToLeaderboard(scenarioInfo.OrderNo, EasyMobile.EM_GameServicesConstants.Leaderboard_BlackoutLeaderboard);
        }
    }

    #endregion

    #region Enums



    #endregion
}
