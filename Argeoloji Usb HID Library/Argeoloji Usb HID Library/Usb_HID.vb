
Imports Microsoft.Win32.SafeHandles
Imports System
Imports System.Diagnostics
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms


Public Class Usb_HID_Port

        Friend Const FILE_FLAG_OVERLAPPED As Integer = 1073741824
        Friend Const FILE_SHARE_READ As Integer = 1
        Friend Const FILE_SHARE_WRITE As Integer = 2
        Friend Const GENERIC_READ As UInteger = 2147483648
        Friend Const GENERIC_WRITE As UInteger = 1073741824
        Friend Const INVALID_HANDLE_VALUE As Integer = -1
        Friend Const OPEN_EXISTING As Integer = 3
        Friend Const WAIT_TIMEOUT As Integer = 258
        Friend Const WAIT_OBJECT_0 As Integer = 0
        Friend Const DIGCF_PRESENT As Integer = 2
        Friend Const DIGCF_DEVICEINTERFACE As Integer = 16
        Friend Const HidP_Input As Short = 0
        Friend Const HidP_Output As Short = 1
        Friend Const HidP_Feature As Short = 2
        Friend Const DBT_DEVICEARRIVAL As Integer = 32768
        Friend Const DBT_DEVICEREMOVECOMPLETE As Integer = 32772
        Friend Const DBT_DEVTYP_DEVICEINTERFACE As Integer = 5
        Friend Const DBT_DEVTYP_HANDLE As Integer = 6
        Friend Const DEVICE_NOTIFY_ALL_INTERFACE_CLASSES As Integer = 4
        Friend Const DEVICE_NOTIFY_SERVICE_HANDLE As Integer = 1
        Friend Const DEVICE_NOTIFY_WINDOW_HANDLE As Integer = 0
        Friend Const WM_DEVICECHANGE As Integer = 537

        Private Cihaz As Device_Info_Structure
        Public Event Usb_Plug_Event As UsbEventsHandler
        Public Delegate Sub UsbEventsHandler(ByVal sender As Object, ByVal e As EventArgs)


        Public Sub New(ByVal VendorID As Integer, ByVal ProductID As Integer)
            Cihaz.CihazBagli = False
            Cihaz.targetVid = VendorID
            Cihaz.targetPid = ProductID
        End Sub
        Protected Overrides Sub Finalize()
            Detach_Usb_Device()
        End Sub
        Protected Sub Detach_Usb_Device()
            If CihazBagli Then
                CihazBagli = False
                If Not Cihaz.hidHandle.IsInvalid Then Cihaz.hidHandle.Close()
                If Not Cihaz.readHandle.IsInvalid Then Cihaz.readHandle.Close()
                If Not Cihaz.writeHandle.IsInvalid Then Cihaz.writeHandle.Close()
                On_Usb_Event(EventArgs.Empty)
            End If
        End Sub

        Public Property CihazBagli As Boolean
            Get
                Return Cihaz.CihazBagli
            End Get
            Private Set(ByVal value As Boolean)
                Cihaz.CihazBagli = value
            End Set
        End Property

