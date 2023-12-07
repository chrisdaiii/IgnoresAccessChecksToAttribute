此仓库参考 [IgnoresAccessChecksToGenerator](https://github.com/aelij/IgnoresAccessChecksToGenerator) 自己动手实践，帮助理解原理。

1. 生成`IgnoresAccessChecksToAttribute`特性，命名空间为`System.Runtime.CompilerServices`，该特性只会在运行时会生效，在编译期间无效。
2. 创建一个AssemblyInfo.cs文件或随便在一个地方应用`[assembly:IgnoresAccessChecksTo("your assemblyName")]`，忽略对该程序集的访问修饰符检查。
3. 对需要忽略访问修饰符检查的程序集生成一个新的程序集，将所有类改为`public`访问级别，可以放在 obj\Debug\net6.0 下。
4. 将生成的新的程序集添加到当前项目中，需要在`AfterResolveReferences`Task之后，主要用于编译通过。
   
   ```c#
   <ItemGroup>
    <ReferencePath Include="第三步的路径.dll" />
   </ItemGroup>
   ```

[MSBuild项 @(ReferencePath)](https://blog.walterlv.com/post/resolve-project-references-using-target.html)

[相关问题](https://stackoverflow.com/questions/69996924/ignoreaccesscheckstoattribute-does-not-grant-access-to-internal-types-in-referen)

![image](https://github.com/chrisdaiii/IgnoresAccessChecksToAttribute/assets/67849861/460aae48-b84f-4930-9487-e93517826682)
