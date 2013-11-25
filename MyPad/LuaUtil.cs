using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using LuaInterface;

namespace MyPad
{
    public class LuaUtil
    {
        public static void RegisterClass(Lua luaVM, object obj)
        {
            Type t = obj.GetType();

            MethodInfo[] methods = t.GetMethods();

            foreach (MethodInfo mInfo in methods)
            {
                if (mInfo.IsPublic && !mInfo.IsStatic)
                {
                    luaVM.RegisterFunction(mInfo.Name, obj, mInfo);
                }
            }
        }
    }
}
