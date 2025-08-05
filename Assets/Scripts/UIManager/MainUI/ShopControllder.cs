using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopControllder : MonoBehaviour
{
    [SerializeField] public List<CharacterSO> heroes;
    [SerializeField] public List<ItemSO> items;

    public GameObject shopItem;
    public Transform contentPanel;

    private void Start()
    {
        UpdateHero();
    }

    public void UpdateHeroUI(List<HeroModel> heroes)
    {
        //foreach(Transform child in contentPanel)
        //{
        //    Destroy(child.gameObject);
        //}

        foreach (var hero in heroes)
        {
            GameObject item = Instantiate(shopItem, Vector3.zero, Quaternion.identity, contentPanel);

            TextMeshProUGUI nameText = item.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI priceText = item.transform.Find("Price").GetComponent<TextMeshProUGUI>();


            nameText.text = hero.heroName;
            priceText.text = hero.price.ToString();
        }
    }

    private void UpdateHero()
    {
        List<HeroModel> heroList = new List<HeroModel>();
        foreach (var hero in heroes)
        {
            heroList.Add(hero.ToModel());
        }

        UpdateHeroUI(heroList);
    }


}
