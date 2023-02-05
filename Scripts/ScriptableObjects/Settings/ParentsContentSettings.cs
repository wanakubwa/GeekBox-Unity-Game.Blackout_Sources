using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[Serializable]
[CreateAssetMenu(fileName = "ParentsContentSettings.asset", menuName = "Settings/ParentsContentSettings")]
public class ParentsContentSettings : ScriptableObject
{
    #region Fields

    private static ParentsContentSettings instance;

    [Title("Colors Settings")]
    [SerializeField]
    private string neutralParentColorLabel;
    [SerializeField]
    private string playerParentColorLabel;

    [Space]
    [SerializeField]
    private List<ParentColorValue> parentsColorsDefinitions = new List<ParentColorValue>();

    #endregion

    #region Propeties

    public static ParentsContentSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<ParentsContentSettings>("Settings/ParentsContentSettings");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<ParentColorValue> ParentsColorsDefinitions {
        get => parentsColorsDefinitions; 
    }

    public string NeutralParentColorLabel { 
        get => neutralParentColorLabel; 
    }

    public string PlayerParentColorLabel { 
        get => playerParentColorLabel;
    }

    #endregion

    #region Methods

    public string[] GetAllColorsLabels()
    {
        string[] colorsNames = new string[ParentsColorsDefinitions.Count];
        for(int i =0; i < ParentsColorsDefinitions.Count; i++)
        {
            colorsNames[i] = ParentsColorsDefinitions[i].ColorLabel;
        }

        return colorsNames;
    }

    public ParentColorValue GetParentColorByLabel(string colorLabel)
    {
        ParentColorValue output = null;
        for (int i = 0; i < ParentsColorsDefinitions.Count; i++)
        {
            if(ParentsColorsDefinitions[i].ColorLabel.Equals(colorLabel) == true)
            {
                output = ParentsColorsDefinitions[i];
                break;
            }
        }

        return output;
    }

    public ParentColorValue GetNeutralParentColor()
    {
        return GetParentColorByLabel(NeutralParentColorLabel);
    }

    public ParentColorValue GetPlayerParentColor()
    {
        return GetParentColorByLabel(PlayerParentColorLabel);
    }

    public string GetColorLabelByColor(Color target)
    {
        string label = "NONE";
        for(int i =0; i< ParentsColorsDefinitions.Count; i++)
        {
            if(ParentsColorsDefinitions[i].ParentColor.Equals(target) == true)
            {
                label = ParentsColorsDefinitions[i].ColorLabel;
                break;
            }
        }

        return label;
    }

    #endregion

    #region Enums


    [Serializable]
    public class ParentColorValue
    {
        #region Fields

        [SerializeField]
        private string colorLabel;
        [SerializeField]
        private Color parentColor = Color.white;
        [SerializeField]
        private Color fontColor = Color.white;
        [SerializeField]
        private Color shieldsColor = Color.blue;

        #endregion

        #region Propeties

        public string ColorLabel
        {
            get => colorLabel;
        }

        public Color ParentColor
        {
            get => parentColor;
        }

        public Color FontColor { 
            get => fontColor; 
        }

        public Color ShieldsColor { 
            get => shieldsColor; 
        }

        #endregion

        #region Methods


        #endregion

        #region Enums



        #endregion
    }

    #endregion
}
