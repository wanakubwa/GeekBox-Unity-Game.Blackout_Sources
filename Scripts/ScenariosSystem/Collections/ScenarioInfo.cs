using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class ScenarioInfo : IIDEquatable
{
    #region Fields

    [SerializeField, ReadOnly, GUIColor(0, 0.839f, 0.074f)]
    private int orderNo;
    [SerializeField, ReadOnly]
    private string scenarioDirectoryName;
    [SerializeField, ReadOnly]
    private string scenarioNameKey;
    [SerializeField, ReadOnly]
    private int scenarioId;

    #endregion

    #region Propeties

    public string ScenarioDirectoryName
    {
        get => scenarioDirectoryName;
        private set => scenarioDirectoryName = value;
    }

    public string ScenarioNameKey
    {
        get => scenarioNameKey;
        private set => scenarioNameKey = value;
    }

    public int ScenarioId
    {
        get => scenarioId;
        private set => scenarioId = value;
    }

    public int OrderNo
    {
        get => orderNo;
        private set => orderNo = value;
    }

    public int ID => ScenarioId;

    #endregion

    #region Methods

    public static ScenarioInfo GetDefault()
    {
        return new ScenarioInfo(string.Empty, Constants.LOC_DEFAULT_KEY, Guid.NewGuid().GetHashCode());
    }

    public ScenarioInfo() { }

    public ScenarioInfo(string directoryName, string nameKey, int scenarioId)
    {
        ScenarioDirectoryName = directoryName;
        ScenarioNameKey = nameKey;
        ScenarioId = scenarioId;
    }

    public ScenarioInfo(ScenarioInfo source)
    {
        ScenarioDirectoryName = source.ScenarioDirectoryName;
        ScenarioNameKey = source.ScenarioNameKey;
        ScenarioId = source.ScenarioId;
        OrderNo = source.OrderNo;
    }

    public void SetId(int newId)
    {
        ScenarioId = newId;
    }

    public void SetNameKey(string key)
    {
        ScenarioNameKey = key;
    }

    public void SetOrderNo(int no)
    {
        OrderNo = no;
    }

    public bool IDEqual(int otherId)
    {
        return ScenarioId == otherId;
    }

    #endregion

    #region Enums



    #endregion
}
