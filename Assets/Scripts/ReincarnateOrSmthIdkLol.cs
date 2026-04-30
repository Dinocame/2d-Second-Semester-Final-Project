using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReincarnateOrSmthIdkLol : MonoBehaviour
{
    public GameObject player;
    private CinemachineVirtualCamera _cinemachine;
    public float soulPower = 0f;
    private TMP_Text soulPowerText;
    private LevelManager levelManager;

    void Start()
    { 
        _cinemachine = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        soulPowerText =  GameObject.FindWithTag("soulpower").GetComponent<TMP_Text>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.AcceptYourFate.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        soulPower -= Time.deltaTime*3;
        UpdateSoulText();
        if (soulPower <= 0f || Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Corpse"))
        {
            soulPower += collision.gameObject.GetComponent<SoulValue>().soulValue;
            Vector3 pos = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
            Reincarnate(pos);
        }
    }

    void Reincarnate(Vector3 pos)
    {
        GameObject currentPlayer = Instantiate(player, pos, Quaternion.identity);
        // Set cinemachine target to ghost instead of player
        _cinemachine.Follow = currentPlayer.transform;
        _cinemachine.LookAt = currentPlayer.transform;
        PlayerDeath temp = currentPlayer.GetComponent<PlayerDeath>();
        temp.soulPower = soulPower;
        temp.isDead = false;
        levelManager.AcceptYourFate.SetActive(false);
        Destroy(gameObject);
    }

    void UpdateSoulText()
    {
        soulPowerText.text = "Soul Power: " + Mathf.FloorToInt(soulPower);;
    }
}
