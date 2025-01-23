using UnityEngine.UI;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public Text livesText;

    private void Update()
    {
        livesText.text = PlayerStats.Lives.ToString() + "LIVES";
    }
}
