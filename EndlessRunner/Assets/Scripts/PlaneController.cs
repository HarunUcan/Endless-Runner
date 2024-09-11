using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private GameObject _planeEngine;

    private void Start()
    {
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.LoadData();
        GetComponent<AudioSource>().volume = PlayerStats.sfxVolume;
        if (!PlayerStats.isSfxOn)
            GetComponent<AudioSource>().volume = 0;
    }
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, _speed);
        EngineRotation();
    }

    void EngineRotation()
    {
        _planeEngine.transform.Rotate(0, 0, 15);
    }
}
