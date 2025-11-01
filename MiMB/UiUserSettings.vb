Imports System.Configuration

<SettingsProvider(GetType(LocalFileSettingsProvider))>
NotInheritable Class UiUserSettings
    Inherits ApplicationSettingsBase

    Private Shared ReadOnly _inst As UiUserSettings =
        CType(Synchronized(New UiUserSettings()), UiUserSettings)

    Public Shared ReadOnly Property [Default] As UiUserSettings
        Get : Return _inst : End Get
    End Property

    <UserScopedSetting(), DefaultSettingValue("1.0")>
    Public Property UiScale As Single
        Get : Return CSng(Me("UiScale")) : End Get
        Set(value As Single) : Me("UiScale") = value : End Set
    End Property
End Class
