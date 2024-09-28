using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileProperties tilePros { get; private set; }
    public Cell cell { get; private set; }
    public int value { get; private set; }

    public bool locked { get; set; }

    private Image backgroundImage;

    private TextMeshProUGUI text;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetProperties(TileProperties properties, int value)
    {
        this.tilePros = properties;
        this.value = value;
        backgroundImage.color = properties.backgroundColor;
        text.color = properties.textColor;
        text.text = value.ToString();
    }

    public void Spawn(Cell cell)
    {

        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;

        this.transform.position = cell.transform.position;
    }

    public void MoveTo(Cell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;

        StartCoroutine(Animate(cell.transform.position, false));
    }

    public void Merge(Cell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = null;
        cell.tile.locked = true;

        StartCoroutine(Animate(cell.transform.position, true));
    }
    private IEnumerator Animate(Vector3 to, bool merging)
    {
        float elapse = 0f;
        float duration = 0.15f;

        Vector3 from = transform.position;
        while (elapse < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapse / duration);
            elapse += Time.deltaTime;
            yield return null;
        }
        transform.position = to;

        if (merging)
        {
            Destroy(gameObject);
        }
    }
}
