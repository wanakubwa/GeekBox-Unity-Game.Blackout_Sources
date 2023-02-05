using Sirenix.Utilities;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScenarioCreatorBookmark : ScenarioEditorBookmarkBase
{
    #region Fields

    [SerializeField]
    private TMP_InputField scenarioDirectoryName;
    [SerializeField]
    private TMP_InputField scenarioKeyName;
    [SerializeField]
    private TextMeshProUGUI scenarioId;

    #endregion

    #region Propeties

    private TMP_InputField ScenarioDirectoryName { 
        get => scenarioDirectoryName; 
    }

    private TMP_InputField ScenarioKeyName { 
        get => scenarioKeyName; 
    }

    private TextMeshProUGUI ScenarioId { 
        get => scenarioId; 
    }

    #endregion

    #region Methods

    public void CreateNewScenario()
    {
        ScenariosManager.Instance.CreateNewEditorScenario();
    }

    public void SaveScenario()
    {
        if(ScenarioDirectoryName.text.IsNullOrWhitespace() == false)
        {
            ScenariosManager.Instance.SaveEditorScenario(ScenarioDirectoryName.text, ScenarioKeyName.text, ScenarioId.text);
        }
        else
        {
            Debug.Log("Nazwa katalogu scenariusza nie moze byc pusta!".SetColor(Color.red));
        }
    }

    public void LoadScenario()
    {
        PopUpManager.Instance.ShowScenarioLoadEditorPopUp();
    }

    public override void InitializeBookmark()
    {
        base.InitializeBookmark();

        RefreshScenarioData();
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        ScenariosManager.Instance.OnScenarioInfoUpdate += RefreshScenarioData;
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        ScenariosManager.Instance.OnScenarioInfoUpdate -= RefreshScenarioData;
    }

    private void RefreshScenarioData()
    {
        ScenarioInfo scenario = ScenariosContentSettings.Instance.GetScenarioInfoByDirectory(ScenariosManager.Instance.CurrentScenarioInfo.ScenarioDirectoryName);

        SetScenarioNameKey(scenario.ScenarioNameKey);
        ScenarioDirectoryName.SetTextWithoutNotify(scenario.ScenarioDirectoryName);
        SetScenarioId(scenario.ScenarioId.ToString());

        MEC.Timing.RunCoroutine(WaitFrameAndRebuildLayout());
    }

    private void SetScenarioNameKey(string key)
    {
        ScenarioKeyName.text = key;
    }

    private void SetScenarioId(string id)
    {
        ScenarioId.text = id;
    }

    // Hack do naprawy zle przeliczanej pozycji inputfieldow w pierwszej klatce.
    private IEnumerator<float> WaitFrameAndRebuildLayout()
    {
        yield return MEC.Timing.WaitForOneFrame;

        if(ScenarioDirectoryName != null && ScenarioKeyName != null)
        {
            ScenarioDirectoryName.enabled = false;
            ScenarioKeyName.enabled = false;

            yield return MEC.Timing.WaitForOneFrame;

            ScenarioDirectoryName.enabled = true;
            ScenarioKeyName.enabled = true;
        }
    }

    #endregion

    #region Enums



    #endregion
}
