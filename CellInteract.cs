using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CellInteract : MonoBehaviour {

    public GameObject buildMenu;
    public GameObject upgradeSellMenu;
    public MoneyManager moneyManager;

    public Color interactColor = Color.grey;
    public Color originalColor;
    public MeshRenderer m_Renderer;

    public bool isBuildMenuActive;
    public bool isUpgradeSellMenuActive;
    public bool selected = false;
    public bool hasTower = false;

    public bool hasEnemy = false;

    public GameObject tower;
    public int TowerValue;

    public Spawn sp;

    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        originalColor = m_Renderer.material.color;
        isBuildMenuActive = buildMenu.activeSelf;
        isUpgradeSellMenuActive = upgradeSellMenu.activeSelf;
        moneyManager = (MoneyManager)Object.FindObjectOfType<MoneyManager>();
        sp = FindObjectOfType<Spawn>();
    }

    void Update()
    {
        isBuildMenuActive = buildMenu.activeSelf;
        isUpgradeSellMenuActive = upgradeSellMenu.activeSelf;

        RaycastHit hit;

        
        if (Physics.BoxCast(gameObject.transform.position, new Vector3(.5f,.5f,.5f),  Vector3.up, out hit, Quaternion.identity, 100f))
        {
            if (hit.collider.gameObject.GetComponent<EnemyMovement>())
            {
                hasEnemy = true;
            }
            else
            {
                hasEnemy = false;
            }
        }
        else
        {
            hasEnemy = false;
        }
    }


    void OnMouseOver()
    {
        if (!isBuildMenuActive && !isUpgradeSellMenuActive && !hasEnemy && !sp.GameOver)
        {
            m_Renderer.material.color = interactColor;
        }
    }

    void OnMouseExit()
    {
        if (!isBuildMenuActive && !isUpgradeSellMenuActive && !sp.GameOver)
        {
            m_Renderer.material.color = originalColor;
        }
    }

    void OnMouseDown()
    {
        if(!isBuildMenuActive && !selected && !hasTower && !isUpgradeSellMenuActive && !hasEnemy && !sp.GameOver)
        {
            buildMenu.SetActive(true);
            selected = true;
            var buildMenuComponent = buildMenu.gameObject.GetComponent<BuildMenu>();
            buildMenuComponent.selectedCube = gameObject;
            m_Renderer.material.color = interactColor;
        }
        else if (isBuildMenuActive && selected && !hasEnemy && !sp.GameOver)
        {
            buildMenu.SetActive(false);           
            selected = false;
            m_Renderer.material.color = originalColor;
        }
        else if (!isUpgradeSellMenuActive && !selected && hasTower &&!isBuildMenuActive && !hasEnemy && !sp.GameOver)
        {
            upgradeSellMenu.SetActive(true);
            selected = true;
            var upgradeSellComponent = upgradeSellMenu.gameObject.GetComponent<UpgradeSellMenu>();
            upgradeSellComponent.selectedCube = gameObject;
            m_Renderer.material.color = interactColor;
        }
        else if (isUpgradeSellMenuActive && selected && !hasEnemy && !sp.GameOver)
        {
            upgradeSellMenu.SetActive(false);
            selected = false;
            m_Renderer.material.color = originalColor;
        }
    }

    public void BuildTower(string towerType, int price)
    {
        if(!hasEnemy)
        {
            hasTower = true;
            buildMenu.SetActive(false);
            m_Renderer.material.color = originalColor;


            var _tower = (GameObject)Instantiate(Resources.Load(towerType));
            _tower.transform.position = new Vector3(gameObject.transform.position.x, .91f, gameObject.transform.position.z);

            var spawner = FindObjectOfType<Spawn>();

            if(towerType == "NormalTower")
            {
                var sphere = _tower.GetComponentInChildren<NormalTower>();
                sphere.transform.LookAt(spawner.transform, Vector3.up);

                tower = _tower;

                selected = false;
                TowerValue = price;
                StartCoroutine(IsValidPlacement());
            }
            else if(towerType == "FreezeTower")
            {

                tower = _tower;

                selected = false;
                TowerValue = price;
                StartCoroutine(IsValidPlacement());
            }
            else if(towerType=="ShockTower")
            {
                var sphere = _tower.GetComponentInChildren<ShockTower>();
                sphere.transform.LookAt(spawner.transform, Vector3.up);

                tower = _tower;

                selected = false;
                TowerValue = price;
                StartCoroutine(IsValidPlacement());
            }
            
        }
    }

    public void DestroyTower()
    {
        hasTower = false;
        upgradeSellMenu.SetActive(false);
        m_Renderer.material.color = originalColor;
        Destroy(tower.gameObject);
        selected = false;
    }

    IEnumerator IsValidPlacement()
    {
        yield return null;


        NavMeshPath testPath = new NavMeshPath();
        var basePosition = FindObjectOfType<Base>().transform.position;
        
        NavMesh.CalculatePath(basePosition, new Vector3(0f,0f,0f),NavMesh.AllAreas,testPath);

        if (testPath.status != NavMeshPathStatus.PathComplete)
        {
            DestroyTower();
        }
        else
        {
            moneyManager.AddMoney(-TowerValue);
        }
        
    }
}