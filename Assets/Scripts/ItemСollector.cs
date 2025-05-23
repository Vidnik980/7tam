using DG.Tweening;
using System;
using UnityEngine;

public class Item—ollector : MonoBehaviour
{
    [SerializeField] private Transform[] slots = new Transform[7];
    [SerializeField] private Figure[] collectedItems = new Figure[7];

    public event Action<Figure> onCollect;
    public static Item—ollector instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGetFigure();
        }
    }
    private void TryGetFigure()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Figure figure))
            {
                if (figure.enabled == true)
                {
                    figure.enabled = false;
                    MoveItemToSlot(figure);
                }
            }
        }
    }

    private void MoveItemToSlot(Figure item)
    {
        int freeSlotIndex = GetFreeSlotIndex();
        if (freeSlotIndex == -1)
        {
            Debug.Log("There are no free cells!");
            return;
        }

        collectedItems[freeSlotIndex] = item;
        item.GetComponent<Collider2D>().isTrigger = true;
        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        item.transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
        item.transform.DOMove(slots[freeSlotIndex].position, 0.5f).SetEase(Ease.InOutSine);
        onCollect?.Invoke(item);
    }
    private int GetFreeSlotIndex()
    {
        for (int i = 0; i < collectedItems.Length; i++)
        {
            if (!collectedItems[i])
                return i;
        }
        return -1;
    }
}