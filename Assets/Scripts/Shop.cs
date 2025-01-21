using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = Map.BuildManager;
    }
    public void PurchaseStandardTurret()
    {
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseMissleTurret()
    {
        buildManager.SetTurretToBuild(buildManager.missleTurretPrefab);
    }
}
