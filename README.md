# WebForumOptimized
<h2>Our project is already here and works properly!.</h2>

Should you have any problems while running the project from your local Visual Studio or anywhere else, please delete the following lines from the *./PL/WebForum.csproj* file:

```
<Target Name="CopyRoslynFiles" AfterTargets="AfterBuild" Condition="!$(Disable_CopyWebApplication) And '$(OutDir)' != '$(OutputPath)'">
    <ItemGroup>
      <RoslynFiles Include="$(CscToolPath)\*" />
    </ItemGroup>
    <MakeDir Directories="$(WebProjectOutputDir)\bin\roslyn" />
    <Copy SourceFiles="@(RoslynFiles)" DestinationFolder="$(WebProjectOutputDir)\bin\roslyn" SkipUnchangedFiles="true" Retries="$(CopyRetryCount)"       RetryDelayMilliseconds="$(CopyRetryDelayMilliseconds)" />
</Target>
```
