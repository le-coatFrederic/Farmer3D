using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField]
    private float range = 2.0f;

    public Inventory inventory;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject pickupText;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("Item"))
            {
                Debug.Log("Item touché");

                pickupText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (inventory.HaveSpace())
                    {
                        inventory.AddItem(hit.transform.gameObject.GetComponent<Item>().item);
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        Debug.Log("Inventaire plein");
                    }

                }
            }
        }
        else
        {
            pickupText.SetActive(false);
        }
    }
}
