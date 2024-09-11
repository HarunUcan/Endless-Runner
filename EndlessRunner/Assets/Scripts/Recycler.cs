using UnityEngine;

public class Recycler : MonoBehaviour
{
    [SerializeField] private float _recycleOffset = 15f;
    [SerializeField] private Transform _buildingsSpawnPoint;
    private Vector3 _buildingsStartPoint;

    private void Start()
    {
        _buildingsStartPoint = _buildingsSpawnPoint.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
            other.gameObject.SetActive(false);
        else if (other.tag == "Ground")
        {
            GetAllObjectsInPlatform();
            GameManager.Instance.ReplaceGamePlatform();
        }


    }

    void GetAllObjectsInPlatform()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _recycleOffset);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Obstacle" || collider.gameObject.tag == "SlopeObstacle")
            {
                collider.gameObject.SetActive(false);
                collider.transform.SetParent(null);
                GameManager.Instance.AddObstacleToPool(collider.gameObject);
            }
            else if (collider.gameObject.tag == "GoldUnit")
            {
                foreach (Transform childOfGoldUnit in collider.transform)
                {
                    childOfGoldUnit.GetComponent<MeshRenderer>().enabled = true;
                    childOfGoldUnit.GetComponent<CoinManager>().ResetLocalPosition();
                    childOfGoldUnit.GetComponent<CoinManager>().IsMagnetActive = false;
                }

                collider.gameObject.SetActive(false);
                collider.transform.SetParent(null);
                GameManager.Instance.AddGoldToPool(collider.gameObject);
            }
            else if(collider.gameObject.tag == "MagnetUnit")
            {
                foreach (Transform childOfGoldUnit in collider.transform)
                    childOfGoldUnit.gameObject.SetActive(true);                

                collider.gameObject.SetActive(false);
                collider.transform.SetParent(null);
                GameManager.Instance.AddMagnetToPool(collider.gameObject);
            }
            else if(collider.gameObject.tag == "StarUnit")
            {
                foreach (Transform childOfGoldUnit in collider.transform)
                    childOfGoldUnit.gameObject.SetActive(true);                

                collider.gameObject.SetActive(false);
                collider.transform.SetParent(null);
                GameManager.Instance.AddStarToPool(collider.gameObject);
            }
            else if(collider.gameObject.tag == "Buildings")
            {
                collider.transform.position = _buildingsStartPoint;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawWireSphere(transform.position, _recycleOffset);
    }

}
