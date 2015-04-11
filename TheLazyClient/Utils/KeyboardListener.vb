
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices
Imports System.Windows.Input


''' <summary>
''' Listens keyboard globally.
''' 
''' <remarks>Uses WH_KEYBOARD_LL.</remarks>
''' </summary>
Public Class KeyboardListener
    Implements IDisposable
    ''' <summary>
    ''' Creates global keyboard listener.
    ''' </summary>
    Public Sub New()
        ' We have to store the HookCallback, so that it is not garbage collected runtime
        hookedLowLevelKeyboardProc = DirectCast(AddressOf LowLevelKeyboardProc, InterceptKeys.LowLevelKeyboardProc)

        ' Set the hook
        hookId = InterceptKeys.SetHook(hookedLowLevelKeyboardProc)

        ' Assign the asynchronous callback event
        hookedKeyboardCallbackAsync = New KeyboardCallbackAsync(AddressOf KeyboardListener_KeyboardCallbackAsync)
    End Sub

    ''' <summary>
    ''' Destroys global keyboard listener.
    ''' </summary>
    Protected Overrides Sub Finalize()
        Try
            Dispose()
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    ''' <summary>
    ''' Fired when any of the keys is pressed down.
    ''' </summary>
    Public Event KeyDown As RawKeyEventHandler

    ''' <summary>
    ''' Fired when any of the keys is released.
    ''' </summary>
    Public Event KeyUp As RawKeyEventHandler

#Region "Inner workings"
    ''' <summary>
    ''' Hook ID
    ''' </summary>
    Private hookId As IntPtr = IntPtr.Zero

    ''' <summary>
    ''' Asynchronous callback hook.
    ''' </summary>
    ''' <param name="nCode"></param>
    ''' <param name="wParam"></param>
    ''' <param name="lParam"></param>
    Private Delegate Sub KeyboardCallbackAsync(keyEvent As InterceptKeys.KeyEvent, vkCode As Integer)

    ''' <summary>
    ''' Actual callback hook.
    ''' 
    ''' <remarks>Calls asynchronously the asyncCallback.</remarks>
    ''' </summary>
    ''' <param name="nCode"></param>
    ''' <param name="wParam"></param>
    ''' <param name="lParam"></param>
    ''' <returns></returns>
    <MethodImpl(MethodImplOptions.NoInlining)> _
    Private Function LowLevelKeyboardProc(nCode As Integer, wParam As UIntPtr, lParam As IntPtr) As IntPtr
        If nCode >= 0 Then
            If wParam.ToUInt32() = CInt(InterceptKeys.KeyEvent.WM_KEYDOWN) OrElse wParam.ToUInt32() = CInt(InterceptKeys.KeyEvent.WM_KEYUP) OrElse wParam.ToUInt32() = CInt(InterceptKeys.KeyEvent.WM_SYSKEYDOWN) OrElse wParam.ToUInt32() = CInt(InterceptKeys.KeyEvent.WM_SYSKEYUP) Then
                hookedKeyboardCallbackAsync.BeginInvoke(CType(wParam.ToUInt32(), InterceptKeys.KeyEvent), Marshal.ReadInt32(lParam), Nothing, Nothing)
            End If
        End If

        Return InterceptKeys.CallNextHookEx(hookId, nCode, wParam, lParam)
    End Function

    ''' <summary>
    ''' Event to be invoked asynchronously (BeginInvoke) each time key is pressed.
    ''' </summary>
    Private hookedKeyboardCallbackAsync As KeyboardCallbackAsync

    ''' <summary>
    ''' Contains the hooked callback in runtime.
    ''' </summary>
    Private hookedLowLevelKeyboardProc As InterceptKeys.LowLevelKeyboardProc

    ''' <summary>
    ''' HookCallbackAsync procedure that calls accordingly the KeyDown or KeyUp events.
    ''' </summary>
    ''' <param name="keyEvent">Keyboard event</param>
    ''' <param name="vkCode">VKCode</param>
    Private Sub KeyboardListener_KeyboardCallbackAsync(keyEvent As InterceptKeys.KeyEvent, vkCode As Integer)
        Select Case keyEvent
            ' KeyDown events
            Case InterceptKeys.KeyEvent.WM_KEYDOWN
                RaiseEvent KeyDown(Me, New RawKeyEventArgs(vkCode, False))
                Exit Select
            Case InterceptKeys.KeyEvent.WM_SYSKEYDOWN
                RaiseEvent KeyDown(Me, New RawKeyEventArgs(vkCode, True))
                Exit Select

                ' KeyUp events
            Case InterceptKeys.KeyEvent.WM_KEYUP
                RaiseEvent KeyUp(Me, New RawKeyEventArgs(vkCode, False))
                Exit Select
            Case InterceptKeys.KeyEvent.WM_SYSKEYUP
                RaiseEvent KeyUp(Me, New RawKeyEventArgs(vkCode, True))
                Exit Select
            Case Else

                Exit Select
        End Select
    End Sub

