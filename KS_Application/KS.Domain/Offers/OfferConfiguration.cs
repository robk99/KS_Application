namespace KS.Domain.Offers
{
    /// <summary>
    /// Provides a constants from Business Logic for an Offers
    /// </summary>
    public static class OfferConfiguration
    {
        // Hides the total number of offers from a client (end user) 
        #region OfferNumber
        public const long InitialOfferNumber = 1000;
        public const int OfferNumberIncrement = 10;
        #endregion
    }
}
