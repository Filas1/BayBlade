using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private GlobalData globalData;
    private GlobalBaybladeData[] bayblades => globalData.Bayblades;
    [SerializeField] private LayerMask previewMask;
    
    private VisualElement root;
    private Button buyButton;
    private Button leftButton;
    private Button rightButton;
    private Button backButton;
    private VisualElement beybladeRender;
    [SerializeField] private Camera renderCamera;
    [SerializeField] private Transform containerForBayblade;
    private int layerForPreview;
    
    private RenderTexture renderTexture;
    private GameObject currentBeybladeModel;
    private Transform cameraPivot;

    private void Awake()
    {
        cameraPivot = renderCamera.transform.parent;
        layerForPreview = SortingLayer.GetLayerValueFromName("BeybladePreview");
        InitUI();
    }

    private void InitUI()
    {
        if (!globalData.ViewedBayblade)
        {
            globalData.ViewedBayblade = bayblades[0];
        }
        UIDocument document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        buyButton = root.Q<Button>("BuyButton");
        leftButton = root.Q<Button>("LeftButton");
        rightButton = root.Q<Button>("RightButton");
        beybladeRender = root.Q<VisualElement>("beyblade-render");
        backButton = root.Q<Button>("BackButton");
        
        // Create render texture for 3D model
        SetupRenderTexture();
    }
    private void BeybladeRenderCallback(GeometryChangedEvent evt)
    {
        renderTexture.Release();
        renderTexture = new RenderTexture((int)evt.newRect.width, (int)evt.newRect.height, 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();
        renderCamera.targetTexture = renderTexture;

        beybladeRender.style.backgroundImage = new StyleBackground() { value = Background.FromRenderTexture(renderTexture) };
    }
    private void OnEnable()
    {
        beybladeRender.RegisterCallback<GeometryChangedEvent>(BeybladeRenderCallback);

        buyButton.clicked += OnBuyButtonClicked;
        leftButton.clicked += OnLeftButtonClicked;
        rightButton.clicked += OnRightButtonClicked;
        backButton.clicked += OnBackButtonClicked;
    }

    private void OnDisable()
    {
        beybladeRender.UnregisterCallback<GeometryChangedEvent>(BeybladeRenderCallback);

        buyButton.clicked -= OnBuyButtonClicked;
        leftButton.clicked -= OnLeftButtonClicked;
        rightButton.clicked -= OnRightButtonClicked;
        backButton.clicked -= OnBackButtonClicked;
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
        
        UpdateBeybladeModel();
    }

    /*
    private void UpdateDisplay()
    {
        if (bayblades.Length == 0) return;
        
        var current = bayblades[currentIndex];
        
        // Update UI elements
        beybladeName.text = current.name;
        beybladeDescription.text = current.Description;
        priceValue.text = current.Price.ToString();
        moneyValue.text = globalData.PlayerMoney.ToString();
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
            buyButton.SetEnabled(globalData.PlayerMoney >= current.Price);
        }
        
        // Update 3D model
        UpdateBeybladeModel();
    }*/

    private void UpdateBeybladeModel()
    {
        // Remove current model if it exists
        if (currentBeybladeModel)
            Destroy(currentBeybladeModel);
        
        if (!globalData.ViewedBayblade.Prefab) return;
        
        currentBeybladeModel = Instantiate(globalData.ViewedBayblade.Prefab, containerForBayblade, true);
        
        currentBeybladeModel.GetComponent<Rigidbody>().isKinematic = true;
        SetLayerRecursively(currentBeybladeModel, 3);
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
        currentBeybladeModel?.transform.Rotate(Vector3.up * (30 * Time.deltaTime));
    }

    private void OnBuyButtonClicked()
    {
        if (globalData.ViewedBayblade.Owned)
        {
            globalData.Equipped = globalData.ViewedBayblade;
        }
        if (globalData.PlayerMoney < globalData.ViewedBayblade.Price)
        {
            //Maybe say that player is poor xD
        }
        else
        {
            // Buy the Beyblade
            globalData.PlayerMoney -= globalData.ViewedBayblade.Price;
            globalData.ViewedBayblade.Owned = true;
        }
    }

    private void OnLeftButtonClicked()
    {
        if(bayblades.Length < 2) return;
        
        var currentIndex = Array.IndexOf(bayblades, globalData.ViewedBayblade);
        currentIndex--;
        
        if (currentIndex < 0)
            currentIndex = bayblades.Length - 1;

        globalData.ViewedBayblade = bayblades[currentIndex];
        
        UpdateBeybladeModel();
    }

    private void OnRightButtonClicked()
    {
        if(bayblades.Length < 2) return;
        
        var currentIndex = Array.IndexOf(bayblades, globalData.ViewedBayblade);
        currentIndex++;
        
        if (currentIndex >= bayblades.Length)
            currentIndex = 0;
        
        globalData.ViewedBayblade = bayblades[currentIndex];
        
        UpdateBeybladeModel();
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