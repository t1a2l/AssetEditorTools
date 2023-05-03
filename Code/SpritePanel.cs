using ColossalFramework.UI;
using AssetEditorTools.UI;

namespace AssetEditorTools
{
	public class SpritePanel : UIPanel
	{
		public UILabel m_spritelabel;
		public UIButton m_copy;
		public UIButton m_paste;

		public UITextureAtlas u_Atlas;
		public UITextureAtlas u_InfoTooltipAtlas;

		public string u_Thumbnail;
		public string u_InfoTooltipThumbnail;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 25;

			m_spritelabel = UILabels.AddLabel(this, 0.0f, 4.0f, "Copy/Paste Sprites", 181, 1, UIHorizontalAlignment.Left, "Duplicates the thumbnail and tooltip used ingame.");
			m_copy = UIButtons.AddButton(this, 190.0f, 0.0f, "Copy", 90, 25, 0.9f, 4, "Copies the currently loaded asset's tooltip and thumbnail sprites to memory.");			
			m_paste = UIButtons.AddButton(this, 298.0f, 0.0f, "Paste", 90, 25, 0.9f, 4, "Pastes the tooltip and thumbnail sprites in to memory over the currently loaded asset's.");
			m_paste.state = UIButton.ButtonState.Disabled;

			gameObject.AddComponent<SpriteEditorController>();
		}

	}
}
