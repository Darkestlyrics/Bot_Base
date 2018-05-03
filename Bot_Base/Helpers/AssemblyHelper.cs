using Bot_Base.Classes;
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
            foreach (var item in Directory.GetFiles(Path.Combine(AppState.Path,"Plugins"))) {
                res.Add(Assembly.LoadFile(item));
                
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
    }
}
