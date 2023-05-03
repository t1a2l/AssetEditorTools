using ColossalFramework.UI;
using AssetEditorTools.UI;

namespace AssetEditorTools
{
	public class ShowPropertiesPanel : UIPanel
	{
		public UIButton m_showPropertiesButton;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 25;

			m_showPropertiesButton = UIButtons.AddButton(this, 40.0f, 0.0f, "Properties", 324, 25, 0.9f, 4, "Properties");

			gameObject.AddComponent<ShowPropertiesEditorController>();
		}

	}
}
