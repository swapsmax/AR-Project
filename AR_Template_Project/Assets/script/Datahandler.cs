using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Datahandler : MonoBehaviour
{
    private GameObject lights;
    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Item> items;

    private int currid = 0;

    private static Datahandler instance;
    public static Datahandler Instance {
        get{
            if (instance == null)
            {
                instance = FindObjectOfType<Datahandler>();
            }
            return instance;
        }
    }
    //Local loaditems method
    void LoadItems(){
        var items_obj = Resources.LoadAll("items", typeof(Item));
        foreach (var item in items_obj){
            items.Add(item as Item);
        }
    }

    void CreateButton(){
        foreach(Item i in items){
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ItemId = currid;
            b.ButtonTexture = i.itemImage;
            currid++;
        }
    }

    public void SetLight(int id){
        lights = items[id].itemPrefab;
    }

    public GameObject GetLight()
    {
        return lights;
    }
    private void Start(){
        LoadItems();
        //await Get(label);
        CreateButton();
    }

    

    


    // //Cloud load item method
    // public async Task Get(String label){
    //     var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
    //     foreach (var location in locations){
    //         var obj = await Addressables.LoadAssetAsync<Item>(location).Task;
    //         items.Add(obj);
    //     }
    // }
}
