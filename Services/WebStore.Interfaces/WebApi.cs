namespace WebStore.Interfaces
{
    public static class WebApi
    {
        private const string Api = "api/";
        private const string Version1 = "v1/";
        
        public const string Employees = Api + Version1 + "employees";
        public const string Values = Api + Version1 + "values";
        public const string Products = Api + Version1 + "products";
    }
}
