using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public static CamaraController instance;

    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, BigmapCamera, minimapCamera;

    private bool bigmapActive;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!bigmapActive)
            {
                ActivatedBigMap();
            }
            else
            {
                DeActivatedBigMap();
            }
        }
    }

    public void ChangeTarget(Transform Newtarget)
    {
        target = Newtarget;
    }

    public void ActivatedBigMap()
    {
        if (!levelmanager.instance.pause)
        {
            bigmapActive = true;
            BigmapCamera.enabled = true;
            mainCamera.enabled = false;

            Playercontrol.instance.canMove = false;
            Time.timeScale = 0;
            UIController.instance.MapDisplay.SetActive(false);
        }
    }
    public void DeActivatedBigMap()
    {
        if (!levelmanager.instance.pause)
        {
            bigmapActive = false;
            BigmapCamera.enabled = false;
            mainCamera.enabled = true;

            Playercontrol.instance.canMove = true;
            Time.timeScale = 1;
            UIController.instance.MapDisplay.SetActive(true);
        }
    }
}
