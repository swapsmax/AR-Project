using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    private Button btn;
    [SerializeField] private RawImage buttonImage;

    private int _itemId;
    private Sprite buttonTexture;

    public int ItemId{
        set {_itemId = value;}
    }
    public Sprite ButtonTexture{
        set { buttonTexture = value;
            buttonImage.texture = buttonTexture.texture;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(selectObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(ZoomManager.Instance.OnEntered(gameObject)){
            transform.DOScale(Vector3.one * 1.5f, 0.3f);
        }
        else{
            transform.DOScale(Vector3.one, 0.3f);
        }
    }

    //Select the object
    void selectObject(){
        Datahandler.Instance.SetLight(_itemId);
    }
}
