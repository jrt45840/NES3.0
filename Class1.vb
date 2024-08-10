Imports System.IO
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Xml.Serialization

Public Class NES3FileHandler

    Public Structure NESMetadata
        Public Title As String
        Public Region As String
        Public Revision As String
        Public Beta As String
        Public Proto As String
        Public Demo As String
        Public Licensed As String
        Public Aftermarket As String
        Public Pirate As String
        Public Translation As String
        Public Description As String
        Public FrontCoverBase64 As String
        Public BackCoverBase64 As String
        Public CartridgeBase64 As String

    End Structure

#Region "Append Metadata to NES File"
    Public Function AddtoNESFile(ByVal NESFile As String, ByVal NES3File As String, ByVal metadata As NESMetadata) As Boolean
        Try
            ' Read the original NES file data
            Dim originalData() As Byte = File.ReadAllBytes(NESFile)

            ' Serialize metadata to bytes
            Dim metadataBytes() As Byte = SerializeMetadataToBytes(metadata)

            ' Append a section header and metadata size
            Dim sectionHeader() As Byte = Encoding.UTF8.GetBytes("NES3META")
            Dim metadataSize() As Byte = BitConverter.GetBytes(metadataBytes.Length)

            ' Combine original data, section header, metadata size, and metadata
            Dim combinedData(originalData.Length + sectionHeader.Length + metadataSize.Length + metadataBytes.Length - 1) As Byte
            Buffer.BlockCopy(originalData, 0, combinedData, 0, originalData.Length)
            Buffer.BlockCopy(sectionHeader, 0, combinedData, originalData.Length, sectionHeader.Length)
            Buffer.BlockCopy(metadataSize, 0, combinedData, originalData.Length + sectionHeader.Length, metadataSize.Length)
            Buffer.BlockCopy(metadataBytes, 0, combinedData, originalData.Length + sectionHeader.Length + metadataSize.Length, metadataBytes.Length)

            ' Save the new NES 3.0 file
            File.WriteAllBytes(NES3File, combinedData)

            Return True
        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show("Error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function SerializeMetadataToBytes(ByVal metadata As NESMetadata) As Byte()
        Using ms As New MemoryStream()
            Using bw As New BinaryWriter(ms)
                ' Write metadata fields
                bw.Write(metadata.Title)
                bw.Write(metadata.Region)
                bw.Write(metadata.Revision)
                bw.Write(metadata.Beta)
                bw.Write(metadata.Proto)
                bw.Write(metadata.Demo)
                bw.Write(metadata.Licensed)
                bw.Write(metadata.Aftermarket)
                bw.Write(metadata.Pirate)
                bw.Write(metadata.Translation)
                bw.Write(metadata.Description)

                ' Convert and write images as Base64 strings
                bw.Write(metadata.FrontCoverBase64)
                bw.Write(metadata.BackCoverBase64)
                bw.Write(metadata.CartridgeBase64)
            End Using
            Return ms.ToArray()
        End Using
    End Function

    Public Function ConvertImageToBase64(ByVal img As Image) As String
        Using ms As New MemoryStream()
            img.Save(ms, img.RawFormat)
            Dim imgBytes() As Byte = ms.ToArray()
            Return Convert.ToBase64String(imgBytes)
        End Using
    End Function

    Public Function ConvertBase64ToImage(ByVal base64String As String) As Image
        Dim imgBytes() As Byte = Convert.FromBase64String(base64String)
        Using ms As New MemoryStream(imgBytes)
            Return Image.FromStream(ms)
        End Using
    End Function
#End Region

#Region "Read Metadata from NES 3.0 File"
    Public Function ReadNES3File(ByVal nes3file As String) As NESMetadata
        Dim metadata As New NESMetadata
        Try
            ' Read all bytes from the NES 3.0 file
            Dim fileData() As Byte = File.ReadAllBytes(nes3file)

            ' Find the metadata section
            Dim sectionHeader As Byte() = Encoding.UTF8.GetBytes("NES3META")
            Dim index As Integer = FindSectionIndex(fileData, sectionHeader)
            If index < 0 Then Throw New Exception("Metadata section not found")

            ' Read metadata size
            Dim metadataSize As Integer = BitConverter.ToInt32(fileData, index + sectionHeader.Length)

            ' Extract metadata bytes
            Dim metadataBytes(metadataSize - 1) As Byte
            Buffer.BlockCopy(fileData, index + sectionHeader.Length + 4, metadataBytes, 0, metadataSize)

            ' Deserialize metadata
            metadata = DeserializeMetadataFromBytes(metadataBytes)

            Return metadata
        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show("Error: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function DeserializeMetadataFromBytes(ByVal metadataBytes() As Byte) As NESMetadata
        Dim metadata As New NESMetadata
        Try
            Using ms As New MemoryStream(metadataBytes)
                Using br As New BinaryReader(ms)
                    ' Read metadata fields
                    metadata.Title = br.ReadString()
                    metadata.Region = br.ReadString()
                    metadata.Revision = br.ReadString()
                    metadata.Beta = br.ReadString()
                    metadata.Proto = br.ReadString()
                    metadata.Demo = br.ReadString()
                    metadata.Licensed = br.ReadString()
                    metadata.Aftermarket = br.ReadString()
                    metadata.Pirate = br.ReadString()
                    metadata.Translation = br.ReadString()
                    metadata.Description = br.ReadString()

                    ' Read Base64 encoded images
                    metadata.FrontCoverBase64 = br.ReadString()
                    metadata.BackCoverBase64 = br.ReadString()
                    metadata.CartridgeBase64 = br.ReadString()
                End Using
            End Using
        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show("Error: " & ex.Message)
        End Try
        Return metadata
    End Function

    Private Function FindSectionIndex(ByVal fileData() As Byte, ByVal sectionHeader As Byte()) As Integer
        Dim headerLength As Integer = sectionHeader.Length

        ' Search for the section header in the file data
        For i As Integer = 0 To fileData.Length - headerLength
            Dim match As Boolean = True

            ' Check if the section header matches
            For j As Integer = 0 To headerLength - 1
                If fileData(i + j) <> sectionHeader(j) Then
                    match = False
                    Exit For
                End If
            Next

            If match Then
                Return i
            End If
        Next

        ' Return -1 if section header not found
        Return -1
    End Function

#End Region

End Class
