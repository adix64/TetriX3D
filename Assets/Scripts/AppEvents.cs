namespace AppEvent
{
	public enum ScreenFading
	{
		FadeToBlack,
		FadeToScene,
	}
    public enum FightEvents
    {
        IncrementCombo, BreakCombo
    }
    public enum HeroChangeEvents
    {
        ChooseBatmanSkin, EnableEnemy, DisableEnemy
    }
    public enum DashEvents
    {
        BeginDash, EndDash
    }

}

static class BuppetsPostProcessSettings {
    public const float initialContrast = 1f, lightningContrast = 1.618f;
    public const float initialExposure = 1.75f, lightningExposureMin = 2, lightningExposureMax = 5f;
    public const float initSaturation = 1f;
    public const float lightingSaturation = .6f;
    public const float lightningIntensity = 1f;
};