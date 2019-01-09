using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour {

    private GameObject target;
    public Animator anim;
    [SerializeField]
    private GameObject loot;
    private Vector3 pos;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip openChest;
    [SerializeField]
    private AudioClip shootItem;

    private int interactRange = 4;
    private bool openAnim;
    public bool isOpened;
    private bool spawnedItems;

    

    // Use this for initialization
    void Start() {
        anim = GetComponentInChildren<Animator>();
        pos = transform.position;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Interact());
    }

    IEnumerator Interact() {
        yield return new WaitForSeconds(3f);
        audioSource.PlayOneShot(openChest);
        isOpened = true;
        anim.SetBool("openAnim", true);
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems() {
    for (int i = 0; i < Random.Range(9,10); i++) {
        yield return new WaitForSeconds(.3f);
        audioSource.PlayOneShot(shootItem);
        GameObject newitem = Instantiate(loot, new Vector3(pos.x - .5f, pos.y + 1f, pos.z), Quaternion.Euler(0, 0, 0)) as GameObject;
        Vector3 direction = new Vector3(Random.Range(-5, 4), 1.5f, Random.Range(0, 5));
            newitem.GetComponent<Rigidbody>().AddForce(direction * 30);
        }
    }
}
