using UnityEngine;

public class CubeScale : MonoBehaviour
{
    [SerializeField] private GameObject ob;

    private Vector3 originalScale = Vector3.one;

    void Start()
    {
        if (ob != null)
        {
            originalScale = ob.transform.localScale;
        }
    }

    void Update()
    {
        if (ob == null)
            return;

        if (Input.GetButtonDown("ShrinkVertically"))
        {
            ob.transform.localScale = new Vector3(0.2f, originalScale.y, originalScale.z);
        }

        else if (Input.GetButtonDown("ShrinkHorizontally"))
        {
            ob.transform.localScale = new Vector3(originalScale.x, 0.2f, originalScale.z);
        }

        else
        {
            ob.transform.localScale = originalScale;
        }
    }
}
