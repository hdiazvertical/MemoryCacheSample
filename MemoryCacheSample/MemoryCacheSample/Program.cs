using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace MemoryCacheSample {
    class Program {
        static void Main(string[] args) {
            //diccionario de permisos
            var permissions = new Dictionary<string, bool>();
            permissions.Add("p1", true);
            permissions.Add("p2", true);
            permissions.Add("p3", true);
            permissions.Add("p4", true);

            //diccionario de roles
            var roles = new Dictionary<string, bool>();
            roles.Add("r1", true);
            roles.Add("r2", true);

            //diccionario variables utiles globales en el sistema
            var global = new Dictionary<string, dynamic>();
            global.Add("EsmId", 1);
            global.Add("EsmName", "ESM 1");

            var cache = MemoryCache.Default;
            cache.Add("permissions", permissions, null);
            cache.Add("roles", roles, null);
            cache.Add("global", global, null);

            Console.WriteLine(HasPermission("p1").ToString());
            Console.WriteLine(HasRole("r1").ToString());
            Console.WriteLine(HasRole("r3").ToString());
            Console.WriteLine(string.Join(",", GetListPermissions().ToArray()));

            Console.ReadKey();
        }

        public static bool HasPermission(string permission) {
            var cache = MemoryCache.Default;
            var permissions = (IDictionary<string, bool>)cache.Get("permissions");
            return permissions.ContainsKey(permission) ? true : false;
        }

        public static bool HasRole(string roleName) {
            var cache = MemoryCache.Default;
            var permissions = (IDictionary<string, bool>)cache.Get("roles");
            return permissions.ContainsKey(roleName) ? true : false;
        }

        public static List<string> GetListPermissions() {
            var cache = MemoryCache.Default;
            var permissions = (IDictionary<string, bool>)cache.Get("permissions");
            return permissions.Keys.ToList();
        }

        public static List<string> GetListRoles() {
            var cache = MemoryCache.Default;
            var roles = (IDictionary<string, bool>)cache.Get("roles");
            return roles.Keys.ToList();
        }

        public static int GetCurrentEsmId() {
            var cache = MemoryCache.Default;
            var global = (IDictionary<string, dynamic>)cache.Get("global");
            return global["EsmId"];
        }

        public static string GetCurrentEsmName() {
            var cache = MemoryCache.Default;
            var global = (IDictionary<string, dynamic>)cache.Get("global");
            return global["EsmName"];
        }

        public static dynamic GetGlobalParam(string param) {
            var cache = MemoryCache.Default;
            var global = (IDictionary<string, dynamic>)cache.Get("global");
            return global[param];
        }
    }
}
