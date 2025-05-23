using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private float animationDuration = 0.5f;

    private List<string> panelsName;
    private int activeIndex;

    public static PanelManager instance;
    private void Start()
    {
        instance = this;
        panelsName = new List<string>();
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
            panelsName.Add(panel.name);
        }
        panels[0].SetActive(true);
    }
    public void OpenPanel(string namePanel)
    {
        panels[activeIndex].SetActive(false);
        activeIndex = panelsName.IndexOf(namePanel);

        panels[activeIndex].SetActive(true);

        panels[activeIndex].transform.localScale = Vector3.zero;
        panels[activeIndex].transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);
    }
    public void CloseAllPanel()
    {
        panels[activeIndex].SetActive(false);
    }
}
