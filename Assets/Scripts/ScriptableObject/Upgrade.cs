using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[CreateAssetMenu(menuName = "SO Dict/Upgrade")]
public class Upgrade : MonoBehaviour
{
    public enum EUpgrade
    {
        MaxHealth,
        Damage,
        Speed,
        Recover,
    }

    [field : SerializeField] public EUpgrade UpgradeType {  get; private set; }
    [field: SerializeField] public float Amount { get; private set; }
    [field: SerializeField] public int Price { get; private set; }

    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI priceTag;

    private void Start()
    {
        priceTag.text = Price.ToString();
        description.text = UpgradeType.ToString() + " + " + (Amount * 100).ToString() +"%";
    }

    public void Upgrading()
    {
        if(Player.Instance.Status.CurCoinHave >= Price)
        { 
            Player.Instance.Status.UpgradeStatus(this); 
        }
    }
}
