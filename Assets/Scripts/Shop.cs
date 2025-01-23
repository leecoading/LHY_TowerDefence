using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missleTurret;
    public TurretBluePrint LaserTurret;

   BuildManager buildManager;

    private void Start()
    {
        buildManager = Map.BuildManager;
    }
    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissleTurret()
    {
        buildManager.SelectTurretToBuild(missleTurret);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(LaserTurret);
    }
}
