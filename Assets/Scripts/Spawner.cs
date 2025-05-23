using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private FigureCollector figureCollector;
    [SerializeField] private Transform spawnPoint;

    private List<GameObject> figures;
    private bool isMixing;
    private void Start()
    {
        Item—ollector.instance.onCollect += TakeFigure;
    }
    public void TakeFigure(Figure item)
    {
        figures.Remove(item.gameObject);
        if(figures.Count == 0)
        {
            PanelManager.instance.OpenPanel("ResultWin");
        }
    }
    public void NewLevel()
    {
        figures = figureCollector.FigureGeneration(10);
        StartCoroutine(Timer());
    }
    public void Reroll()
    {
        if (figures.Count == 0 || isMixing)
            return;
        foreach (GameObject figure in figures)
        {
            figure.SetActive(false);
        }
        figureCollector.RerollFigure(figures);
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        isMixing = true;
        for (int i = figures.Count - 1; i >= 0 ; i--)
        {
            figures[i].transform.Rotate(0, 0, Random.Range(0, 360));
            figures[i].transform.position = spawnPoint.position;
            figures[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
        isMixing = false;
    }
    private void OnDisable()
    {
        Item—ollector.instance.onCollect -= TakeFigure;
    }
}
