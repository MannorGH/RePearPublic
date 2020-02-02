using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltLogic : MonoBehaviour
{
    public static ConveyorBeltLogic instance = null;

    [SerializeField]
    private GameObject BeltBackground;

    public List<GameObject> BeltItems;
    [SerializeField]
    private int maxBeltItemAmount = 8;
    [SerializeField]
    private float movePercentagePerSecond = 10.0f;

    private float timeUntilNextSpawn = 0.0f;
    [SerializeField]
    private float spawnCooldown = 4.0f;

    private string conveyorBeltBoxPrefabPath = "Prefabs/ConveyorBeltBox";
    [SerializeField]
    private int[] objectTypeProbabilities;
    [SerializeField]
    private GameObject[] objectsBouncyPrefabs, objectsBreakingPrefabs, objectsDeadlyPrefabs, objectsNormalPrefabs, objectsSlipperyPrefabs, objectsStickyPrefabs;
    private int probabilitySum;

    [SerializeField]
    float offsetBeltEnd = 40.0f;
    [SerializeField]
    float offsetBetweenObjects = 20.0f;
    [SerializeField]
    private float boxBottomOffset = 60.0f;

    // Initializes variables.
    private void Awake()
    {
        BeltItems = new List<GameObject>();

        probabilitySum = 0;
        for (int i = 0;  i < objectTypeProbabilities.Length; i++)
        {
            probabilitySum += objectTypeProbabilities[i];
        }

        if (instance == null)
        {
            instance = this;
        } else
        {
            Debug.Log("More than one instance of ConveyorBerltLogic.");
        }
    }

    // Moves conveyer belt boxes, spawns new boxes after cooldown.
    private void Update()
    {
        int currentIndex = 0;
        foreach (GameObject currentBeltItem in BeltItems)
        {
            RectTransform currentRectTransform = currentBeltItem.GetComponent<RectTransform>();
            currentRectTransform.anchoredPosition -= new Vector2(1.0f, 0.0f) * Time.deltaTime * (1920.0f/100.0f) * movePercentagePerSecond;

            if (currentRectTransform.anchoredPosition.x <= (currentRectTransform.rect.width * currentIndex) + offsetBeltEnd + (offsetBetweenObjects * currentIndex))
            {
                currentRectTransform.anchoredPosition = new Vector2((currentRectTransform.rect.width * currentIndex) + offsetBeltEnd + (offsetBetweenObjects * currentIndex), currentRectTransform.anchoredPosition.y);
            }
            currentIndex++;
        }

        if (timeUntilNextSpawn <= 0.0f && BeltItems.Count < maxBeltItemAmount)
        {
            addBeltItem();
            timeUntilNextSpawn = spawnCooldown;
        }
        timeUntilNextSpawn -= Time.deltaTime;
    }

    // Adds a new item boxto the conveyor belt.
    private void addBeltItem()
    {
        if (BeltItems.Count <= maxBeltItemAmount)
        {
            int probabilityRandom = Random.Range(0, probabilitySum);
            int choosenIndex = -1;
            int tempProbabilitySum = probabilitySum;
            for (int i = objectTypeProbabilities.Length - 1; i >= 0; i--)
            {
                tempProbabilitySum -= objectTypeProbabilities[i];
                if (tempProbabilitySum <= probabilityRandom)
                {
                    choosenIndex = i;
                    break;
                }
            }
            GameObject choosenPrefab = null;
            switch (choosenIndex)
            {
                case 0:
                    {
                        choosenPrefab = objectsBouncyPrefabs[Random.Range(0, objectsBouncyPrefabs.Length)];
                        break;
                    }
                case 1:
                    {
                        choosenPrefab = objectsBreakingPrefabs[Random.Range(0, objectsBreakingPrefabs.Length)];
                        break;
                    }
                case 2:
                    {
                        choosenPrefab = objectsDeadlyPrefabs[Random.Range(0, objectsDeadlyPrefabs.Length)];
                        break;
                    }
                case 3:
                    {
                        choosenPrefab = objectsNormalPrefabs[Random.Range(0, objectsNormalPrefabs.Length)];
                        break;
                    }
                case 4:
                    {
                        choosenPrefab = objectsSlipperyPrefabs[Random.Range(0, objectsSlipperyPrefabs.Length)];
                        break;
                    }
                case 5:
                    {
                        choosenPrefab = objectsStickyPrefabs[Random.Range(0, objectsStickyPrefabs.Length)];
                        break;
                    }
                default:
                    {
                        Debug.Log("Invalid object type index.");
                        break;
                    }
            }

            GameObject newBeltItem = Instantiate(choosenPrefab, new Vector3(9999.0f, 9999.0f, 0.0f), Quaternion.identity);
            GameObject newBeltBox = Instantiate(newBeltItem.GetComponent<ObjectPlacement>().ItemBoxPrefab, Vector3.zero, Quaternion.identity);

            newBeltBox.transform.SetParent(BeltBackground.transform, false);
            newBeltBox.transform.localScale = Vector3.one;
            newBeltBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(1920.0f - BeltBackground.GetComponent<RectTransform>().anchoredPosition.x, boxBottomOffset, 0.0f); // 1920 is the reference width of the canvas.

            newBeltItem.GetComponent<ObjectPlacement>().attachedItemBoxObject = newBeltBox;
            newBeltBox.GetComponent<ItemBoxLogic>().attachedItemObject = newBeltItem;

            BeltItems.Add(newBeltBox);
        }
    }
}
