using System.Collections.Generic;
using UnityEngine;

public class FigureCollector : MonoBehaviour
{
    [SerializeField] private GameObject[] figurePrefabs;
    [SerializeField] private Sprite[] colorSprite;
    [SerializeField] private Sprite[] animalsSprite;
    [SerializeField] private Transform folderForFigure;

    private int coutAnimals;
    private int coutFigure;
    private int coutColor;
    public List<Vector3> combinations = new List<Vector3> { };
    private void Awake()
    {
        coutFigure = figurePrefabs.Length;
        coutColor = colorSprite.Length / coutFigure;
        coutAnimals = animalsSprite.Length;
    }
    public List<GameObject> FigureGeneration(int number)
    {
        DestroyFigure();
        if (number > coutFigure * coutColor * coutAnimals)
        {
            Debug.Log("Not enough sprites for this request!");
            return null;
        }
        List<GameObject> prefList = new List<GameObject> { };
        while (number > 0)
        {
            Vector3 rnd = new Vector3(Random.Range(0, coutFigure), Random.Range(0, coutColor), Random.Range(0, coutAnimals));
            if (combinations.Contains(rnd) == false)
            {
                combinations.Add(rnd);
                for (int i = 0; i < 3; i++)
                {
                    prefList.Add(CreateFigure(rnd));
                }
                number--;
            }
        }
        RerollFigure(prefList);
        return prefList;
    }
    private GameObject CreateFigure(Vector3 rnd)
    {
        GameObject pref = Instantiate(figurePrefabs[(int)rnd.x]);
        pref.GetComponent<SpriteRenderer>().sprite = colorSprite[(int)rnd.y * coutFigure + (int)rnd.x];
        pref.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = animalsSprite[(int)rnd.z];
        pref.GetComponent<Figure>().combination = rnd;
        pref.transform.parent = folderForFigure;
        pref.SetActive(false);
        if(Random.Range(0,20) == 0)
        {
            pref.GetComponent<FrozenFigure>().enabled = true;
        }
        return pref;
    }
    private void DestroyFigure()
    {
        foreach (Transform child in folderForFigure)
        {
            Destroy(child.gameObject);
        }
    }
    public void RerollFigure(List<GameObject> prefList)
    {
        int n = prefList.Count;

        while (n > 1)
        {
            int k = Random.Range(0, n--);
            GameObject value = prefList[n];
            prefList[n] = prefList[k];
            prefList[k] = value;
        }
    }
}
