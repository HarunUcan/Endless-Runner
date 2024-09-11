using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private int _lives = 3;
    private bool _canTakeDamage = true;
    private bool _isBlinkingActive = false;
    [SerializeField] private MeshRenderer[] _playerMeshRenderers;
    [SerializeField] private SkinnedMeshRenderer[] _playerMeshes;
    private bool _isMagnetActive = false;
    private float _magnetTimer = 0f;

    private bool _isStarActive = false;
    private float _starTimer = 0f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        //Blink
        if (!_canTakeDamage && !_isBlinkingActive)
        {
            _isBlinkingActive = true;
            StartCoroutine(Blink());
        }
        if (_isMagnetActive)
        {
            _magnetTimer += Time.deltaTime;
            Magnet();
            if (_magnetTimer >= 10f)
            {
                _isMagnetActive = false;
                _magnetTimer = 0f;
            }
        }
        if (_isStarActive)
        {
            _starTimer += Time.deltaTime;
            if (_starTimer >= 10f)
            {
                _isStarActive = false;
                GameManager.Instance.IsStarActive = false;
                _starTimer = 0f;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (_canTakeDamage && (other.tag == "Obstacle" || other.tag == "PartOfObstacle" || other.tag == "Plane"))
        {
            if (_lives > 0)
                _lives--;
            if (_lives <= 0)
                GameManager.Instance.GameOver();

            UIManager.Instance.UpdateLives(_lives);

            GetComponent<PlayerMovement>().SlowDown();
            ChaserManager.Instance.ComeCloserToPlayer();

            StartCoroutine(DontTakeDamage(3f));
        }
        else if (other.tag == "Gold")
        {
            GameManager.Instance.IncreaseScore(10);
            GameManager.Instance.IncreaseCoinCounter();
            other.GetComponent<MeshRenderer>().enabled = false;
            other.GetComponent<AudioSource>().Play();
            other.GetComponent<CoinManager>().IsMagnetActive = false;
        }
        else if (other.tag == "Magnet")
        {
            other.gameObject.SetActive(false);
            _magnetTimer = 0f;
            ActivateMagnet();
        }
        else if (other.tag == "Star")
        {
            other.gameObject.SetActive(false);
            _starTimer = 0f;
            ActivateStar();
        }
    }

    public void SetAvatar(Avatar avatar)
    {
        GetComponent<Animator>().avatar = avatar;
    }
    private void Magnet()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 8f);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Gold")
            {
                collider.GetComponent<CoinManager>().IsMagnetActive = true;
                //collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, 20f * Time.deltaTime);
                //collider.transform.DOMove(transform.position, 0.5f);
            }
        }
    }

    public void ActivateMagnet()
    {
        _isMagnetActive = true;
    }
    public void ActivateStar()
    {
        _isStarActive = true;
        GameManager.Instance.IsStarActive = true;
    }

    IEnumerator DontTakeDamage(float time)
    {
        _canTakeDamage = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(time);
        _canTakeDamage = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    IEnumerator Blink()
    {

        //_playerMesh1.enabled = false;
        foreach (var mesh in _playerMeshes)
            mesh.enabled = false;
        foreach (var mesh in _playerMeshRenderers)
            mesh.enabled = false;
        
        //_playerMesh2.enabled = false;

        yield return new WaitForSeconds(0.1f);

        foreach (var mesh in _playerMeshes)
            mesh.enabled = true;
        foreach (var mesh in _playerMeshRenderers)
            mesh.enabled = true;

        //_playerMesh1.enabled = true;
        //_playerMesh2.enabled = true;

        yield return new WaitForSeconds(0.1f);
        _isBlinkingActive = false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawWireSphere(transform.position, 8f);
    }
}
