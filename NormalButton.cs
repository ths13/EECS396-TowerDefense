using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalButton : MonoBehaviour {

    public Button normalButton;
    public MoneyManager moneyManager;
    public int normalPrice = 500;

    internal void Start () {

        Button btn = normalButton.GetComponent<Button>();
        moneyManager = (MoneyManager) Object.FindObjectOfType<MoneyManager>();
        btn.onClick.AddListener(BuildNormalTower);
        CheckMoney();
    }

	internal void Update () {
        CheckMoney();
    }

    public void CheckMoney()
    {
        if (moneyManager.CurrentMoney < normalPrice)
        {
            normalButton.interactable = false;
        }
        else
        {
            normalButton.interactable = true;
        }
    }

    public void BuildNormalTower()
    {
        GameObject cube = (GameObject) gameObject.GetComponentInParent<BuildMenu>().selectedCube;
        var cellInteract = cube.GetComponent<CellInteract>();
        cellInteract.BuildTower("NormalTower", normalPrice);
    }


}
