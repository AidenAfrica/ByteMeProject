using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletInstantiation : MonoBehaviour
{
    //Instantiate is function used to create clones of GameObjects, specifically a prefab
    public GameObject Bullet;
    public Transform GunPoint;
    private Rigidbody2D rb;
    public float BulletSpeed = 30f;
    public int Ammo = 5;

    private void Start()
    {
        AmmoText.text = "" + Ammo;
        AmmoReload();

    }
    public void Update()
    {
        AmmoReload();

        //Rigidbody2D BulletInstance;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Ammo > 0)
            {

                GameObject BulletInstance = Instantiate(Bullet, GunPoint.position, Quaternion.identity);
                //BulletInstance.AddForce(GunPoint.right * 5000f);
                //BulletInstance = Vector3.right * BulletInstance.velocity;
                //If you press the Fire1 button that you selected, then the prefab of the game object will instantiate
                rb = BulletInstance.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector3.up * BulletSpeed;
                }
                Ammo--;
            }
            else if (Ammo == 0)
            {
                return;
            }


        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Ammo = 5;
            AmmoReload();
        }


    }
    public TextMeshProUGUI AmmoText;

    public void AmmoReload()
    {
        AmmoText.text = "" + Ammo;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            Destroy(Bullet);
        }
    }

}
