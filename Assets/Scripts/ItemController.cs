using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private AudioSource soundBubble;
    [SerializeField] private List<Figure> figures;
    private void OnEnable()
    {
        ItemÑollector.instance.collect += GetFigure;
    }
    private void GetFigure(GameObject item)
    {
        figures.RemoveAll(item => item == null);
        figures.Add(item.GetComponent<Figure>());
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
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < figures.Count; i++)
        {
            if (figures[i].combination == combination)
            {
                Destroy(figures[i].gameObject);
                figures.Remove(figures[i]);
                i--;
                soundBubble.Play();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
    private void OnDisable()
    {
        ItemÑollector.instance.collect -= GetFigure;
    }

}
