using UnityEngine;

public class HelperManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tooltipPanels;
    [SerializeField] private float slideDuration = 0.5f;
    private int activePanel;

    public static HelperManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void ShowTooltip(int index)
    {
        activePanel = index;
        tooltipPanels[activePanel].SetActive(true);
    }
    private void HideTooltip()
    {
        tooltipPanels[activePanel].SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            HideTooltip();
        }
    }
}
