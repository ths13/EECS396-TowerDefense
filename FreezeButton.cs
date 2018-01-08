using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeButton : MonoBehaviour
{

    public Button freezeButton;
    public MoneyManager moneyManager;
    public int normalPrice = 750;

    internal void Start()
    {

        Button btn = freezeButton.GetComponent<Button>();
        moneyManager = (MoneyManager)Object.FindObjectOfType<MoneyManager>();
        btn.onClick.AddListener(BuildFreezeTower);
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
            freezeButton.interactable = false;
        }
        else
        {
            freezeButton.interactable = true;
        }
    }

    public void BuildFreezeTower()
    {
        GameObject cube = (GameObject)gameObject.GetComponentInParent<BuildMenu>().selectedCube;
        var cellInteract = cube.GetComponent<CellInteract>();
        cellInteract.BuildTower("FreezeTower", normalPrice);
    }


}
