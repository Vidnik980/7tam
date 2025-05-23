using UnityEngine;

public class FrozenFigure : MonoBehaviour
{
    [SerializeField] private GameObject snowflake;
    [SerializeField] private int freezeNumber;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer.color = Color.blue;
        GetComponent<Figure>().enabled = false;
        snowflake.SetActive(true);
    }
    private void OnEnable()
    {
        Item—ollector.instance.collect += SnowflakeRemoval;
    }
    private void SnowflakeRemoval(GameObject item)
    {
        freezeNumber--;
        if (freezeNumber == 0)
        {
            spriteRenderer.color = Color.white;
            snowflake.SetActive(false);
            GetComponent<Figure>().enabled = true;
        }
    }
    private void OnMouseDown()
    {
        if (this.enabled)
            HelperManager.instance.ShowTooltip(0);
    }
    private void OnDisable()
    {
        Item—ollector.instance.collect -= SnowflakeRemoval;
    }
}
