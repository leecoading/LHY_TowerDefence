using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TurretBluePrint turretToBuild;
    public GameObject buildEffect;

    public bool CanBuild {  get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    public void BuildTurretOn(Node node)
    {
        if(PlayerStats.Money < turretToBuild.cost)
        {
            //지을 돈이 충분하지 않다는 문구 추가.
            return;
        }
        else
        {
            PlayerStats.Money -= turretToBuild.cost;
        }

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(turretToBuild), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(turretToBuild),Quaternion.identity);
    }
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
}
