using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Vector3 _startPosition;
    private Transform _player;
    public bool IsMagnetActive = false;
    void Start()
    {
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad(); 
        jsonSaveLoad.LoadData();
        GetComponent<AudioSource>().volume = PlayerStats.sfxVolume;
        if (!PlayerStats.isSfxOn)
            GetComponent<AudioSource>().volume = 0;
        _startPosition = transform.localPosition;
        _player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0f);
        if (IsMagnetActive)
        {
            MoveToPlayer();
        }
    }
    public void ResetLocalPosition()
    {
        //transform.DOLocalMove(_startPosition, 0.01f);
        transform.localPosition = _startPosition;
    }

    public void MoveToPlayer()
    {
        //transform.DOMove(_player.position, 0.25f);
        transform.position = Vector3.MoveTowards(transform.position, _player.position, 40f * Time.deltaTime);
    }


}
