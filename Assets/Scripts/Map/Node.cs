using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private GameObject turret;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if(turret != null)
        {
            //여기에 이미 터렛 있는데는 못짓는다는 글씨 출력해줘야됨
            return;
        }

        GameObject turretToBuild = Map.BuildManager.GetTurretToBuild();
        Instantiate(turretToBuild, transform.position, transform.rotation);
    }
}
