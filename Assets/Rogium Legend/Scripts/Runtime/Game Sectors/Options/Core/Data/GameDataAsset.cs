using Rogium.Editors.Core;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Contains all settings data for the game.
    /// </summary>
    public class GameDataAsset : IDataAsset
    {
        private PreferencesAsset preferences;
        private InputBindingsAsset inputBindings;

        private GameDataAsset() { }
        
        public void UpdatePreferences(PreferencesAsset newPreferences) => preferences = new PreferencesAsset.Builder().AsCopy(newPreferences).Build();
        public void UpdateInputBindings(InputBindingsAsset newInputBindings) => inputBindings = new InputBindingsAsset.Builder().AsCopy(newInputBindings).Build();
        
        public override string ToString() => $"{Title}";
        public string ID { get => "Z"; }
        public string Title { get => "Game Data"; }
        public PreferencesAsset Preferences { get => preferences; }
        public InputBindingsAsset InputBindings { get => inputBindings; }
        
        public class Builder
        {
            private readonly GameDataAsset Asset = new();
            
            public Builder()
            {
                Asset.preferences = new PreferencesAsset.Builder().Build();
                Asset.inputBindings = new InputBindingsAsset.Builder().Build();
            }
            
            public Builder WithPreferences(PreferencesAsset preferences)
            {
                Asset.preferences = new PreferencesAsset.Builder().AsCopy(preferences).Build();
                return this;
            }
            
            public Builder WithInputBindings(InputBindingsAsset inputBindings)
            {
                Asset.inputBindings = new InputBindingsAsset.Builder().AsCopy(inputBindings).Build();
                return this;
            }
            
            public Builder AsCopy(GameDataAsset asset)
            {
                Asset.preferences = new PreferencesAsset.Builder().AsCopy(asset.preferences).Build();
                Asset.inputBindings = new InputBindingsAsset.Builder().AsCopy(asset.inputBindings).Build();
                return this;
            }
            
            public GameDataAsset Build() => Asset;
        }
    }
}