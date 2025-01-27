using UnityEngine;

public class BubbleLayer : MonoBehaviour
{
    public string sortingLayer1 = "speechbubble1"; // 第一个Sorting Layer的名字
    public string sortingLayer2 = "speechbubble2"; // 第二个Sorting Layer的名字

    void Start()
    {
        // 获取当前物体的SpriteRenderer组件
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("未找到SpriteRenderer组件，请确保该物体上有SpriteRenderer。");
            return;
        }

        // 随机选择一个Sorting Layer
        string chosenLayer = Random.Range(0, 2) == 0 ? sortingLayer1 : sortingLayer2;

        // 设置SpriteRenderer的Sorting Layer
        spriteRenderer.sortingLayerName = chosenLayer;

        Debug.Log($"随机选择的Sorting Layer是: {chosenLayer}");
    }
}
