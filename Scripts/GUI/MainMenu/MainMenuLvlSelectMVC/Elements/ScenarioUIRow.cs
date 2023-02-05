using UnityEngine;

namespace Blackout.GUI.LvlSelect
{
    public class ScenarioUIRow : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private ScenarioUIElement scenarioElement;
        [SerializeField]
        private Vector2Int shiftMargin = new Vector2Int(200, 0);

        #endregion

        #region Propeties

        public ScenarioUIElement ScenarioElement { 
            get => scenarioElement; 
        }
        public Vector2Int ShiftMargin { 
            get => shiftMargin; 
        }

        #endregion

        #region Methods

        public void SetInfo(ScenarioInfo scenario, IScenarioSelectListener listener)
        {
            ScenarioElement.SetInfo(scenario, listener);
            RefreshScenarioElementPosition(scenario.ScenarioNameKey.ToCharArray());
        }

        public Vector2 GetScenarioElementPosition()
        {
            return transform.localPosition + ScenarioElement.transform.localPosition;
        }

        // djb2 hash - source: http://www.cse.yorku.ca/~oz/hash.html
        private void RefreshScenarioElementPosition(char[] nameKey)
        {
            float rectWith = GetComponent<RectTransform>().rect.width - ShiftMargin.x;

            int hashShift = 0;
            for(int i =0; i < nameKey.Length; i++)
            {
                /* hash * 33 + c */
                hashShift = ((hashShift << 5) + hashShift) + nameKey[i];
            }

            // Warosci przesuniecia od -1 do 1.
            float hashShiftNormalized = (hashShift % 100f) * 0.01f;

            float xPosition = rectWith * 0.5f * hashShiftNormalized;
            ScenarioElement.transform.localPosition = new Vector3(xPosition, 0f, 0f);
        }

        #endregion

        #region Enums



        #endregion
    }
}
