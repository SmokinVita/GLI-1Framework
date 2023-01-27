using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float _canFire = -1f;
    private float _fireRate = 2f;
    private int _ammoAmount = 25;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIManager.Instance.UpdateAmmoCount(_ammoAmount);
    }

    // Update is called once per frame
    void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame && _ammoAmount > 0 && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Debug.Log("fire!");
            _ammoAmount--;
            UIManager.Instance.UpdateAmmoCount(_ammoAmount);

            RaycastHit hitInfo;
            Ray origin = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            
            if (Physics.Raycast(origin, out hitInfo, Mathf.Infinity, 1 << 6))
            {
                Debug.DrawLine(origin.origin, hitInfo.point);
                Debug.Log(hitInfo.collider.name);
                var hit = hitInfo.collider.GetComponent<IDamageable>();
                if (hit != null)
                    hit.Damage();

            }
        }
    }
}
