using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GridTile
{
    public GameObject Canvas;
    public GameObject tileObject;
    public int minLayer = 0;
    public int maxLayer = 3;
    private Vector2 size;

    public GridTile(GameObject tilePrefab, Vector2 position, Vector2 size)
    {
        this.size = size;
        Canvas = GameObject.Find("Canvas");        
        tileObject = Object.Instantiate(original: tilePrefab, position: position, rotation: Quaternion.identity);
        tileObject.transform.localScale = new Vector3(size.x, size.y, 1);
        SpriteRenderer sr = tileObject.GetComponent<SpriteRenderer>();        
        if (sr != null)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        }
    }
    public void PlaceImage(Sprite sprite, Vector2 positionOffset, float rotation, Vector2 scale, bool toFind = false)
    {
        if (sprite == null) return;

        GameObject imageObject = new GameObject("TileImage");
        imageObject.transform.SetParent(tileObject.transform);
        SpriteRenderer sr = imageObject.AddComponent<SpriteRenderer>();
        if (toFind)
        {
            sr.renderingLayerMask = 99;
            Button button = imageObject.AddComponent<Button>();

            button.onClick.AddListener(() => ImageFound(imageObject));
        }
        else
        {
            sr.renderingLayerMask = (uint)UnityEngine.Random.Range(minLayer, maxLayer);
        }

        sr.sprite = sprite;

        imageObject.transform.localPosition = new Vector2(
            positionOffset.x * size.x,
            positionOffset.y * size.y
        );
        imageObject.transform.localRotation = Quaternion.Euler(0, 0, rotation);
        imageObject.transform.localScale = new Vector3(scale.x, scale.y, 1);
    }

    void ImageFound(GameObject imageObject)
    {
        Sprite foundSprite = imageObject.GetComponent<SpriteRenderer>().sprite;

        GameObject.Destroy(imageObject);

        for (int i = 0; i < GridSystem.inventorySlots.Length; i++)
        {
            Image panelImage = GridSystem.inventorySlots[i].GetComponentInChildren<Image>();
            if (panelImage.sprite == foundSprite)
            {
                GameObject.Destroy(GridSystem.inventorySlots[i]);

                GridSystem.inventorySlots[i] = null;

                for (int j = i + 1; j < GridSystem.inventorySlots.Length; j++)
                {
                    GridSystem.inventorySlots[j - 1] = GridSystem.inventorySlots[j];
                }

                System.Array.Resize(ref GridSystem.inventorySlots, GridSystem.inventorySlots.Length - 1);
                break;
            }
        }
    }
}

public class GridSystem : MonoBehaviour
{
    
    public GameObject tilePrefab; 
    public List<Sprite> sprites;
    public int imageInGrid = 20;
    public int rows = 10;         
    public int cols = 10;   
    public float minRotation, maxRotation;
    public float minScale, maxScale;
    public float minOffset, maxOffset;
    public bool rotate = true;
    private GridTile[,] gridTiles;
    private Vector2 tileSize;
    public int imagesToFind;
    public GameObject Canvas, panelPrefab;
    public static GameObject[] inventorySlots;
    private Sprite[] spritesToFind;

    void Start()
    {
        InitializeGrid();
        GetRandomSpritestoFind(imagesToFind);
        PlaceRandomImages();
        ShowInventoryPanels();
    }

    void InitializeGrid()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        tileSize = new Vector2(2 * screenBounds.x / cols, 2 * screenBounds.y / rows);

