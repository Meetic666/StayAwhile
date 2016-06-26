using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    bool[] m_PlayerDead;

    bool m_IsLoading;

	// Use this for initialization
	void Start ()
    {
        BasePlayer[] players = FindObjectsOfType<BasePlayer>();

        m_PlayerDead = new bool[players.Length];

        EventManager.Instance.RegisterListener(typeof(PlayerDeathEventData), new GameEventDelegate(OnReceiveDeathEvent));
	}
	
    void OnReceiveRespawnEvent(EventData data)
    {
        BasePlayer player = ((PlayerDeathEventData)data).m_Player;

        m_PlayerDead[player.playerNum] = false;
    }

	// Update is called once per frame
	void OnReceiveDeathEvent (EventData data)
    {
        if(m_IsLoading)
        {
            return;
        }

        BasePlayer player = ((PlayerDeathEventData)data).m_Player;

        player.gameObject.SetActive(false);

        StartCoroutine(SetRespawnTimer(player));

        m_PlayerDead[player.playerNum] = true;

        CheckForEndGame();
    }

    IEnumerator SetRespawnTimer(BasePlayer player)
    {
        yield return new WaitForSeconds(5.0f);

        if(!m_IsLoading)
        {
            player.gameObject.SetActive(true);
        }
    }

    void CheckForEndGame()
    {
        bool isEndGame = true;

        foreach(bool isDead in m_PlayerDead)
        {
            isEndGame &= isDead;
        }

        if(isEndGame)
        {
            m_IsLoading = true;

            StartCoroutine(EndGameDelay());
        }
    }

    IEnumerator EndGameDelay()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadSceneAsync("Splash");
    }
}
