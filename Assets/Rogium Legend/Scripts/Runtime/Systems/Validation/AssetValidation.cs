using RedRats.Safety;

namespace Rogium.Systems.Validation
{
    /// <summary>
    /// Contains methods for validating general asset parameters.
    /// </summary>
    public static class AssetValidation
    {
        public const int TitleMinAllowedCharacters = 2;
        public const int TitleMaxAllowedCharacters = 24;
        public const int DescriptionMinAllowedCharacters = 0;
        public const int DescriptionMaxAllowedCharacters = 2000;

        public const int MaxFrameDuration = 2000;
        
        public const float MaxUseDelay = 60f;

        public const int MaxEnemyHealth = 20000;
        public const float MaxEnemyInvincibilityTime = 60f;
        
        /// <summary>
        /// Validate an Asset Title.
        /// </summary>
        /// <param name="title">The title to validate.</param>
        public static void ValidateTitle(string title)
        {
            Preconditions.IsStringInRange(title, TitleMinAllowedCharacters, TitleMaxAllowedCharacters, "Title");
        }

        /// <summary>
        /// Validate (usually) long descriptive text.
        /// </summary>
        /// <param name="description">The description text to validate.</param>
        public static void ValidateDescription(string description)
        {
            Preconditions.IsStringInRange(description, DescriptionMinAllowedCharacters, DescriptionMaxAllowedCharacters, "Description");
        }
    }
}