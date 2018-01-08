using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SellTower : MonoBehaviour {

    public Button sellButton;
    public MoneyManager moneyManager;
    public int normalSellPrice = 450;
    public int freezeSellPrice = 675;
    public int shockSellPrice = 900;

    void Start () {
        Button btn = sellButton.GetComponent<Button>();
        moneyManager = (MoneyManager)Object.FindObjectOfType<MoneyManager>();
        btn.onClick.AddListener(Sell);
    }

    public void Sell()
    {
        GameObject cube = (GameObject)gameObject.GetComponentInParent<UpgradeSellMenu>().selectedCube;
        var cellInteract = cube.GetComponent<CellInteract>();
        string towerType = cellInteract.tower.tag;
        if (towerType == "NormalTower")
            moneyManager.AddMoney(normalSellPrice);
        else if(towerType == "FreezeTower")
            moneyManager.AddMoney(freezeSellPrice);
        else if (towerType == "ShockTower")
            moneyManager.AddMoney(shockSellPrice);
        cellInteract.DestroyTower();
    }
}
