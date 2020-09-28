using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateRisingInfo : MonoBehaviour
{
	public Race raceToGetRisingInfo;
	public bool alreadyChoosed;
	[Header("Risign Sign")]
	public TextMeshProUGUI risingTitle;
	public Image risingElementIcon;
	[Space]
	[Header("Rising Element")]
	public TextMeshProUGUI risingElement;
	public Image risingIcon;
	[Space]
	[Header("Strong Against Element")]
	public TextMeshProUGUI strongElement;
	public Image strongIcon;
	[Header("Weak Against Element")]
	public TextMeshProUGUI weakElement;
	public Image weakIcon;

	private void Start()
	{
		if (alreadyChoosed)
		{
			risingIcon.sprite = PlayerRisignChoice.playerRisignChoice.raceIcon;
			risingTitle.text = PlayerRisignChoice.playerRisignChoice.raceName + ":";

			risingElement.text = PlayerRisignChoice.playerRisignChoice.raceElement.elementName;
			risingElementIcon.sprite = PlayerRisignChoice.playerRisignChoice.raceElement.elementIcon;

			strongElement.text = PlayerRisignChoice.playerRisignChoice.raceElement.strongAgainst[0].elementName;
			strongIcon.sprite = PlayerRisignChoice.playerRisignChoice.raceElement.strongAgainst[0].elementIcon;

			weakElement.text = PlayerRisignChoice.playerRisignChoice.raceElement.strongAgainst[0].strongAgainst[0].strongAgainst[0].elementName;
			weakIcon.sprite = PlayerRisignChoice.playerRisignChoice.raceElement.strongAgainst[0].strongAgainst[0].strongAgainst[0].elementIcon;

		}
		else
		{
			risingElement.text = raceToGetRisingInfo.raceElement.elementName;
			risingElementIcon.sprite = raceToGetRisingInfo.raceElement.elementIcon;

			risingTitle.text = raceToGetRisingInfo.raceName + ":";
			risingIcon.sprite = raceToGetRisingInfo.raceIcon;

			strongElement.text = raceToGetRisingInfo.raceElement.strongAgainst[0].elementName;
			strongIcon.sprite = raceToGetRisingInfo.raceElement.strongAgainst[0].elementIcon;

			weakElement.text = raceToGetRisingInfo.raceElement.strongAgainst[0].strongAgainst[0].strongAgainst[0].elementName;
			weakIcon.sprite = raceToGetRisingInfo.raceElement.strongAgainst[0].strongAgainst[0].strongAgainst[0].elementIcon;
		}

	}

}
