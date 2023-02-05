using MEC;
using SaveLoadSystem;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonScenarioSaveableManager<TimeManager, TimeManagerMemento>, IContentLoadable
{
    #region Fields

    #endregion

    #region Propeties

    // Events.
    /// <summary>
    /// float - time[ms].
    /// </summary>
    public event System.Action<float> OnGameTimeUpdate = delegate { };

    // Variables.
    public float CurrentMilisecondsCounter { get; private set; } = Constants.DEFAULT_VALUE;
    public bool IsTimeCounting { get; private set; }
    private CoroutineHandle TimeCoroutine { get; set; }

    #endregion

    #region Methods

    public void SetIsTimeCounting(bool isTimeCounting)
    {
        IsTimeCounting = isTimeCounting;
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        ScenariosManager.Instance.OnGameScenarioLoaded += OnGameScenarioLoadedHandler;
        InGameEvents.Instance.OnTimePaused += OnTimePausedHandler;
    }

    public void LoadContent()
    {

    }

    public void UnloadContent()
    {
        MEC.Timing.KillCoroutines(TimeCoroutine);
        CurrentMilisecondsCounter = Constants.DEFAULT_VALUE;
        SetIsTimeCounting(false);
    }

    public override void CreateNewScenario()
    {
        CurrentMilisecondsCounter = Constants.DEFAULT_VALUE;
        SetIsTimeCounting(false);
    }

    public override void LoadManager(TimeManagerMemento memento)
    {
        CurrentMilisecondsCounter = memento.CurrentMsCounterSave;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if(ScenariosManager.Instance != null)
        {
            ScenariosManager.Instance.OnGameScenarioLoaded -= OnGameScenarioLoadedHandler;
        }

        if(InGameEvents.Instance != null)
        {
            InGameEvents.Instance.OnTimePaused -= OnTimePausedHandler;
        }
    }

    private void OnGameScenarioLoadedHandler()
    {
        SetIsTimeCounting(true);
        TimeCoroutine = MEC.Timing.RunCoroutine(_CountTime());
    }

    private IEnumerator<float> _CountTime()
    {
        yield return MEC.Timing.WaitForOneFrame;

        while (true)
        {
            if(IsTimeCounting == true)
            {
                CurrentMilisecondsCounter = CurrentMilisecondsCounter + (Constants.SECONDS_TO_MILI_FACTOR * Time.deltaTime);
                OnGameTimeUpdate(CurrentMilisecondsCounter);
            }
            
            yield return MEC.Timing.WaitForOneFrame;
        }
    }

    private void OnTimePausedHandler(bool isPaused)
    {
        SetIsTimeCounting(!isPaused);
    }

    #endregion

    #region Enums



    #endregion
}
