using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	public PlayerStats playerStats;
	public Gun gun;
	public Text ammoText;

	[SerializeField] RectTransform healthBarFill;

	private void Update()
	{
		SetHealthAmount(playerStats.GetHealthPct());
		SetAmmoAmount();
	}

	void SetHealthAmount(float _amount)
	{
		if(_amount >= 0f)
			healthBarFill.localScale = new Vector3(1f, _amount, 1f);
		if (_amount < 0f)
			healthBarFill.localScale = new Vector3(1f, 0, 1f);
	}

	void SetAmmoAmount()
	{
		ammoText.text = gun.GetAmmo().ToString("0");
	}
}
