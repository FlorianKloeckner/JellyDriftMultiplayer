﻿using System;
using MilkShake;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class ShakeController : MonoBehaviour
{
	// Token: 0x0600019C RID: 412 RVA: 0x00008CA0 File Offset: 0x00006EA0
	private void Awake()
	{
		ShakeController.Instance = this;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00008CA8 File Offset: 0x00006EA8
	private void Start()
	{
		this.shaker = CameraController.Instance.transform.GetComponentInChildren<Shaker>();
		this.shakeInstance = this.shaker.Shake(this.preset, null);
		this.shakeInstance.StrengthScale = 0f;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00008CFC File Offset: 0x00006EFC
	public void Shake()
	{
		this.shaker.Shake(this.crashShake, null);
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00008D24 File Offset: 0x00006F24
	private void FixedUpdate()
	{
		if (!this.car)
		{
			return;
		}
		float magnitude = this.car.acceleration.magnitude;
		float num = 0f;
		foreach (Suspension suspension in this.car.wheelPositions)
		{
			if (suspension.traction > num)
			{
				num = suspension.traction;
			}
		}
		if (this.car.speed < 2f)
		{
			num = 0f;
		}
		this.shakeInstance.StrengthScale = Mathf.Clamp(num * 0.5f, 0f, 1f);
	}

	// Token: 0x040001AA RID: 426
	public Car car;

	// Token: 0x040001AB RID: 427
	private Shaker shaker;

	// Token: 0x040001AC RID: 428
	public ShakePreset preset;

	// Token: 0x040001AD RID: 429
	public ShakePreset crashShake;

	// Token: 0x040001AE RID: 430
	private ShakeInstance shakeInstance;

	// Token: 0x040001AF RID: 431
	public static ShakeController Instance;
}
