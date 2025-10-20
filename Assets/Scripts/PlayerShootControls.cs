using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootControls : MonoBehaviour
{
    [SerializeField] GameObject Gun;
    [SerializeField] GameObject BulletSpawn;
    [SerializeField] GameObject BulletPrefab;

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

        HandleGunShooting();
    }

    private void HandleGunShooting()
    {
        if (Input.GetMouseButton(0))
        {
            ObjectPoolManager.SpawnObject(BulletPrefab, BulletSpawn.transform.position, Quaternion.identity);
        }
    }
}
