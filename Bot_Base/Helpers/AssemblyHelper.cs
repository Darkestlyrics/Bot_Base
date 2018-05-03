using Bot_Base.Classes;
using NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Base.Helpers {
    static class AssemblyHelper {

        public static List<Assembly> GetAssemblies() {
            List<Assembly> res = new List<Assembly>();
            foreach (var item in Directory.GetFiles(Path.Combine(AppState.Path, "Plugins"))) {
                Assembly assembly = Assembly.LoadFile(item);
                GetDependencies(assembly);
                res.Add(assembly);
            }
            return res;
        }

        private static void GetDependencies(Assembly assembly) {
            List<Package> a = assembly.GetReferencedAssemblies().Select(o => new Package(o.Name, o.Version.ToString())).ToList() ;
            List<Package> b = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(o => new Package(o.Name, o.Version.ToString())).ToList();
            List<Package> c = GetDownloads(b, a);
            c.ForEach(o => DownloadPackage(o.Name,o.Version));
        }


        private static List<Package> GetDownloads(List<Package> internalPackages,List<Package> externalPackages) {
            List<Package> res = new List<Package>();
            bool found = false;
            foreach (Package exPackage in externalPackages) {
                found = false;
                foreach(Package inPackage in internalPackages) {
                    if (inPackage.Name == exPackage.Name) { //We have this package
                        //Check versions
                        if (new Version(exPackage.Version) <= new Version(inPackage.Version)) {
                            //The external package is newer
                            found = true;
                            continue;
                        } 
                    } 
                }
                if (!found) res.Add(exPackage);
            }
            return res;
        }

        public static MethodInfo MethodOf(Expression<System.Action> expression) {
            MethodCallExpression body = (MethodCallExpression)expression.Body;
            return body.Method;
        }

        public static bool MethodHasAuthorizeAttribute(Expression<System.Action> expression) {
            MemberInfo member = MethodOf(expression);
            return MemberHasAuthorizeAttribute(member);
        }

        public static bool TypeHasAuthorizeAttribute(Type t) {
            return MemberHasAuthorizeAttribute(t);
        }

        private static bool MemberHasAuthorizeAttribute(MemberInfo member) {
            const bool includeInherited = false;
            return member.GetCustomAttributes(typeof(DSharpPlus.CommandsNext.Command), includeInherited).Any();
        }

        private static void DownloadPackage(string packageName,string version) {
            var repo = PackageRepositoryFactory.Default
                .CreateRepository("https://packages.nuget.org/api/v2");

            string path = AppState.Path;
            var packageManager = new PackageManager(repo, path);
            packageManager.PackageInstalled += PackageManager_PackageInstalled;
            var package = repo.FindPackage(packageName);
            if (package != null) {
                packageManager.InstallPackage(package, false, true);
            }
        }

        private static void PackageManager_PackageInstalled(object sender,
                                                    PackageOperationEventArgs e) {
            var files = e.FileSystem.GetFiles(e.InstallPath, "*.dll", true);
            foreach (var file in files) {
                try {
                    AppDomain domain = AppDomain.CreateDomain("tmp");
                    Type typeProxyType = typeof(TypeProxy);
                    var typeProxyInstance = (TypeProxy)domain.CreateInstanceAndUnwrap(
                            typeProxyType.Assembly.FullName,
                            typeProxyType.FullName);

                    var type = typeProxyInstance.LoadFromAssembly(file, "Command");
                    object instance =
                        domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
                } catch (Exception ex) {

                }
            }
        }
    }

    public class TypeProxy : MarshalByRefObject {
        public Type LoadFromAssembly(string assemblyPath, string typeName) {
            try {
                var asm = Assembly.LoadFile(assemblyPath);
                return asm.GetType(typeName);
            } catch (Exception) { return null; }
        }
    }
    public  class Package {
        private string name;

        public string Name {
            get { return name; }
            set { name = value; }
        }

        private string version;

        public string Version {
            get { return version; }
            set { version = value; }
        }
        public Package() {

        }

        public Package(string name,string version) {
            Name = name;
            Version = version;
        }
    }
}
