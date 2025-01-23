using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    private Renderer rend;
    private Color startColor;

    [Header ("Optional")]
    public GameObject turret;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = Map.BuildManager;
    }

    public Vector3 GetBuildPosition(TurretBluePrint turretToBuild)
    {
        Vector3 BuildPosition = new Vector3(transform.position.x, turretToBuild.prefab.transform.position.y, transform.position.z);
        return BuildPosition + positionOffset;
    }

    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if(buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if(turret != null)
        {
            //¿©±â¿¡ ÀÌ¹Ì ÅÍ·¿ ÀÖ´Âµ¥´Â ¸øÁþ´Â´Ù´Â ±Û¾¾ Ãâ·ÂÇØÁà¾ßµÊ
            return;
        }

        Map.BuildManager.BuildTurretOn(this);
    }
}
