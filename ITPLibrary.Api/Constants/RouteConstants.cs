namespace ITPLibrary.Api.Constants
{
    public class RouteConstants
    {
        #region Books
        public const string Popular = "popular-books";
        public const string Promoted = "promoted-books";
        public const string Details = "book-details";
        #endregion

        #region Users
        public const string Login = "login";
        public const string Register = "register";
        public const string Recovery = "password-recovery";
        #endregion

        #region Shopping Cart
        public const string AddShoppingCartItem = "add-item";
        public const string DeleteShoppingCartItem = "delete-item";
        public const string GetShoppingCart = "get-shopping-cart";
        #endregion

        #region Order
        public const string PostOrder = "post-order";
        public const string GetOrders = "get-orders";
        public const string UpdateOrder = "update-order";
        #endregion
    }
}