#Region "DLL Import"

        <DllImport("kernel32.dll", SetLastError:=True)>
        Friend Shared Function CancelIo(ByVal hFile As SafeFileHandle) As Integer
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Friend Shared Function CreateEvent(ByVal SecurityAttributes As IntPtr, ByVal bManualReset As Boolean, ByVal bInitialState As Boolean, ByVal lpName As String) As IntPtr
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Friend Shared Function CreateFile(ByVal lpFileName As String, ByVal dwDesiredAccess As UInteger, ByVal dwShareMode As Integer, ByVal lpSecurityAttributes As IntPtr, ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, ByVal hTemplateFile As Integer) As SafeFileHandle
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Friend Shared Function GetOverlappedResult(ByVal hFile As SafeFileHandle, ByVal lpOverlapped As IntPtr, ByRef lpNumberOfBytesTransferred As Integer, ByVal bWait As Boolean) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Protected Shared Function ReadFile(ByVal hFile As SafeFileHandle, ByVal lpBuffer As IntPtr, ByVal nNumberOfBytesToRead As Integer, ByRef lpNumberOfBytesRead As Integer, ByVal lpOverlapped As IntPtr) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Friend Shared Function WaitForSingleObject(ByVal hHandle As IntPtr, ByVal dwMilliseconds As Integer) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Friend Shared Function WriteFile(ByVal hFile As SafeFileHandle, ByVal lpBuffer As Byte(), ByVal nNumberOfBytesToWrite As Integer, ByRef lpNumberOfBytesWritten As Integer, ByVal lpOverlapped As IntPtr) As Boolean
        End Function


        <DllImport("setupapi.dll", SetLastError:=True)>
        Friend Shared Function SetupDiCreateDeviceInfoList(ByRef ClassGuid As Guid, ByVal hwndParent As Integer) As Integer
        End Function

        <DllImport("setupapi.dll", SetLastError:=True)>
        Friend Shared Function SetupDiDestroyDeviceInfoList(ByVal DeviceInfoSet As IntPtr) As Integer
        End Function

        <DllImport("setupapi.dll", SetLastError:=True)>
        Friend Shared Function SetupDiEnumDeviceInterfaces(ByVal DeviceInfoSet As IntPtr, ByVal DeviceInfoData As IntPtr, ByRef InterfaceClassGuid As Guid, ByVal MemberIndex As Integer, ByRef DeviceInterfaceData As SP_DEVICE_INTERFACE_DATA) As Boolean
        End Function

        <DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Friend Shared Function SetupDiGetClassDevs(ByRef ClassGuid As Guid, ByVal Enumerator As IntPtr, ByVal hwndParent As IntPtr, ByVal Flags As Integer) As IntPtr
        End Function

        <DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Friend Shared Function SetupDiGetDeviceInterfaceDetail(ByVal DeviceInfoSet As IntPtr, ByRef DeviceInterfaceData As SP_DEVICE_INTERFACE_DATA, ByVal DeviceInterfaceDetailData As IntPtr, ByVal DeviceInterfaceDetailDataSize As Integer, ByRef RequiredSize As Integer, ByVal DeviceInfoData As IntPtr) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_FlushQueue(ByVal HidDeviceObject As SafeFileHandle) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_FreePreparsedData(ByVal PreparsedData As IntPtr) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_GetAttributes(ByVal HidDeviceObject As SafeFileHandle, ByRef Attributes As HIDD_ATTRIBUTES) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_GetFeature(ByVal HidDeviceObject As SafeFileHandle, ByVal lpReportBuffer As Byte(), ByVal ReportBufferLength As Integer) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_GetInputReport(ByVal HidDeviceObject As SafeFileHandle, ByVal lpReportBuffer As Byte(), ByVal ReportBufferLength As Integer) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Sub HidD_GetHidGuid(ByRef HidGuid As Guid)
        End Sub

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_GetNumInputBuffers(ByVal HidDeviceObject As SafeFileHandle, ByRef NumberBuffers As Integer) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_GetPreparsedData(ByVal HidDeviceObject As SafeFileHandle, ByRef PreparsedData As IntPtr) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_SetFeature(ByVal HidDeviceObject As SafeFileHandle, ByVal lpReportBuffer As Byte(), ByVal ReportBufferLength As Integer) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_SetNumInputBuffers(ByVal HidDeviceObject As SafeFileHandle, ByVal NumberBuffers As Integer) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidD_SetOutputReport(ByVal HidDeviceObject As SafeFileHandle, ByVal lpReportBuffer As Byte(), ByVal ReportBufferLength As Integer) As Boolean
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidP_GetCaps(ByVal PreparsedData As IntPtr, ByRef Capabilities As HIDP_CAPS) As Integer
        End Function

        <DllImport("hid.dll", SetLastError:=True)>
        Friend Shared Function HidP_GetValueCaps(ByVal ReportType As Integer, ByVal ValueCaps As Byte(), ByRef ValueCapsLength As Integer, ByVal PreparsedData As IntPtr) As Integer
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Friend Shared Function RegisterDeviceNotification(ByVal hRecipient As IntPtr, ByVal NotificationFilter As IntPtr, ByVal Flags As Integer) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Friend Shared Function UnregisterDeviceNotification(ByVal Handle As IntPtr) As Boolean
        End Function

