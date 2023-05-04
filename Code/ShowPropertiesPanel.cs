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
			var view = UIView.GetAView();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 25;

			m_showPropertiesButton = UIButtons.AddButton(this, 150.0f, 0.0f, "Hide Properties", 150.0f, 25.0f, 0.9f, 4, "Properties");

			gameObject.AddComponent<ShowPropertiesEditorController>();
		}

	}
}
