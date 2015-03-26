Public Class PermissionManager
    Public Enum PermissionLevels As Integer
        Banned = -25
        Restricted = -5
        Normal = 10
        Trusted = 20
        Moderator = 35
        SuperModerator = 50
        Admin = 100
    End Enum
    Public Shared Function IsAbleTo(requested As PermissionLevels) As Boolean
        Dim r As Boolean = c.localUser.permission_level >= requested
        If r = False Then
            ShowDenyActionMessage(requested)
        End If
        Return r
    End Function
    Public Shared Sub ShowDenyActionMessage(requested As PermissionLevels)
        MsgBox(String.Format("És necessari ser {0} o superior per fer això", GetLevelDisplayName(requested).ToLower))
    End Sub
    Public Shared Function GetLevel(level As Integer) As PermissionLevels
        Dim highest As PermissionLevels = PermissionLevels.Banned
        For Each v As PermissionLevels In [Enum].GetValues(GetType(PermissionLevels))
            If level >= v And v >= highest Then
                highest = v
            End If
        Next
        Return highest
    End Function

    Public Shared Function GetLevelDisplayName(l As PermissionLevels) As String
        Select Case l
            Case Is = PermissionLevels.Banned
                Return "Bandejat"
            Case Is = PermissionLevels.Restricted
                Return "Restringit"
            Case Is = PermissionLevels.Normal
                Return "Normal"
            Case Is = PermissionLevels.Trusted
                Return "Usuari de confiança"
            Case Is = PermissionLevels.Moderator
                Return "Moderador"
            Case Is = PermissionLevels.SuperModerator
                Return "Super Moderador"
            Case Is = PermissionLevels.Admin
                Return "Administrador"
        End Select
        Return "Random"
    End Function


End Class