#End Region

        Private Function Find_HID_Devices(ByRef listOfDevicePathNames As String(), ByRef numberOfDevicesFound As Integer) As Boolean

            If CihazBagli Then Detach_Usb_Device()

            Dim RequiredSize As Integer = 0
            Dim num As IntPtr = IntPtr.Zero
            Dim DeviceInfoSet As New IntPtr
            Dim flag1 As Boolean = False
            Dim MemberIndex As Integer = 0
            Dim DeviceInterfaceData As New SP_DEVICE_INTERFACE_DATA
            Dim Guid As New Guid()
            HidD_GetHidGuid(Guid)

            Dim flag2 As Boolean

            Try
                DeviceInfoSet = SetupDiGetClassDevs(Guid, IntPtr.Zero, IntPtr.Zero, 18)
                flag2 = False
                MemberIndex = 0
                DeviceInterfaceData.cbSize = Marshal.SizeOf(CObj(DeviceInterfaceData))

                Do

                    If Not SetupDiEnumDeviceInterfaces(DeviceInfoSet, IntPtr.Zero, Guid, MemberIndex, DeviceInterfaceData) Then
                        flag1 = True
                    Else
                        Dim deviceInterfaceDetail As Boolean = SetupDiGetDeviceInterfaceDetail(DeviceInfoSet, DeviceInterfaceData, IntPtr.Zero, 0, RequiredSize, IntPtr.Zero)
                        num = Marshal.AllocHGlobal(RequiredSize)
                        Marshal.WriteInt32(num, If(IntPtr.Size = 4, 4 + Marshal.SystemDefaultCharSize, 8))
                        deviceInterfaceDetail = SetupDiGetDeviceInterfaceDetail(DeviceInfoSet, DeviceInterfaceData, num, RequiredSize, RequiredSize, IntPtr.Zero)
                        Dim Pointer As New IntPtr(num.ToInt64() + 4)
                        listOfDevicePathNames(MemberIndex) = Marshal.PtrToStringAuto(Pointer)
                        flag2 = True
                    End If

                    Interlocked.Increment(MemberIndex)
                Loop While Not flag1

            Catch ex As Exception
                Call Debug.WriteLine(ex.Message.ToString())
                Return False
            Finally
                If num <> IntPtr.Zero Then Marshal.FreeHGlobal(num)
                If DeviceInfoSet <> IntPtr.Zero Then SetupDiDestroyDeviceInfoList(DeviceInfoSet)
            End Try

            If flag2 Then
                numberOfDevicesFound = MemberIndex - 2
            End If

            Return flag2
        End Function

        Public Function CheckDevicePresent() As Boolean
            Dim listOfDevicePathNames As String() = New String(127) {}
            Dim flag1 As Boolean = False
            Dim numberOfDevicesFound As Integer = 0

            Try
                Dim flag2 As Boolean = False

                If Find_HID_Devices(listOfDevicePathNames, numberOfDevicesFound) Then
                    Dim index As Integer = 0

                    Do

                        Cihaz.hidHandle = CreateFile(listOfDevicePathNames(index), 0UI, 3, IntPtr.Zero, 3, 0, 0)

                        If Not Cihaz.hidHandle.IsInvalid Then
                            Cihaz.attributes.size = Marshal.SizeOf(CObj(Cihaz.attributes))
                            flag1 = HidD_GetAttributes(Cihaz.hidHandle, Cihaz.attributes)

                            If flag1 Then

                                If CInt(Cihaz.attributes.vendorId) = Cihaz.targetVid AndAlso CInt(Cihaz.attributes.productId) = Cihaz.targetPid Then
                                    flag2 = True
                                    Cihaz.devicePathName = listOfDevicePathNames(index)
                                Else
                                    flag2 = False
                                    Cihaz.hidHandle.Close()
                                End If
                            Else
                                flag2 = False
                                Cihaz.hidHandle.Close()
                            End If
                        End If

                        Interlocked.Increment(index)
                    Loop While Not flag2 AndAlso index <> numberOfDevicesFound + 1
                End If

                If flag2 Then
                    queryDeviceCapabilities()

                    If flag1 = False Then Return False
                    Cihaz.readHandle = CreateFile(Cihaz.devicePathName, 2147483648UI, 3, IntPtr.Zero, 3, 1073741824, 0)

                    If Cihaz.readHandle.IsInvalid Then Return False

                    Cihaz.writeHandle = CreateFile(Cihaz.devicePathName, 1073741824UI, 3, IntPtr.Zero, 3, 0, 0)

                    If Cihaz.writeHandle.IsInvalid Then
                        Cihaz.writeHandle.Close()
                        Return False
                    Else
                        CihazBagli = True
                        On_Usb_Event(EventArgs.Empty)
                        Return True
                    End If
                End If
            Catch ex As Exception

            End Try

            Return False
        End Function

        Private Protected Sub queryDeviceCapabilities()
            Dim PreparsedData As New IntPtr

            Try
                Dim flag As Boolean = HidD_GetPreparsedData(Cihaz.hidHandle, PreparsedData)
                If HidP_GetCaps(PreparsedData, Cihaz.capabilities) = 0 Then Return
                Debug.WriteLine("Usb_HID_Library:queryDeviceCapabilities() -> Device query results:")
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Usage: {0}", Convert.ToString(Cihaz.capabilities.usage, 16)))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Usage Page: {0}", Convert.ToString(Cihaz.capabilities.usagePage, 16)))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Input Report Byte Length: {0}", Cihaz.capabilities.inputReportByteLength))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Output Report Byte Length: {0}", Cihaz.capabilities.outputReportByteLength))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Feature Report Byte Length: {0}", Cihaz.capabilities.featureReportByteLength))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Link Collection Nodes: {0}", Cihaz.capabilities.numberLinkCollectionNodes))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Input Button Caps: {0}", Cihaz.capabilities.numberInputButtonCaps))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Input Value Caps: {0}", Cihaz.capabilities.numberInputValueCaps))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Input Data Indices: {0}", Cihaz.capabilities.numberInputDataIndices))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Output Button Caps: {0}", Cihaz.capabilities.numberOutputButtonCaps))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Output Value Caps: {0}", Cihaz.capabilities.numberOutputValueCaps))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Output Data Indices: {0}", Cihaz.capabilities.numberOutputDataIndices))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Feature Button Caps: {0}", Cihaz.capabilities.numberFeatureButtonCaps))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Feature Value Caps: {0}", Cihaz.capabilities.numberFeatureValueCaps))
                Debug.WriteLine(String.Format("Usb_HID_Library:queryDeviceCapabilities() ->     Number of Feature Data Indices: {0}", Cihaz.capabilities.numberFeatureDataIndices))
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            Finally
                If PreparsedData <> IntPtr.Zero Then
                    HidD_FreePreparsedData(PreparsedData)
                End If
            End Try
        End Sub

        Public Function Send_Data(ByVal DataBuffer As Byte()) As Boolean

            If CihazBagli = False Then Return False
            Try
                Return WriteFile(Cihaz.writeHandle, DataBuffer, DataBuffer.Length, 0, IntPtr.Zero)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Return False
            End Try

        End Function

        Public Function Read_Data(ByRef GelenGidenData As Byte(), ReadTimeOut As Integer) As Boolean

            If CihazBagli = False Then Return False
            Dim Flag As Boolean

            Try

                Dim hHandle As IntPtr = CreateEvent(IntPtr.Zero, False, False, "")
                Dim nativeOverlapped As New NativeOverlapped With {
                    .OffsetLow = 0,
                    .OffsetHigh = 0,
                    .EventHandle = hHandle
                }
                Dim Pointer1 As IntPtr = Marshal.AllocHGlobal(GelenGidenData.Length)
                Dim Pointer2 As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(CObj(nativeOverlapped)))
                Marshal.StructureToPtr(CObj(nativeOverlapped), Pointer2, False)
                Flag = ReadFile(Cihaz.readHandle, Pointer1, GelenGidenData.Length, GelenGidenData.Length, Pointer2)

                If Flag = False Then
                    Select Case WaitForSingleObject(hHandle, ReadTimeOut)
                        Case 0
                            Flag = True
                            GetOverlappedResult(Cihaz.readHandle, Pointer2, GelenGidenData.Length, False)
                        Case 258 ' Time out Doldu.
                            Flag = False
                            CancelIo(Cihaz.readHandle)
                        Case Else
                            Flag = False
                            Detach_Usb_Device()
                    End Select
                End If

                If Flag Then
                    Marshal.Copy(Pointer1, GelenGidenData, 0, GelenGidenData.Length)
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Return False
            End Try

            Return Flag
        End Function
        Public Overridable Sub On_Usb_Event(ByVal e As EventArgs)
            RaiseEvent Usb_Plug_Event(Me, e)
        End Sub

        Private Function Bizim_Cihaz_mi(ByVal m As Message) As Boolean
            Try
                Dim Deviceinterface1 As New DEV_BROADCAST_DEVICEINTERFACE_1
                Dim DevBroadcastHdr As New DEV_BROADCAST_HDR
                Marshal.PtrToStructure(m.LParam, CObj(DevBroadcastHdr))

                If DevBroadcastHdr.dbch_devicetype = 5 Then
                    Dim int32 As Long = Convert.ToInt64((DevBroadcastHdr.dbch_size - 32) / 2)
                    Deviceinterface1.dbcc_name = New Char(CInt(int32 + 1 - 1)) {}
                    Marshal.PtrToStructure(m.LParam, CObj(Deviceinterface1))
                    Return String.Compare(New String(Deviceinterface1.dbcc_name, 0, Convert.ToInt32(CInt((DevBroadcastHdr.dbch_size - 32) / 2))).ToLower(New CultureInfo("en-US")), Cihaz.devicePathName.ToLower(New CultureInfo("en-US")), True) = 0
                End If

            Catch ex As Exception

            End Try

            Return False
        End Function

        Public Function RegisterHandleNotifications(ByVal WindowHandle As IntPtr) As Boolean
            Dim broadcastDeviceinterface As New DEV_BROADCAST_DEVICEINTERFACE
            Dim num As IntPtr = IntPtr.Zero
            Dim HidGuid As New Guid
            HidD_GetHidGuid(HidGuid)

            Try
                Dim cb As Integer = Marshal.SizeOf(CObj(broadcastDeviceinterface))
                broadcastDeviceinterface.dbcc_size = cb
                broadcastDeviceinterface.dbcc_devicetype = 5
                broadcastDeviceinterface.dbcc_reserved = 0
                broadcastDeviceinterface.dbcc_classguid = HidGuid
                num = Marshal.AllocHGlobal(cb)
                Marshal.StructureToPtr(CObj(broadcastDeviceinterface), num, True)
                Cihaz.deviceNotificationHandle = RegisterDeviceNotification(WindowHandle, num, 0)
                Marshal.PtrToStructure(num, CObj(broadcastDeviceinterface))

                If Cihaz.deviceNotificationHandle.ToInt64() = IntPtr.Zero.ToInt64() Then
                    Return False
                End If

                Return True
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            Finally
                If num <> IntPtr.Zero Then Marshal.FreeHGlobal(num)
            End Try

            Return False
        End Function

        Public Sub ParseMessages(ByVal m As Message)
            If m.Msg <> 537 Then Return
            Try

                Select Case m.WParam.ToInt64()
                    Case 32768 ' Bir Usb Cihaz Takıldı.
                        If CihazBagli = False Then ' Bir Cihaz Takıldı Fakat Bizim Cihazda Bağlı Değildi, Bağlanmayı Tekrar Deniyoruz.
                            CheckDevicePresent()
                            Exit Select
                        End If

                    Case 32772 ' Cihaz Çıkarıldı.
                        If Bizim_Cihaz_mi(m) Then ' Çıkarılan Cihaz Bizim Cihaz ise ...
                            Detach_Usb_Device()
                            Exit Select
                        End If
                End Select

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Sub



        <StructLayout(LayoutKind.Sequential)>
        Friend Class SECURITY_ATTRIBUTES
            Friend nLength As Integer
            Friend lpSecurityDescriptor As Integer
            Friend bInheritHandle As Integer
        End Class

        Private Structure Device_Info_Structure
            Public targetVid As Integer
            Public targetPid As Integer
            Public CihazBagli As Boolean
            Public attributes As HIDD_ATTRIBUTES
            Public capabilities As HIDP_CAPS
            Public readHandle As SafeFileHandle
            Public writeHandle As SafeFileHandle
            Public hidHandle As SafeFileHandle
            Public devicePathName As String
            Public deviceNotificationHandle As IntPtr
        End Structure

        Friend Structure SP_DEVICE_INTERFACE_DATA
            Friend cbSize As Integer
            Friend InterfaceClassGuid As Guid
            Friend Flags As Integer
            Friend Reserved As IntPtr
        End Structure

        Friend Structure HIDD_ATTRIBUTES
            Friend size As Integer
            Friend vendorId As UShort
            Friend productId As UShort
            Friend versionNumber As UShort
        End Structure

        Friend Structure HIDP_CAPS
            Friend usage As Short
            Friend usagePage As Short
            Friend inputReportByteLength As Short
            Friend outputReportByteLength As Short
            Friend featureReportByteLength As Short
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=17)>
            Friend reserved As Short()
            Friend numberLinkCollectionNodes As Short
            Friend numberInputButtonCaps As Short
            Friend numberInputValueCaps As Short
            Friend numberInputDataIndices As Short
            Friend numberOutputButtonCaps As Short
            Friend numberOutputValueCaps As Short
            Friend numberOutputDataIndices As Short
            Friend numberFeatureButtonCaps As Short
            Friend numberFeatureValueCaps As Short
            Friend numberFeatureDataIndices As Short
        End Structure



        <StructLayout(LayoutKind.Sequential)>
        Friend Class DEV_BROADCAST_DEVICEINTERFACE
            Friend dbcc_size As Integer
            Friend dbcc_devicetype As Integer
            Friend dbcc_reserved As Integer
            Friend dbcc_classguid As Guid
            Friend dbcc_name As Short
        End Class

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
        Friend Class DEV_BROADCAST_DEVICEINTERFACE_1
            Friend dbcc_size As Integer
            Friend dbcc_devicetype As Integer
            Friend dbcc_reserved As Integer
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16, ArraySubType:=UnmanagedType.U1)>
            Friend dbcc_classguid As Byte()
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=255)>
            Friend dbcc_name As Char()
        End Class

        <StructLayout(LayoutKind.Sequential)>
        Friend Class DEV_BROADCAST_HDR
            Friend dbch_size As Integer
            Friend dbch_devicetype As Integer
            Friend dbch_reserved As Integer
        End Class
    End Class