        gridTiles = new GridTile[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector2 position = new Vector2(
                    -screenBounds.x + col * tileSize.x + tileSize.x / 2,
                    -screenBounds.y + row * tileSize.y + tileSize.y / 2
                );

                gridTiles[row, col] = new GridTile(tilePrefab, position, tileSize);
            }
        }
    }



    public void PlaceImageInTile(int row, int col, Sprite sprite, Vector2 positionOffset, float rotation, Vector2 scale, bool interactable = false)
    {
        
        if (row >= 0 && row < rows && col >= 0 && col < cols)
        {
            if (interactable)
            {
                gridTiles[row, col].PlaceImage(sprite, positionOffset, rotation, scale, interactable);
            }
            else
            {
                gridTiles[row, col].PlaceImage(sprite, positionOffset, rotation, scale);
            }
        }
        else
        {
            Debug.LogWarning("Invalid tile position!");
        }
    }

    void PlaceRandomImages()
    {                
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                for (int k = 0; k < imageInGrid; k++)
                {
                    float scale = (float)System.Math.Round(Random.Range(minScale, maxScale),2);
#if rotate
                    int[] rotas = new int[4] { 0, 90, 180, 270 };
                    PlaceImageInTile(i, j,
                        sprites[Random.Range((int)0f, (int)(float)sprites.Length)],
                        new Vector2(Random.Range(minOffset, maxOffset), Random.Range(minOffset, maxOffset)),
                        rotas[Random.Range(0,rotas.Length)],
#else
                    PlaceImageInTile(i, j,
                        sprites[Random.Range((int)0f, (int)(float)sprites.Count)],
                        new Vector2(Random.Range(minOffset, maxOffset), Random.Range(minOffset, maxOffset)),
                        0,
#endif
                    new Vector2(scale, scale)
                    );
                }                
            }
        }

        for (int i = 0; i < spritesToFind.Length; i++)
        {
            float scale = (float)System.Math.Round(Random.Range(minScale, maxScale), 2);
#if rotate
            int[] rotas = new int[4] { 0, 90, 180, 270 };
            PlaceImageInTile(Random.Range(2,rows-3), Random.Range(2,cols-3),
                sprites[Random.Range((int)0f, (int)(float)sprites.Length)],
                new Vector2(Random.Range(minOffset, maxOffset), Random.Range(minOffset, maxOffset)),
                rotas[Random.Range(0,rotas.Length)],
#else
            PlaceImageInTile(Random.Range(2,rows-3), Random.Range(2,cols-3),
                sprites[Random.Range((int)0f, (int)(float)sprites.Count)],
                new Vector2(Random.Range(minOffset, maxOffset), Random.Range(minOffset, maxOffset)),
                0,
#endif
            new Vector2(scale, scale),
            true
            );
        }
    }

    public void GetRandomSpritestoFind(int count)
    {
        spritesToFind = new Sprite[count];
        for (int i = 0; i < spritesToFind.Length; i++)
        {
            int j = Random.Range(0, sprites.Count);
            spritesToFind[i] = sprites[j];
            sprites.RemoveAt(j);
        }
    }

    void ShowInventoryPanels()
    {
        inventorySlots = new GameObject[spritesToFind.Length];

        float panelWidth = 64f;
        float panelHeight = 64f;
        float imageWidth = 48f;
        float imageHeight = 48f;
        float spacing = 10f;
        float startX = spacing;

        for (int i = 0; i < spritesToFind.Length; i++)
        {           
            inventorySlots[i] = Object.Instantiate(panelPrefab, Canvas.transform) as GameObject;           
            RectTransform panelRect = inventorySlots[i].GetComponent<RectTransform>();
            panelRect.sizeDelta = new Vector2(panelWidth, panelHeight);
            panelRect.anchorMin = new Vector2(0, 0);
            panelRect.anchorMax = new Vector2(0, 0);
            panelRect.pivot = new Vector2(0, 0);
           
            float xPos = startX + (panelWidth + spacing) * i;
            float yPos = spacing;
           
            panelRect.anchoredPosition = new Vector2(xPos, yPos);
           
            Image inventorySlotImage = inventorySlots[i].transform.Find("Image").GetComponent<Image>();
            if (inventorySlotImage != null)
            {               
                if (spritesToFind != null && i < spritesToFind.Length)
                {
                    inventorySlotImage.sprite = spritesToFind[i];
                }
               
                RectTransform imageRect = inventorySlotImage.GetComponent<RectTransform>();
                imageRect.sizeDelta = new Vector2(imageWidth, imageHeight);
                imageRect.anchorMin = new Vector2(0.5f, 0.5f);
                imageRect.anchorMax = new Vector2(0.5f, 0.5f);
                imageRect.pivot = new Vector2(0.5f, 0.5f);
                imageRect.anchoredPosition = Vector2.zero;
            }
        }
    }
}