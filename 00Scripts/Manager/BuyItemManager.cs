using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemManager : MonoBehaviour
{
    public static BuyItemManager I;

    [SerializeField] private CanvasGroup buyItemListCanvasGroup;
    [SerializeField] private CanvasGroup buyItemAskCanvasGroup;
    
    [SerializeField] private TextMeshProUGUI askText;
    
    [SerializeField] private Button buyYesButton;
    [SerializeField] private Button buyNoButton;
    [SerializeField] private TextMeshProUGUI priceText;

    public GameObject tvArea;
    public ObjectAnimationHandler objAnimHandler;
    
    [Header("TV Item List")] public ItemList[] items;
    
    private int currentIndex;
    private string itemName;
    private int itemPrice;
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(BuyItemManager)) as BuyItemManager;
        }
        
        buyItemListCanvasGroup.alpha = 0;
        buyItemListCanvasGroup.blocksRaycasts = false;
        
        buyItemAskCanvasGroup.alpha = 0;
        buyItemAskCanvasGroup.blocksRaycasts = false;
        
        buyYesButton.onClick.AddListener(OnBuyYesButtonClick);
        buyNoButton.onClick.AddListener(OnBuyNoButtonClick);
    }
    
    public void ShowBuyItemList()
    {
        buyItemListCanvasGroup.alpha = 1;
        buyItemListCanvasGroup.blocksRaycasts = true;
        
        buyItemAskCanvasGroup.alpha = 0;
        buyItemAskCanvasGroup.blocksRaycasts = false;
    }
    
    public void ShowBuyItemAsk()
    {
        buyItemListCanvasGroup.alpha = 0;
        buyItemListCanvasGroup.blocksRaycasts = false;
        
        buyItemAskCanvasGroup.alpha = 1;
        buyItemAskCanvasGroup.blocksRaycasts = true;
    }
    
    public void SetBuyItemAsk()
    {
        string buyItemName = items[currentIndex].itemName;
        int buyItemPrice = items[currentIndex].itemPrice;
        
        askText.text = $"{buyItemName}을(를) 구매하시겠습니까?\n가격: {buyItemPrice}";
        
        this.itemName = buyItemName;
        this.itemPrice = buyItemPrice;
        
        ShowBuyItemAsk();
    }
    
    private void OnBuyYesButtonClick()
    {
        if (CurrencyManager.I.currency < itemPrice)
        {
            LogManager.I.GenerateLog("돈이 부족합니다.");
            return;
        }
        
        LogManager.I.GenerateLog($"{itemName}을(를) 구매하였습니다.");

        if (itemName is "어린왕자" or "눈의 여왕")
            BookManager.I.bookList.Find(x => x.title == itemName).canRead = true;
        
        CurrencyManager.I.SubtractCurrency(itemPrice);
        
        ShowBuyItemList();
    }
    
    public void HideCg()
    {
        buyItemListCanvasGroup.alpha = 0;
        buyItemListCanvasGroup.blocksRaycasts = false;
        
        buyItemAskCanvasGroup.alpha = 0;
        buyItemAskCanvasGroup.blocksRaycasts = false;
    }
    
    private void OnBuyNoButtonClick()
    {
        ShowBuyItemList();
    }

    public void Exit()
    {
        HideCg();
        tvArea.SetActive(false);
        objAnimHandler.StopAnimation();
        PlanManager.I.NextDay();
    }

    public void Next()
    {
        if (currentIndex >= items.Length - 1)
            currentIndex = 0;
        else
            currentIndex++;

        priceText.text = items[currentIndex].itemPrice.ToString();
        objAnimHandler.PlayAnimation(items[currentIndex].tvAnimString);
        
    }

    public void Init()
    {
        currentIndex = 0;
        priceText.text = items[currentIndex].itemPrice.ToString();
        objAnimHandler.PlayAnimation(items[currentIndex].tvAnimString);
    }
    
}

[Serializable]
public class ItemList
{
    public string itemName;
    public int itemPrice;
    public string tvAnimString;
}
