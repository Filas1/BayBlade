using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private GlobalData globalData;
    private GlobalBaybladeData[] bayblades => globalData.Bayblades;
    public int playerMoney = 1000;
    [SerializeField] private LayerMask previewMask;
    
    private VisualElement root;
    private Label moneyValue;
    private Label beybladeName;
    private Label beybladeDescription;
    private Label priceValue;
    private Label attackStat;
    private Label defenseStat;
    private Label staminaStat;
    private Button buyButton;
    private Button leftButton;
    private Button rightButton;
    private VisualElement beybladeRender;
    [SerializeField] private Camera renderCamera;
    [SerializeField] private Transform containerForBayblade;
    private int layerForPreview;
    
    private int currentIndex = 0;
    private RenderTexture renderTexture;
    private GameObject currentBeybladeModel;

    private void OnEnable()
    {
        UIDocument document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        
        // Get UI elements
        moneyValue = root.Q<Label>("MoneyValue");
        beybladeName = root.Q<Label>("BeybladeName");
        beybladeDescription = root.Q<Label>("BeybladeDescription");
        priceValue = root.Q<Label>("PriceValue");
        attackStat = root.Q<Label>("AttackStat");
        defenseStat = root.Q<Label>("DefenseStat");
        staminaStat = root.Q<Label>("StaminaStat");
        buyButton = root.Q<Button>("BuyButton");
        leftButton = root.Q<Button>("LeftButton");
        rightButton = root.Q<Button>("RightButton");
        beybladeRender = root.Q<VisualElement>("beyblade-render");
        beybladeRender.RegisterCallback<GeometryChangedEvent>((evt) =>
        {
            renderTexture = new RenderTexture((int)evt.newRect.width ,(int)evt.newRect.height , 16, RenderTextureFormat.ARGB32);
            renderTexture.Create();
            renderCamera.targetTexture = renderTexture;
        
            beybladeRender.style.backgroundImage = new StyleBackground()
            {
                value = Background.FromRenderTexture(renderTexture)
            };
            
        });
        // Set up buttons
        buyButton.clicked += OnBuyButtonClicked;
        leftButton.clicked += OnLeftButtonClicked;
        rightButton.clicked += OnRightButtonClicked;
        root.Q<Button>("BackButton").clicked += OnBackButtonClicked;
        
        // Create render texture for 3D model
        SetupRenderTexture();
        
        // Display initial Beyblade
        UpdateDisplay();
    }

    private void Start()
    {
        SortingLayer.GetLayerValueFromName("BeybladePreview");
    }

    private void SetupRenderTexture()
    {
        renderTexture = new RenderTexture((int)beybladeRender.transform.scale.x ,(int)beybladeRender.transform.scale.y , 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();
        renderCamera.targetTexture = renderTexture;
        
        beybladeRender.style.backgroundImage = new StyleBackground()
        {
            value = Background.FromRenderTexture(renderTexture)
        };
    }

    private void UpdateDisplay()
    {
        if (bayblades.Length == 0) return;
        
        var current = bayblades[currentIndex];
        
        // Update UI elements
        beybladeName.text = current.name;
        beybladeDescription.text = current.Description;
        priceValue.text = current.Price.ToString();
        moneyValue.text = playerMoney.ToString();
        attackStat.text = $"ATTACK: {current.MaxAttack}";
        defenseStat.text = $"DEFENSE: {current.MaxDefense}";
        staminaStat.text = $"STAMINA: {current.MaxStamina}";
        
        // Update buy button based on ownership
        if (ReferenceEquals(current, globalData.Equipped))
        {
            buyButton.text = "EQUIPPED";
            buyButton.RemoveFromClassList("buy-button");
            buyButton.RemoveFromClassList("owned-button");
            buyButton.AddToClassList("equipped-button");
            buyButton.SetEnabled(false);
        }
        else if (current.Owned)
        {
            buyButton.text = "EQUIP";
            buyButton.RemoveFromClassList("buy-button");
            buyButton.RemoveFromClassList("equipped-button");
            buyButton.AddToClassList("owned-button");
            buyButton.SetEnabled(true);
        }
        else
        {
            buyButton.text = "BUY";
            buyButton.RemoveFromClassList("owned-button");
            buyButton.RemoveFromClassList("equipped-button");
            buyButton.AddToClassList("buy-button");
            buyButton.SetEnabled(playerMoney >= current.Price);
        }
        
        // Update 3D model
        UpdateBeybladeModel();
    }

    private void UpdateBeybladeModel()
    {
        // Remove current model if it exists
        if (currentBeybladeModel)
            Destroy(currentBeybladeModel);
            
        // Instantiate new model
        if (!bayblades[currentIndex].Prefab) return;
        currentBeybladeModel = Instantiate(bayblades[currentIndex].Prefab, containerForBayblade, true);
        

        // Set layer for render camera
        currentBeybladeModel.GetComponent<Rigidbody>().isKinematic = true;
        SetLayerRecursively(currentBeybladeModel,3);
    }
    
    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    private void Update()
    {
        currentBeybladeModel.transform.Rotate(Vector3.up * (30 * Time.deltaTime));
    }

    private void OnBuyButtonClicked()
    {
        var current = bayblades[currentIndex];
        
        if (current.Owned)
        {
            globalData.Equipped = current;
        }
        else if (playerMoney >= current.Price)
        {
            // Buy the Beyblade
            playerMoney -= (int)current.Price;
            current.Owned = true;
        }
        
        UpdateDisplay();
    }

    private void OnLeftButtonClicked()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = bayblades.Length - 1;
            
        UpdateDisplay();
    }

    private void OnRightButtonClicked()
    {
        currentIndex++;
        if (currentIndex >= bayblades.Length)
            currentIndex = 0;
            
        UpdateDisplay();
    }

    private void OnBackButtonClicked()
    {
        // Return to main menu
        // SceneManager.LoadScene("MainMenu");
        
        // Or deactivate this panel and activate main menu panel
        gameObject.SetActive(false);
        // mainMenuPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        // Clean up resources
        if (renderTexture)
        {
            renderTexture.Release();
            Destroy(renderTexture);
        }
        
        if (currentBeybladeModel)
            Destroy(currentBeybladeModel);
            
        if (renderCamera)
            Destroy(renderCamera.gameObject);
    }
}