using Core.Economy;
using System.Collections;
using TowerDefense.Level;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI.HUD
{
    /// <summary>
    /// A class for controlling the displaying the currency
    /// </summary>
    public class CurrencyUI : MonoBehaviour
    {
        /// <summary>
        /// The text element to display information on
        /// </summary>
        public Text display;

        /// <summary>
        /// The text element to display information on
        /// </summary>
        public GameObject notEnoughEnergyDisplay;

        /// <summary>
        /// The currency prefix to display next to the amount
        /// </summary>
        public string currencySymbol = "$";

        protected Currency m_Currency;

        /// <summary>
        /// Assign the correct currency value
        /// </summary>
        protected virtual void Start()
        {
            notEnoughEnergyDisplay.SetActive(false);
            if (LevelManager.instance != null)
            {
                m_Currency = LevelManager.instance.currency;

                UpdateDisplay();
                m_Currency.currencyChanged += UpdateDisplay;
                m_Currency.cannotAffordAction += UpdateCannotAfordDisplay;
            }
            else
            {
                Debug.LogError("[UI] No level manager to get currency from");
            }
        }

        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (m_Currency != null)
            {
                m_Currency.currencyChanged -= UpdateDisplay;
            }
        }

        /// <summary>
        /// A method for updating the display based on the current currency
        /// </summary>
        protected void UpdateDisplay()
        {
            int current = m_Currency.currentCurrency;
            display.text = current.ToString();
        }

        protected void UpdateCannotAfordDisplay()
        {
            StartCoroutine(ActiveAndDeactivate());
        }

        IEnumerator ActiveAndDeactivate()
        {

            notEnoughEnergyDisplay.SetActive(true);

            yield return new WaitForSeconds(1.0f); //will wait 5seconds before continuing

            notEnoughEnergyDisplay.SetActive(false);

        }
    }
}