dotnet build "..\MSI.Keyboard.Backlight.Manager.sln"
dotnet publish "..\MSI.Keyboard.Backlight.Manager.sln" -c Release /p:PublishProfile=Properties\PublishProfiles\FolderProfile.pubxml
AdvancedInstaller /build script.aip