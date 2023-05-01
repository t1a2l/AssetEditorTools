using ICities;

namespace AssetEditorTools
{
    /// <summary>
    /// AI Changer main mod class; provides Mod Info and handles loading
    /// </summary>
    public class Mod : IUserMod
    {
        public string Name => "Asset Editor Tools";

        public string Description => "Allows you to change the PrefabAI, UICategory, UIPriority and ItemClass in the Asset Editor. including building, vehicle, citizen, network etc.";
    }

}