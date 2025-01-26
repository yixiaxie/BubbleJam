using System.Collections.Generic;
using UnityEngine;

public class ThoughtBubbleManager : MonoBehaviour
{
    [Header("Player 1 Settings")]
    //public KeyCode player1Key = KeyCode.A;
    public Transform player1SpawnArea;
    public RectTransform player1LanguageSlot;
    public GameObject player1ThoughtBubblePrefab;
    public GameObject[] player1LanguageBubblePrefabs;

    [Header("Player 2 Settings")]
    //public KeyCode player2Key = KeyCode.UpArrow;
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

    private bool isPlayer1SlotFull = false;
    private bool isPlayer2SlotFull = false;
    private float slotEmptyDistance = 1.0f;
    public GameManager gameManager;

    void Update()
    {
        if (!gameManager.isGameActive)
        {
            return;
        }
        isPlayer1SlotFull = IsSlotFull(player1LanguageSlot);
        isPlayer2SlotFull = IsSlotFull(player2LanguageSlot);

        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    if (IsLeftSideKey(key)) // 左侧键盘
                    {
                        Debug.Log($"Player 1 pressed: {key}");
                        GenerateThoughtBubble(1, player1SpawnArea, player1ThoughtBubbles, player1LanguageSlot, player1LanguageBubblePrefabs);
                    }
                    else if (IsRightSideKey(key)) // 右侧键盘
                    {
                        Debug.Log($"Player 2 pressed: {key}");
                        GenerateThoughtBubble(2, player2SpawnArea, player2ThoughtBubbles, player2LanguageSlot, player2LanguageBubblePrefabs);
                    }
                }
            }
        }

        
        

        CheckAndGenerateLanguageBubble(1, player1ThoughtBubbles, player1LanguageSlot, player1LanguageBubblePrefabs, ref isPlayer1SlotFull);
        CheckAndGenerateLanguageBubble(2, player2ThoughtBubbles, player2LanguageSlot, player2LanguageBubblePrefabs, ref isPlayer2SlotFull);
    }

    bool IsLeftSideKey(KeyCode key)
    {
        return key == KeyCode.Q || key == KeyCode.W || key == KeyCode.E || key == KeyCode.R || key == KeyCode.T ||
               key == KeyCode.A || key == KeyCode.S || key == KeyCode.D || key == KeyCode.F || key == KeyCode.G ||
               key == KeyCode.Z || key == KeyCode.X || key == KeyCode.C || key == KeyCode.V || key == KeyCode.B ||
               key == KeyCode.Alpha1 || key == KeyCode.Alpha2 || key == KeyCode.Alpha3 || key == KeyCode.Alpha4 || key == KeyCode.Alpha5;
    }

    // 右侧键盘判断（排除回车）
    bool IsRightSideKey(KeyCode key)
    {
        return key == KeyCode.Y || key == KeyCode.U || key == KeyCode.I || key == KeyCode.O || key == KeyCode.P ||
               key == KeyCode.H || key == KeyCode.J || key == KeyCode.K || key == KeyCode.L || key == KeyCode.Semicolon ||
               key == KeyCode.N || key == KeyCode.M || key == KeyCode.Comma || key == KeyCode.Period || key == KeyCode.Slash ||
               key == KeyCode.Alpha6 || key == KeyCode.Alpha7 || key == KeyCode.Alpha8 || key == KeyCode.Alpha9 || key == KeyCode.Alpha0 ;
    }

    private bool IsSlotFull(RectTransform languageSlot)
    {
        // 遍历槽位下的每个子物体
        foreach (Transform child in languageSlot)
        {
            // 计算子物体与槽位的距离
            float distance = Vector2.Distance(languageSlot.position, child.position);

            // 如果距离小于指定的阈值，认为槽位仍满
            if (distance < slotEmptyDistance)
            {
                return true;
            }
        }
        // 如果没有子物体或所有子物体都超出距离，槽位为空
        return false;
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
        GameObject currentLanguageBubble = languageSlot.childCount > 0 ? languageSlot.GetChild(0).gameObject : null;
        //SpeechBubble bubble = currentLanguageBubble?.GetComponent<SpeechBubble>();
        //bool isFlying = bubble != null && bubble.isFlying;


        // 如果槽已满，检测升级
        if (isSlotFull)
        {
            CheckAndUpdateLanguageBubble(playerID, thoughtBubbles, languageSlot, languageBubblePrefabs);
            return;
        }
       

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

        // 消耗对应数量的想法气泡
        for (int i = 0; i < requiredThoughts; i++)
        {
            Destroy(thoughtBubbles[0]);
            thoughtBubbles.RemoveAt(0);
        }

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

        

        // 设置槽位状态为满
        isSlotFull = true;

        Debug.Log($"Player {playerID} generated a Language Bubble.");
    }


    void CheckAndUpdateLanguageBubble(int playerID,
       List<GameObject> thoughtBubbles,
       RectTransform languageSlot,
       GameObject[] languageBubblePrefabs)
    {
        GameObject currentLanguageBubble = languageSlot.childCount > 0 ? languageSlot.GetChild(0).gameObject : null;
        int currentLevel = -1;

        // 获取当前槽内语言气泡的等级
        if (currentLanguageBubble != null)
        {
            for (int i = 0; i < languageBubblePrefabs.Length; i++)
            {
                if (currentLanguageBubble.name.Contains(languageBubblePrefabs[i].name))
                {
                    currentLevel = i;
                    break;
                }
            }
        }

        // 检查是否有更高级别的 Bubble
        int thoughtCount = thoughtBubbles.Count;
        GameObject selectedPrefab = null;
        int requiredThoughts = 0;

        for (int i = languageBubbleRequirements.Length - 1; i > currentLevel; i--)
        {
            if (thoughtCount >= languageBubbleRequirements[i])
            {
                selectedPrefab = languageBubblePrefabs[i];
                requiredThoughts = languageBubbleRequirements[i];
                break;
            }
        }

        // 如果没有更高级别，退出
        if (selectedPrefab == null) return;

        // 替换语言气泡内容
        if (currentLanguageBubble != null)
        {
            Debug.Log($"Player {playerID} upgrades Language Bubble from Level {currentLevel + 1} to Level {requiredThoughts}");
            Destroy(currentLanguageBubble);
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


    
        }

        // 消耗差值的想法气泡
        int difference = requiredThoughts - languageBubbleRequirements[currentLevel];
        for (int i = 0; i < difference; i++)
        {
            if (thoughtBubbles.Count > 0)
            {
                Destroy(thoughtBubbles[0]);
                thoughtBubbles.RemoveAt(0);
            }
        }

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

