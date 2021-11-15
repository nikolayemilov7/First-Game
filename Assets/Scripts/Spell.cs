using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    private GameObject _firePrefab;
    private GameObject _instantiatedObj;

    public bool spell = false;

    private float _cooldown = 3;
    private float _nextFire = 0;

    public void ActivateSpell()
    {
        if (Time.time > _nextFire)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                spell = true;
            }

            if (Input.GetMouseButtonDown(0) & spell)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "Map")
                    {
                        _instantiatedObj = Instantiate(_firePrefab, hit.point, Quaternion.Euler(270f, 0, 0));
                        _nextFire = Time.time + _cooldown;
                        Destroy(_instantiatedObj, 3);
                    }
                    else if (hit.transform.tag == "Enemy")
                    {
                        _instantiatedObj = Instantiate(_firePrefab, hit.point, Quaternion.Euler(270f, 0, 0));
                        GameObject.Find("Enemy").GetComponent<Enemy>().Damage(100);
                        _nextFire = Time.time + _cooldown;
                        Destroy(_instantiatedObj, 3);
                    }
                }
                spell = false;
            }
        }
    }
}