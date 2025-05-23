using DG.Tweening;
using System;
using UnityEngine;

public class Item—ollector : MonoBehaviour
{
    [SerializeField] private Transform[] slots = new Transform[7];
    [SerializeField] private GameObject[] prefabsInSlotOccupied = new GameObject[7];

    public event Action<GameObject> collect;
    public static Item—ollector instance;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if(hit.collider.TryGetComponent(out Figure figure))
                {
                    if(figure.enabled == true)
                    {
                        figure.enabled = false;
                        GameObject item = hit.collider.gameObject;
                        MoveItemToSlot(item);
                    }
                }
            }
        }
    }

    private void MoveItemToSlot(GameObject item)
    {
        int freeSlotIndex = GetFreeSlotIndex();
        if (freeSlotIndex == -1)
        {
            Debug.Log("There are no free cells!");
            return;
        }

        prefabsInSlotOccupied[freeSlotIndex] = item;
        item.GetComponent<Collider2D>().isTrigger = true;
        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        item.transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
        item.transform.DOMove(slots[freeSlotIndex].position, 0.5f).SetEase(Ease.InOutSine);
        collect?.Invoke(item);
    }
    private int GetFreeSlotIndex()
    {
        for (int i = 0; i < prefabsInSlotOccupied.Length; i++)
        {
            if (!prefabsInSlotOccupied[i])
                return i;
        }
        return -1;
    }
}