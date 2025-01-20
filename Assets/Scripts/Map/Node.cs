using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private GameObject turret;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = Map.BuildManager;
    }
    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;

        if(turret != null)
        {
            //¿©±â¿¡ ÀÌ¹Ì ÅÍ·¿ ÀÖ´Âµ¥´Â ¸øÁþ´Â´Ù´Â ±Û¾¾ Ãâ·ÂÇØÁà¾ßµÊ
            return;
        }

        GameObject turretToBuild = Map.BuildManager.GetTurretToBuild();
        Instantiate(turretToBuild, transform.position, transform.rotation);
    }
}
