using RedRats.Safety;

namespace Rogium.Systems.Validation
{
    /// <summary>
    /// Contains methods for validating general asset parameters.
    /// </summary>
    public static class AssetValidation
    {
        public const int TitleMinAllowedCharacters = 2;
        public const int TitleMaxAllowedCharacters = 64;
        public const int DescriptionMinAllowedCharacters = 0;
        public const int DescriptionMaxAllowedCharacters = 2000;

        public const int MaxFrameDuration = 2000;
        
        public const float MaxUseDelay = 60f;
        public const float MaxKnockbackSelfTime = 30f;
        public const float MaxKnockbackOtherTime = 30f;

        public const int MaxEnemyHealth = 20000;
        public const float MaxEnemyInvincibilityTime = 60f;
        
        /// <summary>
        /// Validate an Asset Title.
        /// </summary>
        /// <param name="title">The title to validate.</param>
        public static void ValidateTitle(string title)
        {
            SafetyNet.EnsureStringInRange(title, TitleMinAllowedCharacters, TitleMaxAllowedCharacters, "Title");
        }

        /// <summary>
        /// Validate (usually) long descriptive text.
        /// </summary>
        /// <param name="description">The description text to validate.</param>
        public static void ValidateDescription(string description)
        {
            SafetyNet.EnsureStringInRange(description, DescriptionMinAllowedCharacters, DescriptionMaxAllowedCharacters, "Description");
        }
    }
}