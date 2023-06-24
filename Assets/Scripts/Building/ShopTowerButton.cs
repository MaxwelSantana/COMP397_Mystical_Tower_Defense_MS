using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTowerButton : MonoBehaviour
{
    public int cost;
    public Button button;

    public void EnableButton(bool enabled)
    {
        button.interactable = enabled;
    }
}
