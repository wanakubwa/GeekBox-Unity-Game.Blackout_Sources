using System;

[Serializable]
public class UpgradeEffectBase
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public virtual void Apply(ParentValues targetValues) { }

    public virtual string GetValueText() 
    {
        float value = GetValue();
        string info = value.ToString();

        if (value > 0f)
        {
            info = string.Format("+{0}", value);
        }
        else if (value < 0f)
        {
            info = string.Format("-{0}", value);
        }

        return info;
    }

    public virtual float GetValue()
    {
        return Constants.DEFAULT_VALUE;
    }

    public virtual string GetInfo()
    {
        string effectKey = TechnologyTreeSettings.Instance.Effects.GetEffectKeyByTypeName(GetType().Name);
        return string.Format(effectKey.Localize(), GetValueText());
    }

    #endregion

    #region Enums



    #endregion
}
