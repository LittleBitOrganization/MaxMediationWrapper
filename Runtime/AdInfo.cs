namespace LittleBitGames.Ads.AdUnits
{
    public class AdInfo : IAdInfo
    {
        public AdInfo(MaxSdkBase.AdInfo adInfo)
        {
            Revenue = adInfo.Revenue;
            Placement = adInfo.Placement;
            RevenuePrecision = adInfo.RevenuePrecision;
            NetworkName = adInfo.NetworkName;
            AdFormat = adInfo.AdFormat;
            NetworkPlacement = adInfo.NetworkPlacement;
            AdUnitIdentifier = adInfo.AdUnitIdentifier;
            CreativeIdentifier = adInfo.CreativeIdentifier;
            DspName = adInfo.DspName;
        }

        public string AdUnitIdentifier { get; }
        public string AdFormat { get; }
        public string NetworkName { get; }
        public string NetworkPlacement { get; }
        public string Placement { get; }
        public string CreativeIdentifier { get; }
        public double Revenue { get; }
        public string RevenuePrecision { get; }
        public string DspName { get; }
    }
}