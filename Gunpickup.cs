using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpickup : MonoBehaviour
{

    public Gun TheGun;

    public float waittoBeCollected = .5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waittoBeCollected > 0)
        {
            waittoBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waittoBeCollected <= 0)
        {
            bool hasGun = false;
            foreach(Gun gunToCheck in Playercontrol.instance.availableGun)
            {
                if(TheGun.weaponName == gunToCheck.weaponName)
                {
                    hasGun = true;
                }
            }

            if (!hasGun)
            {
                Gun gunclone = Instantiate(TheGun);
                gunclone.transform.parent = Playercontrol.instance.gunHand;
                gunclone.transform.position = Playercontrol.instance.gunHand.position;
                gunclone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunclone.transform.localScale = Vector3.one;

                Playercontrol.instance.availableGun.Add(gunclone);

                //Playercontrol.instance.currentGun = Playercontrol.instance.availableGun.Count + 1;

                Playercontrol.instance.switchGun();
            }

            audioManager.instance.playSFX(7);

            Destroy(gameObject);
        }
    }
}
