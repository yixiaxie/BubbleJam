using System.Collections.Generic;
using UnityEngine;

public class ThoughtBubbleManager : MonoBehaviour
{
    [Header("Player 1 Settings")]
    public KeyCode player1Key = KeyCode.A;
    public Transform player1SpawnArea;
    public RectTransform player1LanguageSlot;
    public GameObject player1ThoughtBubblePrefab;
    public GameObject[] player1LanguageBubblePrefabs;

    [Header("Player 2 Settings")]
    public KeyCode player2Key = KeyCode.UpArrow;
    public Transform player2SpawnArea;
    public RectTransform player2LanguageSlot;
    public GameObject player2ThoughtBubblePrefab;
    public GameObject[] player2LanguageBubblePrefabs;

    [Header("Language Bubble Requirements")]
    public int[] languageBubbleRequirements;

    [Header("Settings")]
    public int maxThoughtsPerPlayer = 10;

    private List<GameObject> player1ThoughtBubbles = new List<GameObject>();
    private List<GameObject> player2ThoughtBubbles = new List<GameObject>();

    public bool isPlayer1SlotFull = false;
    public bool isPlayer2SlotFull = false;

    void Update()
    {
        // 玩家1生成想法气泡
        if (Input.GetKeyDown(player1Key))
        {
            GenerateThoughtBubble(1, player1SpawnArea, player1ThoughtBubbles, player1LanguageSlot, player1LanguageBubblePrefabs);
            Debug.Log($"Spawned Thought Bubble under parent: {player1SpawnArea}");
        }

        // 玩家2生成想法气泡
        if (Input.GetKeyDown(player2Key))
        {
            GenerateThoughtBubble(2, player2SpawnArea, player2ThoughtBubbles, player2LanguageSlot, player2LanguageBubblePrefabs);
            Debug.Log($"Spawned Thought Bubble under parent: {player2SpawnArea}");
        }

        CheckAndGenerateLanguageBubble(1, player1ThoughtBubbles, player1LanguageSlot, player1LanguageBubblePrefabs, ref isPlayer1SlotFull);
        CheckAndGenerateLanguageBubble(2, player2ThoughtBubbles, player2LanguageSlot, player2LanguageBubblePrefabs, ref isPlayer2SlotFull);
    }

    void GenerateThoughtBubble(
     int playerID,
     Transform spawnArea,
     List<GameObject> thoughtBubbles,
     RectTransform languageSlot,
     GameObject[] languageBubblePrefabs)
    {
        Debug.Log($"GenerateThoughtBubble called for Player {playerID}");

        // 检查是否超过最大气泡数量
        if (thoughtBubbles.Count >= maxThoughtsPerPlayer)
        {
            Debug.Log("Max thought bubbles reached. Removing oldest bubble.");
            Destroy(thoughtBubbles[0]);
            thoughtBubbles.RemoveAt(0);
        }

        // 在生成区域内生成气泡
        Vector2 randomPosition = GetRandomPositionWithinArea(spawnArea);
        Debug.Log($"Generated Thought Bubble Position: {randomPosition}");

        GameObject bubblePrefab = playerID == 1 ? player1ThoughtBubblePrefab : player2ThoughtBubblePrefab;
        if (bubblePrefab == null)
        {
            Debug.LogError("Bubble prefab is not assigned!");
            return;
        }

        GameObject bubble = Instantiate(bubblePrefab, randomPosition, Quaternion.identity, spawnArea);
        thoughtBubbles.Add(bubble);

        Debug.Log($"Thought Bubble generated for Player {playerID} at {randomPosition}");
        //TryUpdateLanguageBubble(thoughtBubbles, languageSlot, languageBubblePrefabs);
    }


    void CheckAndGenerateLanguageBubble(
       int playerID,
       List<GameObject> thoughtBubbles,
       RectTransform languageSlot,
       GameObject[] languageBubblePrefabs,
       ref bool isSlotFull)
    {
        // 如果槽已满，不生成新的语言气泡
        if (isSlotFull) return;

        // 检查是否有足够的想法气泡
        int thoughtCount = thoughtBubbles.Count;
        GameObject selectedPrefab = null;
        int requiredThoughts = 0;

        for (int i = languageBubbleRequirements.Length - 1; i >= 0; i--)
        {
            if (thoughtCount >= languageBubbleRequirements[i])
            {
                selectedPrefab = languageBubblePrefabs[i];
                requiredThoughts = languageBubbleRequirements[i];
                break;
            }
        }

        // 如果没有足够的气泡，退出
        if (selectedPrefab == null) return;

        // 生成语言气泡
        GameObject newBubble = Instantiate(selectedPrefab, languageSlot.position, Quaternion.identity, languageSlot);

        // 自动适配槽位大小
        RectTransform bubbleRect = newBubble.GetComponent<RectTransform>();
        if (bubbleRect != null)
        {
            bubbleRect.anchoredPosition = Vector2.zero;
            bubbleRect.sizeDelta = languageSlot.sizeDelta * 0.8f;
            bubbleRect.localScale = Vector3.one;
        }

        // 消耗对应数量的想法气泡
        for (int i = 0; i < requiredThoughts; i++)
        {
            Destroy(thoughtBubbles[0]);
            thoughtBubbles.RemoveAt(0);
        }

        // 设置槽位状态为满
        isSlotFull = true;

        Debug.Log($"Player {playerID} generated a Language Bubble.");
    }


    
    public void OnLanguageBubbleFired(int playerID)
    {
        // 当语言气泡被发射时，重置槽位状态
        if (playerID == 1)
        {
            isPlayer1SlotFull = false;
        }
        else if (playerID == 2)
        {
            isPlayer2SlotFull = false;
        }

        Debug.Log($"Player {playerID} Language Slot is now empty.");
    }

    Vector2 GetRandomPositionWithinArea(Transform spawnArea)
    {
        RectTransform areaRect = spawnArea.GetComponent<RectTransform>();
        Vector2 min = areaRect.TransformPoint(areaRect.rect.min);
        Vector2 max = areaRect.TransformPoint(areaRect.rect.max);

        return new Vector2(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y)
        );
    }
}

