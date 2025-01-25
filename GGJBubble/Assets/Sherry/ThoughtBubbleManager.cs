using System.Collections.Generic;
using UnityEngine;

public class ThoughtBubbleManager : MonoBehaviour
{
    [Header("Player 1 Settings")]
    public KeyCode player1Key = KeyCode.A;
    public Transform player1SpawnArea;
    public Transform player1LanguageSlot;
    public GameObject player1ThoughtBubblePrefab;
    public GameObject player1LanguageBubble1Prefab;
    public GameObject player1LanguageBubble2Prefab;
    public GameObject player1LanguageBubble3Prefab;

    [Header("Player 2 Settings")]
    public KeyCode player2Key = KeyCode.UpArrow;
    public Transform player2SpawnArea;
    public Transform player2LanguageSlot;
    public GameObject player2ThoughtBubblePrefab;
    public GameObject player2LanguageBubble1Prefab;
    public GameObject player2LanguageBubble2Prefab;
    public GameObject player2LanguageBubble3Prefab;

    [Header("Settings")]
    public int maxThoughtsPerPlayer = 10;

    private List<GameObject> player1ThoughtBubbles = new List<GameObject>();
    private List<GameObject> player2ThoughtBubbles = new List<GameObject>();

    void Update()
    {
        // 玩家1生成想法气泡
        if (Input.GetKeyDown(player1Key))
        {
            GenerateThoughtBubble(1, player1SpawnArea, player1ThoughtBubbles, player1LanguageSlot);
        }

        // 玩家2生成想法气泡
        if (Input.GetKeyDown(player2Key))
        {
            GenerateThoughtBubble(2, player2SpawnArea, player2ThoughtBubbles, player2LanguageSlot);
        }
    }

    void GenerateThoughtBubble(int playerID, Transform spawnArea, List<GameObject> thoughtBubbles, Transform languageSlot)
    {
        // 检查是否超过最大气泡数量
        if (thoughtBubbles.Count >= maxThoughtsPerPlayer)
        {
            Destroy(thoughtBubbles[0]);
            thoughtBubbles.RemoveAt(0);
        }

        // 在生成区域内生成气泡
        Vector2 randomPosition = GetRandomPositionWithinArea(spawnArea);
        GameObject bubblePrefab = playerID == 1 ? player1ThoughtBubblePrefab : player2ThoughtBubblePrefab;
        GameObject bubble = Instantiate(bubblePrefab, randomPosition, Quaternion.identity, spawnArea);
        thoughtBubbles.Add(bubble);

        // 更新语言气泡槽（但不会消耗所有气泡）
        UpdateLanguageBubble(thoughtBubbles.Count, playerID, languageSlot);
    }

    void UpdateLanguageBubble(int thoughtCount, int playerID, Transform languageSlot)
    {
        // 确定需要显示的语言气泡
        GameObject languagePrefab = null;

        if (thoughtCount >= 3)
        {
            languagePrefab = playerID == 1 ? player1LanguageBubble3Prefab : player2LanguageBubble3Prefab;
        }
        else if (thoughtCount == 2)
        {
            languagePrefab = playerID == 1 ? player1LanguageBubble2Prefab : player2LanguageBubble2Prefab;
        }
        else if (thoughtCount == 1)
        {
            languagePrefab = playerID == 1 ? player1LanguageBubble1Prefab : player2LanguageBubble1Prefab;
        }

        // 更新语言槽中的气泡
        if (languagePrefab != null)
        {
            // 清空旧的语言气泡
            foreach (Transform child in languageSlot)
            {
                Destroy(child.gameObject);
            }

            // 生成新的语言气泡
            Instantiate(languagePrefab, languageSlot.position, Quaternion.identity, languageSlot);
        }
    }

    Vector2 GetRandomPositionWithinArea(Transform spawnArea)
    {
        // 获取生成区域的边界
        RectTransform areaRect = spawnArea.GetComponent<RectTransform>();
        Vector2 min = areaRect.TransformPoint(areaRect.rect.min);
        Vector2 max = areaRect.TransformPoint(areaRect.rect.max);

        // 在范围内生成随机位置
        return new Vector2(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y)
        );
    }
}
