using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools
{
	public class SpriteEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;

		private SpritePanel SpritePanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			SpritePanel = m_view.FindUIComponent<SpritePanel>("SpritePanel");

			SpritePanel.m_copy.eventClick += CopySprite;
			SpritePanel.m_paste.eventClick += PasteSprite;
			SpritePanel.m_paste.state = UIButton.ButtonState.Disabled;

			m_toolController = ToolsModifierControl.toolController;
			
		}

		private void CopySprite(UIComponent component, UIMouseEventParameter eventParam) 
		{
			var prefabInfo = m_toolController.m_editPrefabInfo;
			if(prefabInfo.m_Atlas != null) 
			{ 
				SpritePanel.u_Atlas = prefabInfo.m_Atlas; 
				SpritePanel.m_paste.state = UIButton.ButtonState.Normal;
				if(prefabInfo.m_Thumbnail != null) 
				{ 
					SpritePanel.u_Thumbnail = prefabInfo.m_Thumbnail; 
					SpritePanel.m_paste.atlas = SpritePanel.u_Atlas;
					SpritePanel.m_paste.hoveredFgSprite = SpritePanel.u_Thumbnail;
				}
			}
			if(prefabInfo.m_InfoTooltipAtlas != null) 
			{ 
				SpritePanel.u_InfoTooltipAtlas = prefabInfo.m_InfoTooltipAtlas; 
				SpritePanel.m_paste.state = UIButton.ButtonState.Normal;
				if(prefabInfo.m_InfoTooltipThumbnail != null) 
				{
					SpritePanel.u_InfoTooltipThumbnail = prefabInfo.m_InfoTooltipThumbnail; 
					SpritePanel.m_paste.state = UIButton.ButtonState.Normal;
				}
				SpritePanel.m_paste.atlas = SpritePanel.u_InfoTooltipAtlas;
				SpritePanel.m_paste.normalBgSprite = SpritePanel.u_InfoTooltipThumbnail;
			}
		}

		private void PasteSprite(UIComponent component, UIMouseEventParameter eventParam) 
		{
			var prefabInfo = m_toolController.m_editPrefabInfo;
			if(SpritePanel.u_Atlas != null) prefabInfo.m_Atlas = SpritePanel.u_Atlas;
			if(SpritePanel.u_Thumbnail != null) prefabInfo.m_Thumbnail = SpritePanel.u_Thumbnail;
			if(SpritePanel.u_InfoTooltipAtlas !=null ) prefabInfo.m_InfoTooltipAtlas = SpritePanel.u_InfoTooltipAtlas;
			if(SpritePanel.u_InfoTooltipThumbnail !=null ) prefabInfo.m_InfoTooltipThumbnail = SpritePanel.u_InfoTooltipThumbnail;
		}

	}
}