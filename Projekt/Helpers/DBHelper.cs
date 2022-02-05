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

        public String GetUserRole(LoginModel userData)
        {
            var allUsers = GetUsers();
            foreach (var user in allUsers)
            {
                if (userData.Login == user.Username && userData.Password == user.Password)
                {
                    return user.UserRole;
                }
            }
            return "NOTLOGGED";
        }

        public RegisterStatus RegisterUser(LoginModel userData)
        {
            if (userData.Password != userData.Password2)
                return RegisterStatus.PasswordsNotEqual;

            if (userData.Login.Length >= 50 || userData.Password.Length >= 50)
                return RegisterStatus.LoginOrPasswordTooLong;

            var allUsers = GetUsers();
            foreach (var user in allUsers)
                if (userData.Login == user.Username)
                    return RegisterStatus.LoginAlreadyTaken;

            addUserToDatabase(userData);
            return RegisterStatus.OK;
        }

        public List<Products> GetProducts()
        {
            var productsTable = DataContext.Products;
            List<Products> products = new List<Products>();

            foreach (var p in productsTable)
            {
                products.Add(p);
            }
            return products;
        }

        public List<Products> GetProducts(string str)
        {
            var products = GetProducts();        
            List<Products> models = new List<Products>();
            foreach (var product in products)
            {
                if (product.Name.ToLower().Contains(str.ToLower()))
                {
                    models.Add(product);
                }
            }

            return models;
        }

        public List<Users> GetUsers()
        {
            var usersTable = DataContext.Users;
            List<Users> users = new List<Users>();

            foreach (var u in usersTable)
            {
                users.Add(u);
            }
            return users;
        }

        public List<Orders> GetOrders()
        {
            var ordersTable = DataContext.Orders;
            List<Orders> orders = new List<Orders>();

            foreach (var o in ordersTable)
            {
                orders.Add(o);
            }
            return orders;
        }

        private void addUserToDatabase(LoginModel userData)
        {
            dummyUsers.Add(userData);
        }

        private static DBHelper helper = null;
        private static ShopDataContext DataContext = new ShopDataContext();

        private List<LoginModel> dummyUsers = new List<LoginModel> {
            new LoginModel {Login = "Admin", Password = "123456"},
            new LoginModel {Login = "Haniuś", Password = "hophop"},
            new LoginModel {Login = "Bartuś", Password = "miałmiał"},
            new LoginModel {Login = "Piotruś", Password = "hauhau"},
            new LoginModel {Login = "PowerRangers18", Password = "ninjastorm"}
        };

        private List<ProductModel> dummyProducts = new List<ProductModel> {
            new ProductModel { ID = 1, Name = "Buty Nike XXL", Description = "markowe buty nike", Price = 35800},
            new ProductModel { ID = 2, Name = "Czekolada", Description = "najlepsza czekolada na świecie pozdro", Price = 1250},
            new ProductModel { ID = 3, Name = "Palma japońska", Description = "ładna palma doniczkowa do twojego domu", Price = 5000},
            new ProductModel { ID = 4, Name = "Figurka Matki Boskiej", Description = "Figurka Matki Boskiej, podświetlana LED RGB", Price = 9599}
        };
    }
}