using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    private Button btn;
    [SerializeField] private RawImage buttonImage;
    //public GameObject lights;

    private int _itemId;
    private Sprite _buttonTexture;

    public Sprite ButtonTexture
    {
        set
        {
            _buttonTexture = value;
            buttonImage.texture = _buttonTexture.texture;
        }
    }

    public int ItemId
    {
        set{ _itemId = value; }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
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

    void SelectObject()
    {
        Datahandler.Instance.SetLight(_itemId);
    }

}
