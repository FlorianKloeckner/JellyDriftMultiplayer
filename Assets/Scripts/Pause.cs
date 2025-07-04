﻿using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000030 RID: 48
public class Pause : MonoBehaviour
{
    // Token: 0x17000016 RID: 22
    // (get) Token: 0x060000F7 RID: 247 RVA: 0x000064E0 File Offset: 0x000046E0
    // (set) Token: 0x060000F8 RID: 248 RVA: 0x000064E8 File Offset: 0x000046E8
    public bool paused { get; set; }

	// Token: 0x060000F9 RID: 249 RVA: 0x000064F1 File Offset: 0x000046F1
	private void Awake()
	{
		Pause.Instance = this;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000064F9 File Offset: 0x000046F9
	public void PauseGame()
	{
		if (this.paused)
		{
			return;
		}
		if (GameState.Instance.gamemode != Gamemode.Multiplayer)
		{
			Time.timeScale = 0f;
		}
		else 
		{
			UIManager.Instance.ToggleMultiplayerBanner();
		}
		this.pauseMenu.SetActive(true);
		this.paused = true;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00006521 File Offset: 0x00004721
	public void ResumeGame()
	{
		Time.timeScale = 1f;
		this.pauseMenu.SetActive(false);
		this.paused = false;
		if (GameState.Instance.gamemode == Gamemode.Multiplayer) { UIManager.Instance.ToggleMultiplayerBanner(); }
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000654A File Offset: 0x0000474A
	public void Recover()
	{
		Time.timeScale = 1f;
		GameController.Instance.Recover();
		this.ResumeGame();
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00006566 File Offset: 0x00004766
	public void RestartGame()
	{
		Time.timeScale = 1f;
		this.pauseMenu.SetActive(false);
		this.paused = false;
		GameController.Instance.RestartGame();
	}



	// Token: 0x060000FF RID: 255 RVA: 0x0000658F File Offset: 0x0000478F
	public void TogglePause()
	{
		
		/*if (!GameController.Instance.playing)
		{
			return;
		}
		*/
		if (!this.paused)
		{
			this.PauseGame();
			return;
		}
		this.ResumeGame();
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000065B3 File Offset: 0x000047B3
	public void Quit()
	{
		NetworkManager.Singleton.Shutdown();
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
		this.paused = false;
	}

	// Token: 0x04000115 RID: 277
	public static Pause Instance;

	// Token: 0x04000116 RID: 278
	public GameObject pauseMenu;
	public GameObject codePanel;
	
}
