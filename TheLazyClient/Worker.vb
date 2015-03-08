Imports System.ServiceModel
Imports TheLazyInterfaces
Imports System.ServiceModel.Description

Public Class Worker
    Public Shared Sub Work()
        Dim c As ChannelFactory(Of ITheLazyService) = New ChannelFactory(Of ITheLazyService)("TheLazyServiceEndpoint")

        Dim proxy As ITheLazyService = c.CreateChannel()
        proxy.DoWork()
    End Sub

End Class
