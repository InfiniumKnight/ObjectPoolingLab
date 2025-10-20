using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootControls : MonoBehaviour
{
    
    [SerializeField] GameObject BulletSpawn;
    public float bulletSpeed = 20f;
    public ObjectPoolManager bulletPool;
    private float shootCooldown = 0.5f;


    public float sensitivity;

    public Camera cam;

    float rotY = 0f;

    float rotX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotY += Input.GetAxis("Mouse X") * sensitivity;
        rotX += Input.GetAxis("Mouse Y") * sensitivity;

        rotX = Mathf.Clamp(rotX, -70, 90);

        transform.localEulerAngles = new Vector3(0, rotY, 0);
        cam.transform.localEulerAngles = new Vector3(-rotX, 0, 0);

        if(shootCooldown >= 0.5)
        {
            HandleGunShooting();
        }
        else
        {
            shootCooldown += Time.deltaTime;
        }
        
    }

    private void HandleGunShooting()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject bullet = bulletPool.GetObject();
            bullet.GetComponent<BulletController>().ObjectPool = bulletPool;
            bullet.transform.position = BulletSpawn.transform.position;
            bullet.transform.rotation = BulletSpawn.transform.rotation;

            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }
            shootCooldown = 0;

            StartCoroutine(DeactivateBullet(bullet));
        }
    }

    IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        bulletPool.ReturnObject(bullet);
    }
}
