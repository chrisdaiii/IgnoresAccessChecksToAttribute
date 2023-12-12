此仓库参考 [IgnoresAccessChecksToGenerator](https://github.com/aelij/IgnoresAccessChecksToGenerator) 自己动手实践，帮助理解原理。

1. 生成 `IgnoresAccessChecksToAttribute` 特性，命名空间为 `System.Runtime.CompilerServices`，该特性只会在运行时会生效，在编译期间无效。

   ```c#
   namespace System.Runtime.CompilerServices
   {
       [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
       public sealed class IgnoresAccessChecksToAttribute : Attribute
       {
           public IgnoresAccessChecksToAttribute(string assemblyName)
           {
               AssemblyName = assemblyName;
           }

           public string AssemblyName { get; }
       }
   }
   ```
   
2. 创建一个AssemblyInfo.cs文件或随便在一个地方应用 `IgnoresAccessChecksToAttribute` 特性，表示忽略对该程序集的访问修饰符检查。

   ```c#
   [assembly: IgnoresAccessChecksTo("MyProject")]
   ```
   
3. 生成一个新的 MyProject 程序集，将所有访问级别改为 `public`。
4. 将生成的新的程序集添加到当前项目中，需要在 `AfterResolveReferences` Task之后，用于编译通过。
   
   ```c#
   <ItemGroup>
    <ReferencePath Include="新的程序集.dll" />
   </ItemGroup>
   ```
5. 移除旧的程序集引用，用于消除IDE的语法错误提示和F12跳转到定义。

   ```c#
   <ItemGroup>
    <ReferencePath Remove="旧的程序集.dll" />
   </ItemGroup>
   ```

[MSBuild项 @(ReferencePath)](https://blog.walterlv.com/post/resolve-project-references-using-target.html)

[相关问题](https://stackoverflow.com/questions/69996924/ignoreaccesscheckstoattribute-does-not-grant-access-to-internal-types-in-referen)

![image](https://github.com/chrisdaiii/IgnoresAccessChecksToAttribute/assets/67849861/460aae48-b84f-4930-9487-e93517826682)
