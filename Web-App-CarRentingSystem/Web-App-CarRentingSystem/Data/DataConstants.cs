namespace Web_App_CarRentingSystem.Data
{
    public class DataConstants
    {
        public const int CarBrandMinLength = 2;
        public const int CarBrandMaxLength = 20;
        public const int CarModelMinLength = 2;
        public const int CarModelMaxLength = 30;
        public const int CarDescriptionMinLength = 10;
        public const int CarDescriptionMaxLength = 1000;
        public const int CarYearMinValue = 2000;
        public const int CarYearMaxValue = 2100;

        public class Dealer
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }
    }
}
