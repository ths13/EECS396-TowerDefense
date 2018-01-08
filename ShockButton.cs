using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockButton : MonoBehaviour
{

    public Button shockButton;
    public MoneyManager moneyManager;
    public int normalPrice = 1000;

    internal void Start()
    {

        Button btn = shockButton.GetComponent<Button>();
        moneyManager = (MoneyManager)Object.FindObjectOfType<MoneyManager>();
        btn.onClick.AddListener(BuildShockTower);
        CheckMoney();
    }

    internal void Update()
    {
        CheckMoney();
    }

    public void CheckMoney()
    {
        if (moneyManager.CurrentMoney < normalPrice)
        {
            shockButton.interactable = false;
        }
        else
        {
            shockButton.interactable = true;
        }
    }

    public void BuildShockTower()
    {
        GameObject cube = (GameObject)gameObject.GetComponentInParent<BuildMenu>().selectedCube;
        var cellInteract = cube.GetComponent<CellInteract>();
        cellInteract.BuildTower("ShockTower", normalPrice);
    }


}
