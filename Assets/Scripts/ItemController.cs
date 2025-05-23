using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private AudioSource soundBubble;
    [SerializeField] private List<Figure> figures;
    [SerializeField] private float delayAnimationDead = 0.2f;
    private void Start()
    {
        ItemÑollector.instance.onCollect += GetFigure;
    }
    private void GetFigure(Figure item)
    {
        figures.RemoveAll(item => item == null);
        figures.Add(item);
        MatchChecking();
    }
    private void MatchChecking()
    {
        Dictionary<Vector3, int> vectorCounts = new Dictionary<Vector3, int>();
        bool isCombo = false;
        foreach (Figure figure in figures)
        {
            if (vectorCounts.ContainsKey(figure.combination))
            {
                vectorCounts[figure.combination]++;
                if (vectorCounts[figure.combination] >= 3)
                {
                    isCombo = true;
                    StartCoroutine(RemoveObjects(figure.combination));
                }
            }
            else
            {
                vectorCounts[figure.combination] = 1;
            }
        }
        if (figures.Count == 7 && isCombo == false)
        {
            PanelManager.instance.OpenPanel("ResultLose");
        }
    }
    private IEnumerator RemoveObjects(Vector3 combination)
    {
        var wait = new WaitForSeconds(delayAnimationDead);
        yield return wait;
        for (int i = 0; i < figures.Count; i++)
        {
            if (figures[i].combination == combination)
            {
                Destroy(figures[i].gameObject);
                figures.Remove(figures[i]);
                i--;
                soundBubble.Play();
                yield return wait;
            }
        }
    }
    private void OnDisable()
    {
        ItemÑollector.instance.onCollect -= GetFigure;
    }

}
