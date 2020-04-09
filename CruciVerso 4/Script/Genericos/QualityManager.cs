using UnityEngine;
using System.Collections;

public class QualityManager : MonoBehaviour  {
	
	bool antialiasing;
	bool vSync;
	int textureQuality;
	int anisotropicQuality;
	
	public UILabel labelTexureQuality;
	public UILabel labelAnisotropicQuality;
	
	void Start()
	{
		LoadConfig();
	}
	
	public void Antialiasing()
	{
		if(antialiasing)
		{
			antialiasing = false;
			QualitySettings.antiAliasing = 2;
		}
		else
		{
			antialiasing = true;
			QualitySettings.antiAliasing = 0;
		}
	}	
	
	public void Vsync()
	{
		if(vSync)
		{
			vSync = false;
			QualitySettings.vSyncCount = 2;
		}
		else
		{
			vSync = true;
			QualitySettings.vSyncCount = 0;
		}
	}
	
	public void TextureQuality()
	{

		textureQuality++;
		
		if(textureQuality>2)
		{
			textureQuality = 0;
		}
		
		QualitySettings.masterTextureLimit = textureQuality;
		
		switch(textureQuality)
		{
			case 0:
			labelTexureQuality.text = "High";	
			break;
			case 1:
			labelTexureQuality.text = "Medium";	
			break;
			case 2:
			labelTexureQuality.text = "Low";	
			break;
		}
	}
	
	public void AnisotropicQuality()
	{

		anisotropicQuality++;
		
		if(anisotropicQuality>1)
		{
			anisotropicQuality = 0;
		}
		
		switch(anisotropicQuality)
		{
			case 0:
			labelAnisotropicQuality.text = "Enable";
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
			break;
			case 1:
			labelAnisotropicQuality.text = "Disable";
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			break;
		}
	}
	
	void LoadConfig()
	{
		
	}

}
