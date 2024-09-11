using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChaserManager : MonoBehaviour
{
    public static ChaserManager Instance { get; private set; }
    private float _posZ;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _posZ = transform.position.z;
    }
    void Start()
    {
        transform.DOMoveZ(-2.5f, 5f);
    }

    void Update()
    {
        
    }
    public void ComeCloserToPlayer()
    {
        transform.DOMoveZ(_posZ, .5f).OnComplete(() => transform.DOMoveZ(-2.5f, 5f));
    }
}
