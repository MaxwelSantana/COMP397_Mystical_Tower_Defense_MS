using System.Globalization;
using TowerDefense.Level;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI.HUD
{
    /// <summary>
    /// A simple implementation of UI for player base health
    /// </summary>
    public class PlayerBaseHealth : MonoBehaviour
    {
        /// <summary>
        /// The text element to display information on
        /// </summary>
        public Text display;

        /// <summary>
        /// Get the max health of the player base
        /// </summary>
        protected virtual void Start()
        {
            LevelManager.instance.onDamage += UpdateDisplay;
            UpdateDisplay();
        }

        /// <summary>
        /// Subscribes to the player base health died event
        /// </summary>
        /// <param name="info">
        /// The associated health change information
        /// </param>
        protected virtual void OnBaseDamaged()
        {
            UpdateDisplay();
        }

        /// <summary>
        /// Get the current health of the home base and display it on m_Display
        /// </summary>
        protected void UpdateDisplay()
        {
            LevelManager levelManager = LevelManager.instance;
            if (levelManager == null)
            {
                return;
            }
            int currentHealth = levelManager.Health;
            display.text = currentHealth.ToString(CultureInfo.InvariantCulture);
        }
    }
}