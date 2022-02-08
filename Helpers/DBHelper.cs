using ProjektASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Helpers
{
    public class DBHelper
    {
        public static DBHelper GetInstance()
        {
            if (helper == null)
                return (helper = new DBHelper());

            return helper;
        }

        // poprawa (czek)
        public string GetUserRole(LoginModel userData)
        {
            return GetUserRole(userData.Login);
        }

        // poprawa (czek)
        public string GetUserRole(string username)
        {
            using (DB db = new DB())
            {
                var records = (from u in db.Users
                               where u.Username == username
                               select new
                               {
                                   UserRole = u.Role
                               }).ToList();

                if (records.Count != 0)
                    return records[0].UserRole;
                
                return "NOTLOGGED";
            }
        }

        public RegisterStatus RegisterUser(LoginModel userData)
        {
            if (userData.Password != userData.Password2)
                return RegisterStatus.PasswordsNotEqual;

            if (userData.Login.Length >= 50 || userData.Password.Length >= 50)
                return RegisterStatus.LoginOrPasswordTooLong;

            var allUsers = GetUsersLogins();
            foreach (var user in allUsers)
                if (userData.Login == user.Login)
                    return RegisterStatus.LoginAlreadyTaken;

            addUserToDatabase(userData);
            return RegisterStatus.OK;
        }

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (DB db = new DB())
            {
                var prodSet = db.Products;
                foreach (var product in prodSet)
                {
                    products.Add(new ProductModel { ID = product.ProductID, Name = product.Name, Description = product.Description, 
                                                    Price = product.Price, Hide = product.Hide });
                }
            }
            return products;
        }

        public List<ProductModel> GetProducts(string str)
        {
            var products = GetProducts();

            List<ProductModel> models = new List<ProductModel>();
            foreach (var product in products)
            {
                if (product.Name.ToLower().Contains(str.ToLower()))
                {
                    models.Add(product);
                }
            }

            return models;
        }

        public ProductModel GetProduct(int id)
        {
            DBProduct product;
            using (DB db = new DB())
            {
                product = db.Products.Find(id);
            }
            if (product == null)
                return new ProductModel();

            return new ProductModel { ID = product.ProductID, Name = product.Name, Description = product.Description, Price = product.Price, Hide = product.Hide };
        }

        public List<LoginModel> GetUsersLogins()
        {
            List<LoginModel> users = new List<LoginModel>();
            using (DB db = new DB())
            {
                foreach (var user in db.Users)
                {
                    users.Add(new LoginModel { Login = user.Username, Password = user.Password });
                }
            }
            return users;
        }

        public List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            using (DB db = new DB())
            {
                foreach (var user in db.Users)
                {
                    users.Add(new UserModel { Login = user.Username, Role = user.Role });
                }
            }
            return users;
        }

        public List<ProductModel> GetProductsListFromString(string productsString)
        {
            List<ProductModel> productModels = new List<ProductModel>();
            var stringList = productsString.Split(',');
            foreach (string item in stringList)
            {
                if (String.IsNullOrEmpty(item))
                    continue;

                productModels.Add(this.GetProduct(int.Parse(item)));
            }
            return productModels;
        }

        public int GetTotalPrice(List<ProductModel> productModels)
        {
            int totalPrice = 0;
            foreach (var product in productModels)
            {
                totalPrice += product.Price;
            }
            return totalPrice;
        }

        // poprawa częściowa (czek)
        public List<OrderModel> GetOrders()
        {
            List<OrderModel> orders = new List<OrderModel>();
            using (DB db = new DB())
            {
                foreach (var order in db.Orders)
                {
                    var products = GetProductsListFromString(order.ProductsIDList);
                    orders.Add(new OrderModel { ID = order.OrderID, User = order.UserID, Products = products, TotalPrice = GetTotalPrice(products)}); 
                }
            }
            return orders;
        }

        public List<OrderModel> GetCarts()
        {
            var users = GetUsers();
            List<OrderModel> list = new List<OrderModel>();
            foreach(var user in users)
            {
                var cart = GetUserCart(GetIdFromLogin(user.Login));
                if (cart.Products.Count != 0)
                    list.Add(cart);
            }
            return list;
        }

        public int GetIdFromLogin(string login)
        {
            using (DB db = new DB())
            {
                var records = (from u in db.Users
                               where u.Username == login
                               select new
                               {
                                   UserID = u.UserID
                               }).ToList();

                return records[0].UserID;
            }
            
        }

        public OrderModel GetUserCart(int userId)
        {
            OrderModel cart = new OrderModel();
            cart.Products = new List<ProductModel>();
            using(DB db = new DB())
            {
                var records = (from c in db.Carts
                               join p in db.Products
                               on c.ProductID equals p.ProductID
                               where c.UserID == userId
                               select new
                               {
                                   CartID = c.CartID,
                                   UserID = c.UserID,
                                   ProductID = p.ProductID,
                                   ProductName = p.Name,
                                   ProductDescription = p.Description,
                                   ProductPrice = p.Price,
                                   ProductHide = p.Hide
                               }).ToList();
                foreach (var record in records)
                {
                    cart.ID = record.CartID;
                    cart.TotalPrice += record.ProductPrice;
                    cart.User = record.UserID;
                    cart.Products.Add(new ProductModel { ID = record.ProductID, Name = record.ProductName, Description = record.ProductDescription, Price = record.ProductPrice, Hide = record.ProductHide });
                }
            }
            return cart;
        }

        // poprawa (czek)
        public void ClearCart(int id)
        {
            var userCart = GetUserCart(id).Products;
            foreach (var item in userCart)
            {
                removeFromCart(id, item);
            }
        }

        // poprawa (czek)
        public void AddOrder(OrderModel cart)
        {
            using(DB db = new DB())
            {
                var products = GetStringFromProductsList(cart.Products);
                db.Orders.Add(new DBOrder { UserID = cart.User, ProductsIDList = products });
                db.SaveChanges();
            }            
        }


        public void AddToCart(int userId, ProductModel product)
        {
            using (DB db = new DB())
            {
                db.Carts.Add(new DBCart { UserID = userId, ProductID = product.ID });
                db.SaveChanges();
            }
        }
        public void AddProduct(ProductModel product)
        {
            using(DB db = new DB())
            {
                db.Products.Add(new DBProduct { Name = product.Name, Description = product.Description, Price = product.Price, Hide = false });
                db.SaveChanges();
            }
        }
        public void DeleteProduct(int productId)
        {
            using (DB db = new DB())
            {
                db.Products.Find(productId).Hide = true;
                db.SaveChanges();
            }
        }

        // poprawa (czek)
        public void removeFromCart(int userId, ProductModel product)
        {
            using (DB db = new DB())
            {
                var cart = db.Carts.First(c => (c.UserID == userId && c.ProductID == product.ID));
                db.Carts.Attach(cart);
                db.Carts.Remove(cart);
                db.SaveChanges();
            }
        }

        private void addUserToDatabase(LoginModel userData)
        {
            using(DB db = new DB())
            {
                var newUser = new DBUser { Username = userData.Login, Password = userData.Password, Role = "USER" };
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        // poprawa (czek)
        private string GetStringFromProductsList(List<ProductModel> products)
        {
            string productsString = "";
            foreach(var item in products)
            {
                productsString += item.ID.ToString();
                productsString += ",";
            }

            return productsString;
        }

        private static DBHelper helper = null;
    }
}