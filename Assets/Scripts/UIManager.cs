using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class UIManager : MonoBehaviour
{
	// Token: 0x060001CD RID: 461 RVA: 0x0000993A File Offset: 0x00007B3A
	public bool showCodePanel = true;
	[SerializeField]private GameObject MultiplayerBanner; 
	private void Awake()
	{
		UIManager.Instance = this;
	}
	private void Start(){
		if(GameState.Instance.gamemode != Gamemode.Multiplayer)
		{
			DisableMultiplayerBanner();
		}
	}

	public void DisableMultiplayerBanner()
	{
        MultiplayerBanner.SetActive(false);
		showCodePanel = false;
    }

	public void ToggleMultiplayerBanner()
	{
		if (MultiplayerBanner.activeSelf)
		{
			MultiplayerBanner.SetActive(false);
		}
		else if (showCodePanel)
		{
			MultiplayerBanner.SetActive(true);
		}
	}
	// Token: 0x040001F0 RID: 496
	public Transform splitPos;

	// Token: 0x040001F1 RID: 497
	public static UIManager Instance;
}