#End Region

#Region "IDisposable Members"

    ''' <summary>
    ''' Disposes the hook.
    ''' <remarks>This call is required as it calls the UnhookWindowsHookEx.</remarks>
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        InterceptKeys.UnhookWindowsHookEx(hookId)
    End Sub

#End Region
End Class
''' <summary>
''' Raw KeyEvent arguments.
''' </summary>
Public Class RawKeyEventArgs
    Inherits EventArgs
    ''' <summary>
    ''' VKCode of the key.
    ''' </summary>
    Public VKCode As Integer

    ''' <summary>
    ''' WPF Key of the key.
    ''' </summary>
    Public Key As Key

    ''' <summary>
    ''' Is the hitted key system key.
    ''' </summary>
    Public IsSysKey As Boolean

    ''' <summary>
    ''' Create raw keyevent arguments.
    ''' </summary>
    ''' <param name="VKCode"></param>
    ''' <param name="isSysKey"></param>
    Public Sub New(VKCode As Integer, isSysKey As Boolean)
        Me.VKCode = VKCode
        Me.IsSysKey = isSysKey
        Me.Key = System.Windows.Input.KeyInterop.KeyFromVirtualKey(VKCode)
    End Sub
End Class

''' <summary>
''' Raw keyevent handler.
''' </summary>
''' <param name="sender">sender</param>
''' <param name="args">raw keyevent arguments</param>
Public Delegate Sub RawKeyEventHandler(sender As Object, args As RawKeyEventArgs)

#Region "WINAPI Helper class"
''' <summary>
''' Winapi Key interception helper class.
''' </summary>
Friend NotInheritable Class InterceptKeys
    Private Sub New()
    End Sub
    Public Delegate Function LowLevelKeyboardProc(nCode As Integer, wParam As UIntPtr, lParam As IntPtr) As IntPtr
    Public Shared WH_KEYBOARD_LL As Integer = 13

    Public Enum KeyEvent As Integer
        WM_KEYDOWN = 256
        WM_KEYUP = 257
        WM_SYSKEYUP = 261
        WM_SYSKEYDOWN = 260
    End Enum

    Public Shared Function SetHook(proc As LowLevelKeyboardProc) As IntPtr
        Using curProcess As Process = Process.GetCurrentProcess()
            Using curModule As ProcessModule = curProcess.MainModule
                Return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0)
            End Using
        End Using
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function SetWindowsHookEx(idHook As Integer, lpfn As LowLevelKeyboardProc, hMod As IntPtr, dwThreadId As UInteger) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function UnhookWindowsHookEx(hhk As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function CallNextHookEx(hhk As IntPtr, nCode As Integer, wParam As UIntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function GetModuleHandle(lpModuleName As String) As IntPtr
    End Function
End Class
#End Region

